using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AChest<TModuleType> : GenericTableOfLoot<TModuleType> where TModuleType : APlayer
{
	[SerializeField]
	private bool isOpen;
	private bool hasBeenOpen;

	#region Properties
	public bool IsOpen {	get { return isOpen; }
							set { isOpen = value; } }
	public bool HasBeenOpen {	get { return hasBeenOpen; }
								private set { hasBeenOpen = value; } }
	#endregion

	public bool CanOpen()
	{
		return isOpen && !hasBeenOpen;
	}

	public void Open()
	{
		this.isOpen = true;
		this.hasBeenOpen = true;
		this.items.Clear();
		this.itemGenerator.GenerateItems(this, this.attributeInitializer, this.fixedStuff);
	}
}