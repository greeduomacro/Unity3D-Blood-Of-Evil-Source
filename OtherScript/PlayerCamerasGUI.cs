using UnityEngine;
using System.Collections;

public sealed class PlayerCamerasGUI<TModuleType> : AGUIWindow<TModuleType> where TModuleType : APlayer
{
	private enum e_rect
	{
		MinimapButton,
		MinimapBorder,
		RotateLeft,
		RotateRight,
		ZoomFactorLabel,
		Zoom,
		Dezoom,
		DifficultyAndDate,
		AreaName,
		TransparentMinimap,
		OnHoverRect,
		SIZE
	}
	//public RenderTexture renderTexture;
	//private Texture2D minimapTexture;
	//private Material mat;
	#region Attributes
	private PlayerCameras<APlayer> cameras;
	#endregion
	#region Builder
	public override void Initialize(TModuleType player)
	{
		base.SetModule(player, "Scripts/GUI/Objects/Cameras");
		this.GUIWindowInitialization(new Rect(0, 0, 1, 1), false, false);
		this.cameras = base.ModuleManager.Objects.Cameras;

		this.rects = new Rect[(int)e_rect.SIZE];

		base.rects[(int)e_rect.MinimapButton] = new Rect(0.88541f, 0.3195f, 0.021875f, 0.047085f);
		base.rects[(int)e_rect.ZoomFactorLabel] = new Rect(0.890625f, 0.26345f, 0.021875f, 0.047085f);
		base.rects[(int)e_rect.MinimapBorder] = new Rect(0.8015625f, 0.019058f, 0.1911458f, 0.293721f);
		base.rects[(int)e_rect.RotateLeft] = new Rect(0.848958f, 0.319506f, 0.016666f, 0.0358744f);
		base.rects[(int)e_rect.RotateRight] = new Rect(0.9270833f, 0.319506f, 0.016666f, 0.0358744f);
		base.rects[(int)e_rect.Dezoom] = new Rect(0.8671875f, 0.319506f, 0.016666f, 0.0358744f);
		base.rects[(int)e_rect.Zoom] = new Rect(0.9088541f, 0.319506f, 0.016666f, 0.0358744f);
		base.rects[(int)e_rect.DifficultyAndDate] = new Rect(0.80042708f, -0.0015f, 0.1911458f, 0.3f);
		base.rects[(int)e_rect.AreaName] = new Rect(0.8015625f, -0.0015f, 0.1911458f, 0.3f);
		base.rects[(int)e_rect.TransparentMinimap] = new Rect(0.2f, 0.2f, 0.6f, 0.6f);
		base.rects[(int)e_rect.OnHoverRect] = new Rect (0.0f, 0.0f, 0.1f, 0.035f);
		//this.cameras.Cameras[(int)e_whichCamera.Minimap].targetTexture = renderTexture;
	}
	#endregion
	#region Override Functions
	public override void OnGUIDrawWindow(int windowID)
	{
		base.OnGUIDrawWindow(windowID);

		Font oldFont = GUI.skin.label.font;
		GUI.skin.box.font = base.ModuleManager.ServiceLocator.FontManager.Get("Diablo");

		if (this.cameras.Cameras[(int)e_whichCamera.Minimap].gameObject.activeSelf)
		{
			this.MinimapBorderAndInformations();
			this.MinusAndMajusButtons();
			this.RotateLeftAndRightButtons();
			this.OnHoverText();
			//this.ShowTransparentMinimap();
		}

		this.AreaNameLabel();
		this.DateLabel();

		GUI.skin.box.font = oldFont;
	}

	//void Update()
	//{
	//    //minimapTexture = RTImage(this.cameras.Cameras[(int)e_whichCamera.Minimap]);
	//    //minimapTexture = ReduceTextureAlpha(minimapTexture);
	//    //minimapTexture.
		
	//}
	#endregion
	#region Functions
	private void OnHoverText()
	{
		GUIExtension.OnHoverTextCenterWithMousePosition(base.rects[(int)e_rect.Zoom], base.rects[(int)e_rect.OnHoverRect],
				"<color=#00FF00>" + MultiResolutions.Font(16) + base.ModuleManager.LanguageManager.GetText("Zoom") + "</size></color>", GUI.skin.box);

		GUIExtension.OnHoverTextCenterWithMousePosition(base.rects[(int)e_rect.Dezoom], base.rects[(int)e_rect.OnHoverRect],
			"<color=#FF0000>" + MultiResolutions.Font(16) + base.ModuleManager.LanguageManager.GetText("Unzoom") + "</size></color>", GUI.skin.box);

		GUIExtension.OnHoverTextCenterWithMousePosition(base.rects[(int)e_rect.RotateLeft], base.rects[(int)e_rect.OnHoverRect],
			"<color=#3333CC>" + MultiResolutions.Font(16) + base.ModuleManager.LanguageManager.GetText("Rotate left") + "</size></color>", GUI.skin.box);

		GUIExtension.OnHoverTextCenterWithMousePosition(base.rects[(int)e_rect.RotateRight], base.rects[(int)e_rect.OnHoverRect],
			"<color=#3333CC>" + MultiResolutions.Font(16) + base.ModuleManager.LanguageManager.GetText("Rotate right") + "</size></color>", GUI.skin.box);

		GUIExtension.OnHoverTextCenterWithMousePosition(base.rects[(int)e_rect.MinimapButton], base.rects[(int)e_rect.OnHoverRect],
			"<color=#FF0000>" + MultiResolutions.Font(16) + base.ModuleManager.LanguageManager.GetText("Disable minimap") + "</size></color>",
			GUI.skin.box);

	}
	Texture2D ReduceTextureAlpha(Texture2D texture)
	{
		Color[] sourcePixels = texture.GetPixels();

		for (short i =0; i < sourcePixels.Length; i++)
		{
			sourcePixels[i] = new Color(
				sourcePixels[i].r,
				sourcePixels[i].g,
				sourcePixels[i].b,
				sourcePixels[i].a * 0.5f);
		}

		texture.SetPixels(sourcePixels);
		texture.Apply();

		return texture;
	}

