using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemContainer<TModuleType> where TModuleType : APlayer
 {
	public List<AItem<TModuleType>> items;

	//e_whichItemCanItake whichItem;
	public int size;
	public float currentWeight;
	public float maxWeight;
	public e_itemContainer itemContainer;

	public ItemContainer()	{
		items = new List<AItem<TModuleType>>();
	}

	public bool CanAddItem(AItem<TModuleType> item)
	{
		bool canAddItem =  currentWeight + item.Weight <= maxWeight;

		if (!canAddItem)
			ServiceLocator.Instance.ErrorDisplayStack.Add("You can't add this item to your " + itemContainer.ToString() +
			 "because you are too heavy", e_errorDisplay.Error);

		return canAddItem;
	}

	public bool CanAddItem(AItem<TModuleType> item1, AItem<TModuleType> item2)
	{
		return currentWeight + item1.Weight + item2.Weight <= maxWeight;
	}

	virtual public bool AddItem(AItem<TModuleType> item)
	{
		bool HaveAddedItem = CanAddItem(item);

		if (HaveAddedItem)
		{
			items.Add(item);
			AddItemWeight(item);
		}

		return HaveAddedItem;
	}

	virtual public void RemoveItem(AItem<TModuleType> item)
	{
		RemoveItemWeight(item);
		items.Remove(item);
	}

	public void AddItemWeight(AItem<TModuleType> item)
	{
		currentWeight += item.Weight;
	}

	public void RemoveItemWeight(AItem<TModuleType> item)
	{
		currentWeight -= item.Weight;
	}
}

