using UnityEngine;
using System.Collections;

public enum e_whichCamera
{
	MMORPG,
	Hack_And_Slash = 1,
	Minimap = 1,
	SIZE,
}

public class PlayerCameras<TModuleType> : AModule<TModuleType> where TModuleType : APlayer
{
	#region Attributes
	private Camera current;
	private Camera[] cameras;
	private e_whichCamera whichCamera;
	private SmoothFollow[] parameters;
	#endregion
	#region Properties
	public Camera Current { get { return current; } private set { current = value; } }
	public Camera[] Cameras { get { return cameras; } private set { cameras = value; } }
	public e_whichCamera WhichCamera { get { return whichCamera; } private set { whichCamera = value; } }
	public SmoothFollow[] Parameters { get { return parameters; } private set { parameters = value; } }
	#endregion
	#region Game Designer Attributes
	private Rect minimapViewPort;
	private float maxZoomHeight;
	private float maxDezoomHeight;
	private float zoomAndDezoomSpeed;
	private float minimapZoomSpeed;
	private float minMinimapHeight;
	private float maxMinimapHeight;
	private bool showTransparentMinimap;
	#endregion
	#region Game Designer Properties
	public Rect MinimapViewPort { get { return minimapViewPort; } private set { minimapViewPort = value; } }
	public float MaxZoomHeight { get { return maxZoomHeight; } private set { if (value >= 0) maxZoomHeight = value; } }
	public float MaxDezoomHeight { get { return maxDezoomHeight; } private set { if (value >= 0) maxDezoomHeight = value; } }
	public float ZoomAndDezoomSpeed { get { return zoomAndDezoomSpeed; } private set { if (value >= 0) zoomAndDezoomSpeed = value; } }
	public float MinimapZoomSpeed { get { return minimapZoomSpeed; } private set { if (value >= 0) minimapZoomSpeed = value; } }
	public float MinMinimapHeight { get { return minMinimapHeight; } private set { if (value >= 0) minMinimapHeight = value; } }
	public float MaxMinimapHeight { get { return maxMinimapHeight; } private set { if (value >= 0) maxMinimapHeight = value; } }
	public bool ShowTransparentMinimap { get { return showTransparentMinimap; } set { showTransparentMinimap = value; } }
	#endregion
	#region builder
	public PlayerCameras() { }
	public override void Update()	{ }
	public override void Initialize(TModuleType player)
	{
		this.Initialize(player, e_whichCamera.MMORPG);
	}
	public void Initialize(TModuleType player, e_whichCamera defaultCamera)
	{
		base.SetModule(player, "Scripts/Behaviour/Objects/Camera");

		GameObject cameraMMORPG = base.ModuleManager.ServiceLocator.ObjectManager.Instantiate("CameraMMORPG", "PlayerCameras", "CameraMMORPG");
		GameObject cameraMinimap = base.ModuleManager.ServiceLocator.ObjectManager.Instantiate("CameraPlayer", "PlayerCameras", "CameraMinimap");

		this.CamerasAndParametersAllocation(defaultCamera);
		this.CreateCamerasObjectAndInitializeTheirParameters(cameraMMORPG, cameraMinimap, base.ModuleManager.transform);
		this.InitializeMinimapObject(cameraMinimap);

		this.ChangeView();
		this.MiniMapView();

		current = this.cameras[(int)e_whichCamera.MMORPG];
	}

	private void CamerasAndParametersAllocation(e_whichCamera defaultCamera)
	{
		this.cameras = new Camera[(int)e_whichCamera.SIZE];
		this.parameters = new SmoothFollow[(int)e_whichCamera.SIZE];
		this.whichCamera = defaultCamera;
		this.showTransparentMinimap = true;
	}

	private void CreateCamerasObjectAndInitializeTheirParameters(GameObject cameraMMORPG, GameObject cameraMinimap, Transform trans)
	{
		this.cameras[(int)e_whichCamera.MMORPG] = cameraMMORPG.GetComponent<Camera>();
		this.cameras[(int)e_whichCamera.Minimap] = cameraMinimap.GetComponent<Camera>();

		this.parameters[(int)e_whichCamera.MMORPG] = cameraMMORPG.GetComponent<SmoothFollow>();
		this.parameters[(int)e_whichCamera.Minimap] = cameraMinimap.GetComponent<SmoothFollow>();

		this.parameters[(int)e_whichCamera.MMORPG].Target = trans;
		this.parameters[(int)e_whichCamera.Minimap].Target = trans;

		cameraMMORPG.name = "Camera MMORPG";
		cameraMinimap.name = "Camera Minimap";
	}

	private void InitializeMinimapObject(GameObject cameraMinimap)
	{
		this.DisableAudioListenerSourceAndGUILayer(cameraMinimap);

		this.minimapViewPort = new Rect(0.802f, 0.69f, 0.19f, 0.29f);//new Rect(0,0,1,1);
		this.maxZoomHeight = 1f;
		this.maxDezoomHeight = 20f;
		this.zoomAndDezoomSpeed = 3f;

		this.maxMinimapHeight = 200f;
		this.minMinimapHeight = 10f;
		this.minimapZoomSpeed = 1f;

		this.InitializeMinimapLayer();
	}

	private void DisableAudioListenerSourceAndGUILayer(GameObject obj)
	{
		obj.GetComponent<GUILayer>().enabled = false;
		//obj.GetComponent<LensFlare>().enabled = false;
		obj.GetComponent<AudioListener>().enabled = false;
		obj.GetComponent<AudioSource>().enabled = false;
	}