	Texture2D RTImage(Camera cam)
	{
		RenderTexture currentRT = RenderTexture.active;
		RenderTexture.active = cam.targetTexture;
		cam.Render();
		Texture2D image = new Texture2D(cam.targetTexture.width, cam.targetTexture.height);
		image.ReadPixels(new Rect(0, 0, cam.targetTexture.width, cam.targetTexture.height), 0, 0);
		image.Apply();
		RenderTexture.active = currentRT;
		return image;
	}

	
	#endregion
	#region functions
	void ShowTransparentMinimap()
	{
		//GUI.DrawTexture(MultiResolutions.Rectangle(base.rects[(int)e_rect.TransparentMinimap]), minimapTexture);
	}
	void MinimapBorderAndInformations()
	{
		if (GUI.Button(MultiResolutions.Rectangle(ref base.rects[(int)e_rect.MinimapButton]), base.ModuleManager.ServiceLocator.TextureManager.GetMenuTexture("Minimap Menu")))
		{
			base.ModuleManager.ServiceLocator.SoundManager.PlaySoundIfItNotPlaying("Click");
			this.cameras.EnableAndDisableMinimap();
		}
		GUI.Label(MultiResolutions.Rectangle(ref base.rects[(int)e_rect.ZoomFactorLabel]), "x" + (0.01f * this.cameras.Parameters[(int)e_whichCamera.Minimap].Height).ToString("F2"));
		GUI.Box(MultiResolutions.Rectangle(ref base.rects[(int)e_rect.MinimapBorder]), "");
		GUI.DrawTexture(MultiResolutions.Rectangle(ref base.rects[(int)e_rect.MinimapBorder]), base.ModuleManager.ServiceLocator.TextureManager.GetPlayerCameraTexture("borderMinimap"));
	}

	void RotateLeftAndRightButtons()
	{
		if (GUI.RepeatButton(MultiResolutions.Rectangle(ref base.rects[(int)e_rect.RotateLeft]), base.ModuleManager.ServiceLocator.TextureManager.GetPlayerCameraTexture("rotateLeft")))
		{
			base.ModuleManager.ServiceLocator.SoundManager.PlaySoundIfItNotPlaying("Click");
			this.cameras.RotateLeftMinimap();
		}
		if (GUI.RepeatButton(MultiResolutions.Rectangle(ref base.rects[(int)e_rect.RotateRight]), base.ModuleManager.ServiceLocator.TextureManager.GetPlayerCameraTexture("rotateRight")))
		{
			base.ModuleManager.ServiceLocator.SoundManager.PlaySoundIfItNotPlaying("Click");
			this.cameras.RotateRightMinimap();
		}
	}

	void MinusAndMajusButtons()
	{
		if (GUI.RepeatButton(MultiResolutions.Rectangle(ref base.rects[(int)e_rect.Dezoom]), base.ModuleManager.ServiceLocator.TextureManager.GetPlayerCameraTexture("minus")))
		{
			base.ModuleManager.ServiceLocator.SoundManager.PlaySoundIfItNotPlaying("Click");
			this.cameras.DezoomMinimap();
		}
		if (GUI.RepeatButton(MultiResolutions.Rectangle(ref base.rects[(int)e_rect.Zoom]), base.ModuleManager.ServiceLocator.TextureManager.GetPlayerCameraTexture("majus")))
		{
			base.ModuleManager.ServiceLocator.SoundManager.PlaySoundIfItNotPlaying("Click");
			this.cameras.ZoomMinimap();
		}
	}

	void DateLabel()
	{
		System.TimeSpan time = System.DateTime.Now.TimeOfDay;

		GUI.skin.label.alignment = TextAnchor.UpperRight;
		GUI.Label(MultiResolutions.Rectangle(ref base.rects[(int)e_rect.DifficultyAndDate]), string.Format("<b>" + base.ModuleManager.LanguageManager.GetText("Easy").ToUpper() + " {0:D2}:{1:D2}:{2:D2}</b>",
			time.Hours,
			time.Minutes,
			time.Seconds));
		GUI.skin.label.alignment = TextAnchor.UpperLeft;
	}

	void AreaNameLabel()
	{
		GUI.Label(MultiResolutions.Rectangle(ref base.rects[(int)e_rect.AreaName]), 
			"<b><color=#00FF00FF>" +
			base.ModuleManager.LanguageManager.GetText(base.ModuleManager.Collisions.ChangeArea.Text) +"</color></b>");
	}
	#endregion
}
