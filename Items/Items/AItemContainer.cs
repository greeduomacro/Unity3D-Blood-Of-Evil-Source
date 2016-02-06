using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public abstract class AItemContainer<TModuleType> where TModuleType : APlayer
{
	#region Attributes
	protected List<AItem<TModuleType>> items;
	//e_whichItemCanItake whichItem;
	protected int size;
	protected float currentWeight;
	protected float maxWeight;
	protected e_itemContainer itemContainer;
	#endregion
	#region Properties
	public List<AItem<TModuleType>> Items
	{
		get { return items; }
								private set { items = value; } }
	public int Size {	get { return size; }
						private set { if (value >= 0) size = value; } }
	public float CurrentWeight {	get { return currentWeight; }
									private set { if (value >= 0) currentWeight = value; } }
	public float MaxWeight {	get { return maxWeight; }
								set { if (value >= 0) maxWeight = value; } }
	public e_itemContainer ItemContainer {	get { return itemContainer; }
											private set { itemContainer = value; } }
	#endregion

	public void SetMaxWeightWithStrength(float str)
	{
		maxWeight = 500 + (str * 5);
	}

	public AItemContainer()	{
		this.items = new List<AItem<TModuleType>>();
	}

	public bool CanAddItem(AItem<TModuleType> item)
	{
		bool canAddItem = this.currentWeight + item.Weight <= this.maxWeight;

		if (!canAddItem)
			ServiceLocator.Instance.ErrorDisplayStack.Add("You can't add this item to your " + itemContainer.ToString() +
			 "because you are too heavy", e_errorDisplay.Error);

		return canAddItem;
	}

	public bool CanAddItem(AItem<TModuleType> item1, AItem<TModuleType> item2)
	{
		return this.currentWeight + item1.Weight + item2.Weight <= this.maxWeight;
	}

	virtual public bool AddItem(AItem<TModuleType> item)
	{
		bool HaveAddedItem = this.CanAddItem(item);

		if (HaveAddedItem)
		{
			this.items.Add(item);
			this.AddItemWeight(item);
		}

		return HaveAddedItem;
	}

	virtual public void RemoveItem(AItem<TModuleType> item)
	{
		this.RemoveItemWeight(item);
		this.items.Remove(item);
	}

	public void AddItemWeight(AItem<TModuleType> item)
	{
		this.currentWeight += item.Weight;
	}

	public void RemoveItemWeight(AItem<TModuleType> item)
	{
		this.currentWeight -= item.Weight;
	}
}

