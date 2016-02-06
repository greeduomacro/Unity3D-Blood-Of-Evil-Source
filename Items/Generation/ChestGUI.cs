using UnityEngine;
using System.Collections;

public sealed class ChestGUI<TModuleType> : MonoBehaviour where TModuleType : APlayer
{
	#region Attributes
	private bool showGUI;
	private SimpleChest<TModuleType> chest;
	#endregion
	#region Properties
	public bool ShowGUI {	private get { return showGUI; }
							set { showGUI = value; } }
	public SimpleChest<TModuleType> Chest
	{
		get { return chest; }
								private set { chest = value; } } 
	#endregion

	void Awake()
	{
		chest = new SimpleChest<TModuleType>();
		chest.Initialize(transform);
	}

	void Update()
	{
		chest.Update();
	}
	//private AChest chest;

	//void Awake()
	//{
	//	chest = gameObject.GetComponent<AChest>();
	//}

	//void Update()
	//{
	//	//showGUI = chest.isOpen; //&& Vector3.Distance(player, Transform.position) < 5)	
	//}

	void OnGUI()
	{
		if (showGUI)
		{
			//draggable && closable
		}
	}
}