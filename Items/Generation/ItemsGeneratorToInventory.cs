using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemsGeneratorToInventory<TModuleType> : MonoBehaviour where TModuleType : APlayer
{
	private Transform trans;
	private GenericTableOfLoot<TModuleType> genericTableOfLoot = new GenericTableOfLoot<TModuleType>();

	[SerializeField]	private int numberOfItemGenerated;
	[SerializeField]	private e_equipmentQuality qualityMinimum = e_equipmentQuality.Normal;
	[SerializeField]	private e_equipmentQuality qualityMaximum = e_equipmentQuality.God;
	[SerializeField]	private int levelRequieredMinimum = 1;
	[SerializeField]	private int levelRequieredMaximum = 6;
	[SerializeField]	private bool armorGenerate = false;
	[SerializeField]	private bool weaponGenerate = false;

	private Inventory<TModuleType> inventory;

	void Start()
	{
		this.trans = transform;
		APlayer player = gameObject.GetComponent<APlayer>();

		//A CORRIGERthis.Initialize(player.Items.Inventory, trans, 
		//A CORRIGER	player.Attributes,
		//A CORRIGER	player.Items.AttributeInitializer);
	}

	public void Initialize(Inventory<TModuleType> inventory, Transform transform, PlayerAttribute<TModuleType> playerAttri, AttributeInitializer<TModuleType> attributeInitializer)
	{
		this.genericTableOfLoot.InitializeAttributeInitializer(attributeInitializer);
		this.genericTableOfLoot.itemGenerator.InitializeGenerator(playerAttri);

		this.inventory = inventory;
		this.genericTableOfLoot.itemGenerator.Range = 5;
		this.trans = transform;

		this.SetGeneration();
		this.Generate();
		this.ItemsTableOfLootToInventory();
	}

	void SetGeneration()
	{
		float[] probabilities = new float[(int)e_stuffInstantiate.SIZE];
		if (this.armorGenerate)
			probabilities[((int)e_stuffInstantiate.ARMOR)] = 1;
		if (this.weaponGenerate)
			probabilities[((int)e_stuffInstantiate.WEAPON)] = 1;
		this.genericTableOfLoot.itemGenerator.StuffGenerator.ResetProbabilities(probabilities);

		
	}

	void Generate()
	{
		this.genericTableOfLoot.itemGenerator.Position = this.trans.position;
		this.genericTableOfLoot.itemGenerator.StuffGenerator.InstantiateType = e_itemInstantiateType.OnInterface;
		this.genericTableOfLoot.itemGenerator.InitializeStuffGenerator(new MinMaxf(this.numberOfItemGenerated, this.numberOfItemGenerated), this.levelRequieredMinimum, this.levelRequieredMaximum, this.qualityMinimum, this.qualityMaximum);
		this.genericTableOfLoot.itemGenerator.GenerateItems(this.genericTableOfLoot, this.genericTableOfLoot.attributeInitializer);
	}

	void ItemsTableOfLootToInventory()
	{
		foreach (AItem<TModuleType> item in this.genericTableOfLoot.Items)
			this.inventory.AddItem(item);
		this.genericTableOfLoot.Items.Clear();
	}
}