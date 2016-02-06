using UnityEngine;
using System.Collections;

public enum e_PlayerGUIWindow
{
	Minimap,
	Waypoint,
	Main_Menu,
	//Cursor, NE PAS LE METTRE DANS NOTRE MANAGEUR DE WINDOW CAR SINON ON NE POURRA PLUS GERER LA PROFONDEUR 
	Settings,
	Audio,
	Video,
	Language,
	Resources,
	New_Area,
	Open_Menus,
	SIZE,
}

public class PlayerGUIWindowManager : AGUIWindowManager<APlayer>
{
	#region Attributes
	private GameObject fireWhenGameIsPausing;
	private APlayer player;
	#endregion
	#region Properties
	public GameObject FireWhenGameIsPausing
	{
		get { return fireWhenGameIsPausing; }
		private set { fireWhenGameIsPausing = value; } 
	}
	public APlayer Player
	{
		get { return player; }
		private set { player = value; }
	}
	#endregion
	#region Builder
	public override void BindWindows()
	{
		this.windows = new AGUIWindow<APlayer>[(int)(e_PlayerGUIWindow.SIZE)];
		this.windows[(int)(e_PlayerGUIWindow.Minimap)] = new PlayerCamerasGUI<APlayer>();
		this.windows[(int)(e_PlayerGUIWindow.Main_Menu)] = new PlayerGUIMainMenu<APlayer>();
		this.windows[(int)(e_PlayerGUIWindow.Settings)] = new PlayerGUISettings<APlayer>();
		this.windows[(int)(e_PlayerGUIWindow.Language)] = new PlayerGUILanguage<APlayer>();
		this.windows[(int)(e_PlayerGUIWindow.Audio)] = new PlayerGUIAudio<APlayer>();
		this.windows[(int)(e_PlayerGUIWindow.Video)] = new PlayerGUIVideo<APlayer>();
		this.windows[(int)(e_PlayerGUIWindow.Waypoint)] = new PlayerGUIWaypoints<APlayer>();
		this.windows[(int)(e_PlayerGUIWindow.Resources)] = new PlayerGUIHealthManaXp<APlayer>();
		this.windows[(int)(e_PlayerGUIWindow.Open_Menus)] = new PlayerGUIOpenMenu<APlayer>();
		this.windows[(int)(e_PlayerGUIWindow.New_Area)] = new PlayerGUINewArea<APlayer>();

		this.Initialize();
	}
	#endregion
	#region Unity Functions
	public void InitializeByPlayer()
	{
		this.player = GetComponent<APlayer>();
		this.Initialization();

		this.fireWhenGameIsPausing = player.ServiceLocator.ObjectManager.Instantiate("FireWhenGameIsPausing", "Camera MMORPG", "FireWhenGameIsPausing");
		this.fireWhenGameIsPausing.transform.position = new Vector3(0, -1.334622f, 2.691565f);
		this.fireWhenGameIsPausing.transform.rotation = Quaternion.Euler(296.3243f, 180f, 180f);

		StartCoroutine(this.DisableFireForNSeconds(0.01f));
	}

	public override void OnGUI()
	{
		var oldMat = GUI.matrix;
		GUI.matrix = MultiResolutions.GetGUIMatrix();

		this.ShowBorderAndFlamesIfWindowActive();
		base.OnGUI();

		GUI.matrix = oldMat;
	}
	#endregion
	#region Override Functions

	public override void CloseWindows(bool needToPressEvent = true)
	{
		int closedWindow = 0;

		//dans un premier temps voir si une autre gui que main menu est active, si oui les fermer, autrement on inverse isActive de mainmenustate
		if (!needToPressEvent)
		{

			for (short i =0; i < ((int)e_PlayerGUIWindow.SIZE); i++)
			{
				if (i != ((int)(e_PlayerGUIWindow.Main_Menu)) && this.windows[i].IsActive && this.windows[i].IsClosable)
				{
					this.windows[i].IsActive = false;
					++closedWindow;
				}
			}

			if (0 == closedWindow)
			{
				this.windows[(int)(e_PlayerGUIWindow.Main_Menu)].IsActive = !this.windows[(int)(e_PlayerGUIWindow.Main_Menu)].IsActive;
				this.windows[(int)(e_PlayerGUIWindow.Main_Menu)].DragPosition = MultiResolutions.Rectangle(this.windows[(int)(e_PlayerGUIWindow.Main_Menu)].GetInitialWindowPosition());
			}
		}
	}
	#endregion
	#region Functions
	IEnumerator DisableFireForNSeconds(float time)
	{
		yield return new WaitForSeconds(time);

		for (short i =0; i < ((int)e_PlayerGUIWindow.SIZE); i++)
			if (this.windows[i].IsActive && this.windows[i].IsClosable)
				this.windows[i].IsActive = false;
	}

	private void Initialize()
	{
		base.windows[(int)(e_PlayerGUIWindow.Minimap)].IsActive = true;
		this.maximumDisplayWindowsNumber = 2;

		for (short i =0; i < this.windows.Length; i++)
			base.windows[i].Initialize(player);
	}

	private void ShowBorderAndFlamesIfWindowActive()
	{
		bool showBorderAndFlames = false;

		for (short i =0; i < ((int)e_PlayerGUIWindow.SIZE); i++)
			if (base.windows[i].IsActive && this.windows[i].IsClosable && this.windows[i].ActiveFire)
				showBorderAndFlames = true;

		this.fireWhenGameIsPausing.SetActive(showBorderAndFlames);

		//Time.timeScale = (showBorderAndFlames) ? 0 : 1;

		if (showBorderAndFlames)
			GUI.DrawTexture(MultiResolutions.Rectangle(0f, 0f, 1f, 1f), player.ServiceLocator.TextureManager.GetPlayerCameraTexture("borderMinimap"));
	}
	#endregion
}