using UnityEngine;
using System.Collections;

public abstract class AGUIWindowManager<TModuleType> : MonoBehaviour where TModuleType : MonoBehaviour
{
	#region Data Attributes
	protected AGUIWindow<TModuleType>[] windows;
	protected int maximumDisplayWindowsNumber;
	#endregion
	#region Properties
	public AGUIWindow<TModuleType>[] Windows
	{
		get { return windows; }
		private set { windows = value; }
	}
	public int MaximumDisplayWindowsNumber
	{
		get { return maximumDisplayWindowsNumber; }
		private set { maximumDisplayWindowsNumber = value; }
	}
	#endregion
	#region Abstract Functions
	public abstract void BindWindows();
	public abstract void CloseWindows(bool needToPressEvent = true);
	#endregion
	#region Unity Functions
	public virtual void OnGUI()
	{
		for (short i = 0; i < this.windows.Length; i++)
		{
			if (this.windows[i].IsActive)
			{
				GUILayout.BeginArea(MultiResolutions.Rectangle(this.windows[i].InitPosition));
				this.windows[i].DoMyWindow(i);
				GUILayout.EndArea();
			}
		}
	}
	#endregion
	#region Functions
	public void Initialization()
	{
		this.BindWindows();
		this.InitializationAfterBindage();
	}
	public void InitializeWindowModule(TModuleType moduleManager)
	{
		for (byte i = 0; i < this.windows.Length; i++)
			this.windows[i].ModuleManager = moduleManager;
	}
	public byte GetNumberOfActiveWindows()
	{
		byte numberofActiveWindow = 0;

		for (byte i = 0; i < this.windows.Length; i++)
			++numberofActiveWindow;

		return numberofActiveWindow;
	}
	public AGUIWindow<TModuleType> GetWindow(e_PlayerGUIWindow window)
	{
		return this.windows[(int)window];
	}

	private void InitializationAfterBindage()
	{
		for (short i = 0; i < this.windows.Length; i++)
		{
			this.windows[i].DragPosition = MultiResolutions.Rectangle(this.windows[i].InitPosition);

			if (this.windows[i].IsDraggable)
				this.windows[i].InitPosition = new Rect(0, 0, this.windows[i].InitPosition.width, this.windows[i].InitPosition.height);//.x = 0;				//this.windows[i].InitPosition.y = 0;

			if (!this.windows[i].IsClosable)
				this.windows[i].DragPosition = MultiResolutions.Rectangle(this.windows[i].GetInitialWindowPosition());
		}
	}

	public void OpenGUI(e_PlayerGUIWindow indexGUI)
	{
		int openedWindow = 0;

		for (short i = 0; i < ((int)e_PlayerGUIWindow.SIZE); i++)
			if (i != ((int)(e_PlayerGUIWindow.Main_Menu)) && this.windows[i].IsActive && this.windows[i].IsClosable)
				++openedWindow;

		if (openedWindow < maximumDisplayWindowsNumber || this.windows[(int)(indexGUI)].IsActive)
		{
			this.windows[(int)(indexGUI)].IsActive = !this.windows[(int)(indexGUI)].IsActive;

			if (this.windows[(int)(indexGUI)].IsActive)
				this.windows[(int)(indexGUI)].DragPosition = MultiResolutions.Rectangle(this.windows[(int)(indexGUI)].GetInitialWindowPosition());
		}
		else if (!this.windows[(int)(indexGUI)].IsClosable)
			this.windows[(int)(indexGUI)].IsActive = true;
		else
			ServiceLocator.Instance.ErrorDisplayStack.Add("You can't displays more than " + this.maximumDisplayWindowsNumber + " and main menu in the same time", e_errorDisplay.Critical);
	}
	#endregion
}