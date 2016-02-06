using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenericTableOfLoot<TModuleType> : AInventory<TModuleType> where TModuleType : APlayer
{
	public ItemGenerator<TModuleType> itemGenerator;
	public AttributeInitializer<TModuleType> attributeInitializer;

	public List<AItem<TModuleType>> fixedStuff;
	//public List<AItem> fixedConsomable;
	//public List<AItem> fixedRaw;

	[HideInInspector]
	public AEntityAttribute<TModuleType> entityAttribute;
	
	public MinMaxf stuffGenerated;
	public MinMaxi goldGenerated;
	public MinMaxf consommableGenerated;

	protected MinMaxf goldQuantity;

	public void InitializeAttributeInitializer(AttributeInitializer<TModuleType> attri)
	{
		attributeInitializer = attri;
	}

	public GenericTableOfLoot()
	{
		itemGenerator = new ItemGenerator<TModuleType>();
		fixedStuff = new List<AItem<TModuleType>>();
		stuffGenerated = new MinMaxf();
		consommableGenerated = new MinMaxf();
		this.goldQuantity = new MinMaxf();
		goldGenerated = new MinMaxi();
		size = 50;
		maxWeight = 5000;
	}
}