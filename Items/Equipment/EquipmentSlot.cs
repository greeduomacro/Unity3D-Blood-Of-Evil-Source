using UnityEngine;
using System.Collections;

[System.Serializable]
public class EquipmentSlot<TModuleType> where TModuleType : APlayer
{
	#region Attributes
	private AStuff<TModuleType> item;
	private bool equipped;
	#endregion
	#region Properties
	public AStuff<TModuleType> Item
	{
		get { return item; }
							set { item = value; }  }
	public bool Equipped {	get { return equipped; }
							private set { equipped = value; } }
	#endregion
	void EquipInSlot()	{	 }
	public void EquipInSlot(AStuff<TModuleType> newItem)
	{
		equipped = true;
		switch (newItem.equipmentEmplacement)
		{
			case e_equipmentEmplacement.Left_Hand: newItem.equipped = e_equipmentEquipped.LEFT_HAND; break;
			case e_equipmentEmplacement.Right_Hand: newItem.equipped = e_equipmentEquipped.RIGHT_HAND; break;
			case e_equipmentEmplacement.Both_Hand: newItem.equipped = e_equipmentEquipped.BOTH_HAND; break;
			default: newItem.equipped = e_equipmentEquipped.EQUIPPED; break;
		}
		//item = new WeaponOneHandedSword()

		item = newItem;

	}

	public void UnequipInSlot(AStuff<TModuleType> newItem, Equipment<TModuleType> equipment, ItemManager<TModuleType> itemMgr)
	{
		equipped = false;
		item = null;
		if (newItem != null)
		{
			newItem.equipped = e_equipmentEquipped.NOT_EQUIPPED;
			equipment.RemoveItemWeight(newItem);
			itemMgr.AttributeInitializer.ModifyPlayerAttributesWithEquipment(newItem, -1);
		}
	}
}