	private void InitializeMinimapLayer()
	{
		GameObject cameraMinimapLayer = base.ModuleManager.ServiceLocator.ObjectManager.Instantiate("CameraPlayer", "PlayerCameras", "CameraLayerMinimap"); 
		//GameObject.Instantiate(this.parameters[(int)e_whichCamera.Minimap].gameObject) as GameObject;
		Camera cam = cameraMinimapLayer.GetComponent<Camera>();

		cam.cullingMask = 1 << LayerMask.NameToLayer("Minimap");
		cam.rect = new Rect(0.802f, 0.69f, 0.19f, 0.29f);
		cam.depth = 100;
		cam.clearFlags = CameraClearFlags.Depth;

		this.DisableAudioListenerSourceAndGUILayer(cameraMinimapLayer);

		GameObject.Destroy(cameraMinimapLayer.GetComponent<SmoothFollow>());
		cameraMinimapLayer.transform.parent = this.parameters[(int)e_whichCamera.Minimap].transform;
		cameraMinimapLayer.transform.parent.position = this.parameters[(int)e_whichCamera.Minimap].transform.position;

		cameraMinimapLayer.name = "CameraMinimapLayer";
	}
	#endregion
	#region functions
	public void IncrementView()
	{
		if (this.whichCamera == e_whichCamera.MMORPG)
			this.whichCamera = e_whichCamera.Hack_And_Slash;
		else if (this.whichCamera == e_whichCamera.Hack_And_Slash)
			this.whichCamera = e_whichCamera.MMORPG;

		this.ChangeView();
	}

	public void ChangeView()
	{
		if (this.whichCamera == e_whichCamera.MMORPG)
			this.MMORPGView();
		else if (this.whichCamera == e_whichCamera.Hack_And_Slash)
			this.HackAndSlashView();
	}

	public void MMORPGView()
	{
		this.parameters[(int)e_whichCamera.MMORPG].Distance = 10f;
		this.parameters[(int)e_whichCamera.MMORPG].Height = 5f;
		this.parameters[(int)e_whichCamera.MMORPG].HeightDamping = 2f;
		this.parameters[(int)e_whichCamera.MMORPG].RotationDamping = 3f;
	}

	public void HackAndSlashView()
	{
		this.parameters[(int)e_whichCamera.MMORPG].gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
		this.parameters[(int)e_whichCamera.MMORPG].Distance = 7f;
		this.parameters[(int)e_whichCamera.MMORPG].Height = 13f;
		this.parameters[(int)e_whichCamera.MMORPG].HeightDamping = 2f;
		this.parameters[(int)e_whichCamera.MMORPG].RotationDamping = 0f;
	}

	public void MiniMapView()
	{
		this.cameras[(int)e_whichCamera.Minimap].rect = this.minimapViewPort;
		this.cameras[(int)e_whichCamera.Minimap].depth = 99f;

		this.parameters[(int)e_whichCamera.Minimap].transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
		this.parameters[(int)e_whichCamera.Minimap].Distance = 0f;
		this.parameters[(int)e_whichCamera.Minimap].Height = 100f;
		this.parameters[(int)e_whichCamera.Minimap].HeightDamping = 100f;
		this.parameters[(int)e_whichCamera.Minimap].RotationDamping = 0f;
		this.parameters[(int)e_whichCamera.Minimap].IsLookingTarget = false;
	}

	public void EnableAndDisableMinimap()
	{
		this.cameras[(int)e_whichCamera.Minimap].gameObject.SetActive(!this.cameras[(int)e_whichCamera.Minimap].gameObject.activeSelf);
		base.ModuleManager.WindowManager.OpenGUI(e_PlayerGUIWindow.Minimap);
	}

	public void ZoomAndDezoom(float mouseScrollWheel)
	{
		if (this.Parameters[(int)e_whichCamera.MMORPG].Height > this.maxZoomHeight && mouseScrollWheel < 0 ||
				this.Parameters[(int)e_whichCamera.MMORPG].Height < this.maxDezoomHeight && mouseScrollWheel > 0)
			this.Parameters[(int)e_whichCamera.MMORPG].Height += mouseScrollWheel * this.zoomAndDezoomSpeed;
	}

	public void RotateLeftMinimap()
	{
		this.RotateMinimap(2f);
	}

	public void RotateRightMinimap()
	{
		this.RotateMinimap(-2f);
	}

	private void RotateMinimap(float angle)
	{
		base.ModuleManager.ServiceLocator.RotateSubElementOfMinimap.RotateY += angle;
		Vector3 minimapEulerAngles = this.cameras[(int)e_whichCamera.Minimap].transform.eulerAngles;
		this.cameras[(int)e_whichCamera.Minimap].transform.eulerAngles = new Vector3(minimapEulerAngles.x, minimapEulerAngles.y + angle, minimapEulerAngles.z);
	}

	public void DezoomMinimap()
	{
		if (this.parameters[(int)e_whichCamera.Minimap].Height < this.maxMinimapHeight)
			this.parameters[(int)e_whichCamera.Minimap].Height += this.minimapZoomSpeed;
	}

	public void ZoomMinimap()
	{
		if (this.parameters[(int)e_whichCamera.Minimap].Height > this.minMinimapHeight)
			this.parameters[(int)e_whichCamera.Minimap].Height -= this.minimapZoomSpeed;
	}
	#endregion
}
