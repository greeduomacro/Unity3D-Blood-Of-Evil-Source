using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public sealed class ItemGenerator<TModuleType> where TModuleType : APlayer
{
	#region Attributes
	private StuffGenerator<TModuleType> stuffGenerator;
	private GoldGenerator<TModuleType> goldGenerator;
	private ConsommableGenerator<TModuleType> consommableGenerator;
	private LootAttributes lootAttribute;
	private Vector3 position;
	private Vector3 offset;
	private float range;
	#endregion
	#region Properties
	public StuffGenerator<TModuleType> StuffGenerator
	{
		get { return stuffGenerator; }
											private set { stuffGenerator = value; } }
	public GoldGenerator<TModuleType> GoldGenerator
	{
		get { return goldGenerator; }
											private set { goldGenerator = value; } }
	public ConsommableGenerator<TModuleType> ConssommableGenerator
	{
		get { return consommableGenerator; }
														private set { consommableGenerator = value; } }
	public LootAttributes LootAttribute {	get { return lootAttribute; }
											private set { lootAttribute = value; } }
	public Vector3 Position {	get { return position; }
								set { position = value; } }
	public Vector3 Offset {	get { return offset; }
							private set { offset = value; } }
	public float Range {	get { return range; }
							set { if (value >= 0) range = value; } }
	#endregion


	public ItemGenerator()
	{
		this.stuffGenerator = new StuffGenerator<TModuleType>();
		this.goldGenerator = new GoldGenerator<TModuleType>();
		this.consommableGenerator = new ConsommableGenerator<TModuleType>();
		this.lootAttribute = new LootAttributes();
		this.offset = Vector3.up;
	}

	public void InitializeStuffGenerator(MinMaxf itemNumber, int minLevelRequiered, int maxLevelRequiered, e_equipmentQuality minQuality, e_equipmentQuality maxQuality)
	{
		this.stuffGenerator.LevelRequired.Initialize(minLevelRequiered, maxLevelRequiered);
		this.stuffGenerator.InitializeMinMaxQuality(minQuality, maxQuality);
		this.stuffGenerator.NumberOfItemGenerated.Initialize(itemNumber.min * this.lootAttribute.ItemQuantityPercent, itemNumber.max * this.lootAttribute.ItemQuantityPercent);

		this.stuffGenerator.ItemRarity = this.lootAttribute.ItemRarityPercent;
	}

	public void GenerateItems(AInventory<TModuleType> itemContainer, AttributeInitializer<TModuleType> attributeInitializer, List<AItem<TModuleType>> fixedStuff = null)//rajouter la position du coffre en parametre
	{
		this.stuffGenerator.AddFixedItems(itemContainer, fixedStuff);
		this.stuffGenerator.GenerateRandomItems(itemContainer);
		if (itemContainer.Items.Count > 0)
			this.stuffGenerator.GenerateAttributes(itemContainer, attributeInitializer);

		//Debug.Log("I generated " + itemContainer.items.Count + " items")
		this.ThrowAndClearItemsListIfOnGround(itemContainer);
	}

	private void ThrowAndClearItemsListIfOnGround(AInventory<TModuleType> itemContainer)
	{
		if (itemContainer.Items.Count > 0)
		{
			if (this.stuffGenerator.InstantiateType == e_itemInstantiateType.OnGround)
				foreach (AItem<TModuleType> item in itemContainer.Items)
					this.ThrowItem(item);//rajouter une position random a instancier

			if (this.stuffGenerator.InstantiateType == e_itemInstantiateType.OnGround)
				itemContainer.Items.Clear();
		}
	}

	public void RecuperateAllItems()
	{
		//recuperer litem manager du player qui laura touche
		//lui rajouter chaque objet a la main puis le remove
		//on rajoute les objets que lon peut add simplement
		//sinon on sarete de lappeler
	}

	public void ThrowItem(AItem<TModuleType> item)
	{
		GameObject mesh = item.GetMesh();
		GameObject droppedItem = null;

		if (mesh == null)
			droppedItem = GameObject.CreatePrimitive(PrimitiveType.Cube);
		else
		{
			droppedItem = GameObject.Instantiate(mesh) as GameObject;
			droppedItem.AddComponent<BoxCollider>();
		}

		droppedItem.layer = 0;
		foreach (Transform child in droppedItem.transform)
			child.gameObject.layer = 0;

		droppedItem.transform.Rotate(0, 0, 90);
		droppedItem.AddComponent<Rigidbody>();
		droppedItem.transform.position = position + offset + (new Vector3(Random.insideUnitCircle.x, 0, Random.insideUnitCircle.y) * this.range);
		droppedItem.AddComponent<CollectItemFromGround>();
		//A CORRIGERdroppedItem.GetComponent<CollectItemFromGround>().Item = item;
		droppedItem.tag = "Stuff";
	}

	public void GenerateStuffAttributes(AInventory<TModuleType> aInventory, AttributeInitializer<TModuleType> attributeInitializer)
	{
		this.stuffGenerator.GenerateAttributes(aInventory, attributeInitializer);
	}

	public void GenerateGold(MinMaxf goldQuantity, MinMaxi goldAmount)
	{
		float numberOfItemGenerate = MathExtension.GenerateRandomIntBetweenFloat(new MinMaxf(goldQuantity.min * this.lootAttribute.GoldQuantityPercent, goldQuantity.max * lootAttribute.GoldQuantityPercent));

		for (byte goldIndex = 0; goldIndex < numberOfItemGenerate; goldIndex++)
		{
			this.goldGenerator.GenerateGoldRarityAndAmount(new MinMaxi((int)(goldAmount.Min * lootAttribute.GoldAmountPercent), 
																  (int)(goldAmount.Max * lootAttribute.GoldAmountPercent)), 
																  this.lootAttribute.GoldRarityPercent);

			GameObject goldItem = goldGenerator.GenerateGold();
			
			goldItem.transform.position = position + (new Vector3(Random.insideUnitCircle.x, 0, Random.insideUnitCircle.y) * this.range);
		}
	}

	public void GenerateConsommable(AInventory<TModuleType> itemContainer, MinMaxf consommableQuantity)
	{
		int consommableGet = MathExtension.GenerateRandomIntBetweenFloat(new MinMaxf(consommableQuantity.min * this.lootAttribute.ItemQuantityPercent, consommableQuantity.max * lootAttribute.ItemQuantityPercent));

		for (byte i = 0; i < consommableGet; i++)
			this.consommableGenerator.GenerateConsommable(itemContainer);

		this.ThrowAndClearItemsListIfOnGround(itemContainer);
	}

	public void InitializeGenerator(AEntityAttribute<TModuleType> entityAttribute)
	{
		float itemQuantityPercent = entityAttribute.attributes[(int)e_entityAttribute.Item_Quantity_Percent] * 0.01f;
		float itemRarityPercent = entityAttribute.attributes[(int)e_entityAttribute.Item_Rarity_Percent] * 0.01f;
		float goldAmountPercent = entityAttribute.attributes[(int)e_entityAttribute.Gold_Amount_Percent] * 0.01f;
		float goldQuantityPercent = entityAttribute.attributes[(int)e_entityAttribute.Gold_Quantity_Percent] * 0.01f;
		float goldRarityPercent = entityAttribute.attributes[(int)e_entityAttribute.Gold_Rarity_Percent] * 0.01f;

		this.lootAttribute.Initialize(itemQuantityPercent, itemRarityPercent, goldAmountPercent, goldQuantityPercent, goldRarityPercent);
	}

	public void InitializeGenerator(EnemyAttribute<TModuleType> enemyAttribute)
	{
		float itemQuantityPercent = enemyAttribute.TargetAttribute.attributes[(int)e_entityAttribute.Item_Quantity_Percent] * enemyAttribute.attributes[(int)e_entityAttribute.Item_Quantity_Percent] * 0.0001f;
		float itemRarityPercent = enemyAttribute.TargetAttribute.attributes[(int)e_entityAttribute.Item_Rarity_Percent] * enemyAttribute.attributes[(int)e_entityAttribute.Item_Rarity_Percent] * 0.0001f;
		float goldAmountPercent = enemyAttribute.TargetAttribute.attributes[(int)e_entityAttribute.Gold_Amount_Percent] * enemyAttribute.attributes[(int)e_entityAttribute.Gold_Amount_Percent] * 0.0001f;
		float goldQuantityPercent = enemyAttribute.TargetAttribute.attributes[(int)e_entityAttribute.Gold_Quantity_Percent] * enemyAttribute.attributes[(int)e_entityAttribute.Gold_Quantity_Percent] * 0.0001f;
		float goldRarityPercent = enemyAttribute.TargetAttribute.attributes[(int)e_entityAttribute.Gold_Rarity_Percent] * enemyAttribute.attributes[(int)e_entityAttribute.Gold_Rarity_Percent] * 0.0001f;

		this.lootAttribute.Initialize(itemQuantityPercent, itemRarityPercent, goldAmountPercent, goldQuantityPercent, goldRarityPercent);
	}
}

