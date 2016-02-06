using UnityEngine;
using System.Collections;

public sealed class TextureManager : AServiceInitializer
{
	#region Attributes
	[SerializeField]
	private Texture2D[] skillTextures = null;
	[SerializeField]
	private Texture2D[] playerCamerasTextures = null;
	[SerializeField]
	private Texture2D[] guiTextures = null;
	[SerializeField]
	private Texture2D[] mainMenuTextures = null;
	[SerializeField]
	private Texture2D[] mouseCursorTextures = null;
	[SerializeField]
	private Texture2D[] playerResources = null;
	[SerializeField]
	private Texture2D[] menuTextures = null;
	#endregion
	#region Data Attributes
	private int[] skillTexturesID;
	private int[] playerCamerasID;
	private int[] guiTexturesID;
	private int[] mainMenuTexturesID;
	private int[] mouseCursorTexturesID;
	private int[] playerResourcesTexturesID;
	private int[] menuTexturesID;
	#endregion
	#region Properties
	public Texture2D[] PlayerCamerasTextures
	{
		get { return playerCamerasTextures; }
		private set { playerCamerasTextures = value; }
	}
	public Texture2D[] GUITextures
	{
		get { return guiTextures; }
		private set { guiTextures = value; }
	}
	public Texture2D[] MainMenuTextures
	{
		get { return mainMenuTextures; }
		private set { mainMenuTextures = value; }
	}
	public Texture2D[] MouseCursorTextures
	{
		get { return mouseCursorTextures; }
		private set { mouseCursorTextures = value; }
	}
	public Texture2D[] PlayerResources
	{
		get { return playerResources; }
		private set { playerResources = value; }
	}
	public Texture2D[] MenuTextures
	{
		get { return menuTextures; }
		private set { menuTextures = value; }
	}
	public Texture2D[] SkillTextures
	{
		get { return skillTextures; }
		private set { skillTextures = value; }
	}
	#endregion
	#region Data Properties
	public int[] SkillTexturesID
	{
		get { return skillTexturesID; }
		private set { skillTexturesID = value; }
	}
	public int[] PlayerCamerasID
	{
		get { return playerCamerasID; }
		private set { playerCamerasID = value; }
	}
	public int[] GUITexturesID
	{
		get { return guiTexturesID; }
		private set { guiTexturesID = value; }
	}
	public int[] MainMenuTexturesID
	{
		get { return mainMenuTexturesID; }
		private set { mainMenuTexturesID = value; }
	}
	public int[] MouseCursorTexturesID
	{
		get { return mouseCursorTexturesID; }
		private set { mouseCursorTexturesID = value; }
	}
	public int[] PlayerResourcesTexturesID
	{
		get { return playerResourcesTexturesID; }
		private set { playerResourcesTexturesID = value; }
	}
	public int[] MenuTexturesID
	{
		get { return menuTexturesID; }
		private set { menuTexturesID = value; }
	}
	#endregion
	#region Builder
	public override void Initialize()
	{
		IDManager.InitializeID(this.skillTextures, ref this.skillTexturesID);
		IDManager.InitializeID(this.playerCamerasTextures, ref this.playerCamerasID);
		IDManager.InitializeID(this.guiTextures, ref this.guiTexturesID);
		IDManager.InitializeID(this.mainMenuTextures, ref this.mainMenuTexturesID);
		IDManager.InitializeID(this.mouseCursorTextures, ref this.mouseCursorTexturesID);
		IDManager.InitializeID(this.playerResources, ref this.playerResourcesTexturesID);
		IDManager.InitializeID(this.menuTextures, ref this.menuTexturesID);
	}
	#endregion
	#region Functions
	private Texture2D GetTexture(string textureName, Texture2D[] textures, ref int[] ids)
	{
		int textureID = textureName.GetHashCode();

		for (int i = 0; i < ids.Length; i++)
			if (ids[i] == textureID)
				return textures[i];

		Debug.LogWarning(textureName + " Your texture was not find");
		return new Texture2D(0, 0, TextureFormat.ARGB32, false);
	}
	public Texture2D GetSkillTexture(string textureName)
	{
		return this.GetTexture(textureName, this.skillTextures, ref this.skillTexturesID);
	}
	public Texture2D GetPlayerCameraTexture(string textureName)
	{
		return this.GetTexture(textureName, this.PlayerCamerasTextures, ref this.playerCamerasID);
	}
	public Texture2D GetGUITexture(string textureName)
	{
		return this.GetTexture(textureName, this.guiTextures, ref this.guiTexturesID);
	}
	public Texture2D GetMainMenuTexture(string textureName)
	{
		return this.GetTexture(textureName, this.mainMenuTextures, ref this.mainMenuTexturesID);
	}
	public Texture2D GetMouseCursorexture(string textureName)
	{
		return this.GetTexture(textureName, this.mouseCursorTextures, ref this.mouseCursorTexturesID);
	}
	public Texture2D GetPlayerResourceTexture(string textureName)
	{
		return this.GetTexture(textureName, this.playerResources, ref this.playerResourcesTexturesID);
	}
	public Texture2D GetMenuTexture(string textureName)
	{
		return this.GetTexture(textureName, this.menuTextures, ref this.menuTexturesID);
	}
	#endregion
}
