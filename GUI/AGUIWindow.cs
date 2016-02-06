using UnityEngine;
using System.Collections;

public abstract class AGUIWindow<TModuleType> : AModule<TModuleType> where TModuleType : MonoBehaviour
{
	#region Attributes
	protected Rect[] rects;
	protected Rect initPosition;
	protected Rect dragPosition;
	protected Rect dragRect;
	protected Vector2 offset;
	protected string GUItitle;
	protected bool isActive;
	protected bool isDraggable;
	protected bool isClosable;
	protected bool activeFire;
	#endregion
	#region Properties
	public Rect[] Rects { get { return rects; } private set { rects = value; } }
	public Rect InitPosition { get { return initPosition; } set { initPosition = value; } }
	public Rect DragPosition { get { return dragPosition; } set { dragPosition = value; } }
	public Rect DragRect { get { return dragRect; } private set { dragRect = value; } }
	public Vector2 Offset { get { return offset; } private set { offset = value; } }
	public string GUITitle { get { return GUItitle; } private set { GUItitle = value; } }
	public bool IsActive { get { return isActive; } set { isActive = value; } }
	public bool IsDraggable { get { return isDraggable; } private set { isDraggable = value; } }
	public bool IsClosable { get { return isClosable; } private set { isClosable = value; } }
	public bool ActiveFire { get { return activeFire; } private set { activeFire = value; } }
	#endregion
	#region  Builder
	public AGUIWindow()
	{
		this.isClosable = true;
		this.activeFire = false;
	}
	public void GUIWindowInitialization(Rect initialPosition, bool windowDraggable = false, bool windowClosable = true)
	{
		this.initPosition.x = initialPosition.x;
		this.initPosition.y = initialPosition.y;

		if (initialPosition.width > 1 || this.initPosition.width < 0)
		{
			Debug.Log("Your width is too large or too small");
			this.initPosition.width = 1;
		}
		else
			this.initPosition.width = initialPosition.width;

		if (initialPosition.height > 1 || this.initPosition.height < 0)
		{
			Debug.Log("Your width is too large or too small");
			this.initPosition.height = 1;
		}
		else
			this.initPosition.height = initialPosition.height;

		if (windowDraggable)
			this.DraggableInitialization();

		this.isClosable = windowClosable;

		if (!this.isClosable)
			this.isActive = true;
	}
	#endregion
	#region Virtual Functions
	public virtual void OnGUIDrawWindow(int windowID)
	{
		this.dragRect = new Rect(0, 0, Screen.width, Screen.height);

		#region A enlever pour l'instant
		//if (isDraggable)
		//	GUI.DragWindow(dragRect);
		#endregion
	}
	public virtual void Interactions() { }
	public virtual void Initialize(GameObject gameObject) { }
	#endregion
	#region Functions
	public void SetGUITitle(string newTitle)
	{
		this.GUItitle = newTitle;
		GameObjectExtension.AddSubGameObjectsWithSeparator(base.ModuleManager.gameObject, "Scripts/GUI/" + newTitle, "/");
	}

	public void DoMyWindow(int windowID)
	{
		GUI.skin.window = null;
		this.OnGUIDrawWindow(windowID);
		//this.dragPosition = GUI.Window(windowID, this.dragPosition, this.OnGUIDrawWindow, "");

		if (this.dragPosition.x > Screen.width)
			this.dragPosition.x = Screen.width;
		else if (this.dragPosition.x < 0)
			this.dragPosition.x = 0;

		if (this.dragPosition.y > Screen.height)
			this.dragPosition.y = Screen.height;
		else if (this.dragPosition.y < 0)
			this.dragPosition.y = 0;
	}

	public void DrawWindow(int windowId)
	{
		
	}

	private void DraggableInitialization()
	{
		this.isDraggable = true;
		this.offset = new Vector2(this.initPosition.x, this.initPosition.y);
	}

	public Rect GetInitialWindowPosition()
	{
		if (this.isDraggable)
			return new Rect(this.initPosition.x + this.offset.x, this.initPosition.y + this.offset.y, this.initPosition.width, this.initPosition.height);
		return this.initPosition;
	}

	#endregion
}
