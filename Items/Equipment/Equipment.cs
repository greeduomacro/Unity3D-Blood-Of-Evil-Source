using UnityEngine;
using System.Collections;

[System.Serializable]
public sealed class Equipment<TModuleType> : AItemContainer<TModuleType> where TModuleType : APlayer
{
	private EquipmentSlot<TModuleType>[] equipmentSlots;

	#region Properties
	public EquipmentSlot<TModuleType>[] EquipmentSlots
	{
		get { return equipmentSlots; }
												private set { if (null != value) equipmentSlots = value; } }
	#endregion

	public Equipment()
	{
		this.size = (int)(e_equipmentEmplacement.SIZE);

		this.equipmentSlots = new EquipmentSlot<TModuleType>[size];
		for (short i =0; i < size; i++)
			this.equipmentSlots[i] = new EquipmentSlot<TModuleType>();

		this.maxWeight = 1000;
		this.itemContainer = e_itemContainer.Equipment;
	}

	public EquipmentSlot<TModuleType> GetEquipmentSlot(e_equipmentEmplacement emplacement)
	{
		return this.equipmentSlots[((int)emplacement)];
	}
}
