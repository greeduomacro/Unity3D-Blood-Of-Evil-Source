using UnityEngine;
using System.Collections;

public class EventsEnterAndTabPressed
{
	#region Attributs
	private bool enterPressed;
	private bool tabPressed;
	#endregion
	#region Propriétés
	public bool EnterPressed
	{
		get { return enterPressed; }
		private set { enterPressed = value; }
	}
	public bool TabPressed
	{
		get { return tabPressed; }
		private set { tabPressed = value; }
	}
	#endregion

	public void Update()
	{
		this.enterPressed = Input.GetKeyDown(KeyCode.Return);
		this.tabPressed = Input.GetKeyDown(KeyCode.Tab);
	}
}
