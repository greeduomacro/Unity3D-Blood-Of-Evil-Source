using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AEntity : MonoBehaviour
{
	private ServiceLocator serviceLocator;
	private LanguageManager languageManager;		//module

	public ServiceLocator ServiceLocator
	{
		get { return serviceLocator; } 
		protected set { serviceLocator = value; }
	}

	public LanguageManager LanguageManager
	{
		get { return languageManager; }
		protected set { languageManager = value; }
	}
}
public class Barbarian: APlayer
{
	#region Attributes
	protected float stepsDid;
	protected Vector3 distanceOfTheLastFrame;
	#endregion
	#region Properties
	public float StepsDid {	get { return stepsDid; }
							private set { stepsDid = value; } }
	public Vector3 DistanceFromLastFrame {	get { return distanceOfTheLastFrame; }
											private set { distanceOfTheLastFrame = value; } }
	#endregion
	public void Start()
	{
		base.InitializeSpecificFeaturesOfClass(e_playerClass.Barbarian, new BarbarianSkills<APlayer>(), new BarbarianAttribute<APlayer>());
		//this.items = new ItemManager(this.category);
	}

	void Update()
	{
		base.MyUpdate();

		//SoundManager.Instance.PlayStep3DSound(ref this.stepsDid, ref this.distanceOfTheLastFrame, trans);
	} 
}
