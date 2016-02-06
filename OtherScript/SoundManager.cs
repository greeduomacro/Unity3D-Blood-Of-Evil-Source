using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public sealed class SoundManager : AServiceInitializer
{
	public class BloodOfEvilStep
	{
		[SerializeField]
		private string soundName;
		[SerializeField]
		private Material material;


		public Material Material { get { return material; } private set { material = value; } }
		public string SoundName { get { return soundName; } private set { soundName = value; } }
	}
	#region Attributes
	private AudioSource[] sounds;
	private BloodOfEvilStep[] steps;
	private SoundParameters[] soundsParameters;
	#endregion
	#region Data Attributes
	[SerializeField]
	private float distanceToPlaySound;
	private float volumeMaster;
	private float volumeMusic;
	private float volumeSFX;
	private float volumeDialog;
	#endregion
	#region Properties
	public AudioSource[] Sounds
	{
		get { return sounds; }
		private set { sounds = value; }
	}
	public BloodOfEvilStep[] Steps
	{
		get { return steps; }
		private set { steps = value; }
	}
	public SoundParameters[] SoundsParameters
	{
		get { return soundsParameters; }
		private set { soundsParameters = value; }
	}
	#endregion
	#region Data Properties
	public float DistanceToPlaySound
	{
		get { return distanceToPlaySound; }
		private set { distanceToPlaySound = value; }
	}
	public float VolumeMaster { get { return volumeMaster; } set { if (value >= 0) { volumeMaster = value; this.ModifyAllVolume(); } } }
	public float VolumeMusic { get { return volumeMusic; } set { if (value >= 0) { volumeMusic = value; this.ModifyCategoryVolume(e_soundCategory.Music, this.volumeMusic); } } }
	public float VolumeSFX { get { return volumeSFX; } set { if (value >= 0) { volumeSFX = value; this.ModifyCategoryVolume(e_soundCategory.SFX, this.volumeSFX); } } }
	public float VolumeDialog { get { return volumeDialog; } set { if (value >= 0) { volumeDialog = value; this.ModifyCategoryVolume(e_soundCategory.Dialog, this.volumeDialog); } } }
	#endregion
	#region Unity Functions
	#region Builder
	public override void  Initialize()
	{
		this.distanceToPlaySound = 3f;
		this.InitializeVolume();
		this.InitializeSounds();
	}
	void InitializeVolume()
	{
		this.volumeMaster = 100f;
		this.volumeMusic = 100f;
		this.volumeSFX = 100f;
		this.volumeDialog = 100f;
	}

	void InitializeSounds()
	{
		Transform trans = transform;

		this.sounds = GameObject.FindObjectsOfType<AudioSource>();//transform.GetComponentsInChildren<AudioSource>()
		this.InitializeSoundsParameters();
		Array.ForEach(this.sounds, sound => sound.transform.parent = trans);
	}
	#endregion
	void InitializeSoundsParameters()
	{
		this.soundsParameters = new SoundParameters[this.sounds.Length];

		for (byte i = 0; i < this.sounds.Length; i++)
		{
			SoundParameters parameters = this.sounds[i].GetComponent<SoundParameters>();

			if (null == parameters)
				Debug.LogError("The sound called : " + this.sounds[i].gameObject.name + " don't have the Component SoundParameters");

			this.soundsParameters[i] = parameters;
		}
	}
	#endregion
	#region Functions
	public void ModifyCategoryVolume(e_soundCategory category, float volumeInPercent)
	{
		SoundParameters[] soundsParameters = GameObject.FindObjectsOfType<SoundParameters>();

		for (byte i = 0; i < soundsParameters.Length; i++)
			if (soundsParameters[i].Category == category)
				soundsParameters[i].GetComponent<AudioSource>().volume = soundsParameters[i].VolumeOfInitialization * this.volumeMaster * volumeInPercent * 0.0001f;
	}

	public void ModifyAllVolume()
	{
		SoundParameters[] soundsParameters = GameObject.FindObjectsOfType<SoundParameters>();

		for (byte i = 0; i < soundsParameters.Length; i++)
		{
			switch (soundsParameters[i].Category)
			{
				case e_soundCategory.Music: soundsParameters[i].GetComponent<AudioSource>().volume = soundsParameters[i].VolumeOfInitialization * this.volumeMaster * this.volumeMusic * 0.0001f; break;
				case e_soundCategory.SFX: soundsParameters[i].GetComponent<AudioSource>().volume = soundsParameters[i].VolumeOfInitialization * this.volumeMaster * this.volumeSFX * 0.0001f; break;
				case e_soundCategory.Dialog: soundsParameters[i].GetComponent<AudioSource>().volume = soundsParameters[i].VolumeOfInitialization * this.volumeMaster * this.volumeDialog * 0.0001f; break;
			}
		}
	}

	public void PlayStep3DSound(ref float stepsDid, ref Vector3 distanceOfTheLastFrame, Transform parentObject)
	{
		stepsDid += Vector3.Distance(distanceOfTheLastFrame, parentObject.position) * 2.15f;
		distanceOfTheLastFrame = parentObject.position;

		if (stepsDid >= this.distanceToPlaySound && distanceToPlaySound > 0)
		{
			//RaycastHit hit;

			//if (Physics.Raycast(parentObject.position, -Vector3.up * 100, out hit))
			//{
			//    if (null != hit.collider.gameObject.renderer.sharedMaterial)
			//    {
					//string materialName = "FS_Gazon_01";//hit.collider.gameObject.renderer.sharedMaterial.name;
					//BloodOfEvilStep step = Array.Find(this.steps, stepNode => stepNode.Material.name == materialName);

					//if (null != step)
					{
						//Debug.Log("Im making a step with sound : " + "FS_Gazon_01"/*step.SoundName*/);
						this.GetAndDestroy3DSound("FS_Gazon_01"/*step.SoundName*/, parentObject).PlayOneShot(Array.Find(sounds, sound => sound.name == "FS_Gazon_01").clip);
						stepsDid = 0;
						return;
					}
			//    }
			//}
		}
	}

	

	public AudioSource Get(string name)
	{
		AudioSource src = this.GetSound(name);

		//if (null == src)
		//    Debug.LogWarning("Fmod Sound Manager didn't find your AudioSource (" + name + ") , it will create one new instance of AudioSource for you");
		//else if (src.type == FmodEvent.SourceType.SOURCE_3D)
		//    Debug.LogError("You try to call a 3D, so call the function GetAndDestroy3DSound or Get3DSound for environmenet and ambiance");

		return src;
	}

	public AudioSource GetAndInstantiate(string name)
	{
		AudioSource src = this.GetSound(name);

		GameObject soundObject = Instantiate(src.gameObject) as GameObject;

		Destroy(soundObject, 10f);

		return soundObject.GetComponent<AudioSource>();
	}

	public AudioSource Get2DSound(string name)
	{
		AudioSource src = this.GetSound(name);

		//if (null == src)
		//    Debug.LogWarning("Fmod Sound Manager didn't find your AudioSource (" + name + ") , it will create one new instance of AudioSource for you");
		//else if (src.type == FmodEvent.SourceType.SOURCE_3D)
		//    Debug.LogError("You try to call a 3D, so call the function GetAndDestroy3DSound or Get3DSound for environmenet and ambiance");

		return src;
	}

	public AudioSource GetSound(string name)
	{
		foreach (AudioSource soundy in sounds)
			if (soundy.name == name)
				return soundy;
		
		Debug.LogWarning("the sound " + name + " have not been find");

		return null;
	}

	public void PlaySoundIfItNotPlaying(string name)
	{
		foreach (AudioSource soundy in sounds)
		{
			if (soundy.name == name)
			{
				if (!soundy.isPlaying)
					soundy.Play();
				return;
			}
		}

		Debug.LogWarning("the sound " + name + " have not been find");
	}

	public AudioSource GetAndDestroy3DSound(string name, Transform parentObject)
	{
		GameObject objectSound = this.InstantiateSoundObject(name, parentObject);

		Destroy(objectSound, 30f);

		return objectSound.GetComponent<AudioSource>();
	}

	public AudioSource Get3DSound(string soundName, Transform parentObject)
	{
		GameObject objectSound = this.InstantiateSoundObject(soundName, parentObject);

		return objectSound.GetComponent<AudioSource>();
	}

	public GameObject GetSoundObject(string soundName)
	{
		GameObject obj = null;
		try
		{
			obj = transform.Find(soundName).gameObject;
		}
		catch (Exception exp) { Debug.LogError(soundName + " : " + exp.Message); }

		return obj;
	}

	public GameObject InstantiateSoundObject(string soundName, Transform parentObject)
	{
		GameObject objectSound = Instantiate(this.GetSoundObject(soundName)) as GameObject;

		objectSound.transform.parent = parentObject;
		objectSound.transform.position = parentObject.position;
		objectSound.transform.rotation = parentObject.rotation;

		return objectSound;
	}
	#endregion
}
