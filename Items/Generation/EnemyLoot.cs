using UnityEngine;
using System.Collections;

public class EnemyLoot<TModuleType> : GenericTableOfLoot<TModuleType> where TModuleType : APlayer
{
	#region Attributes
	private Transform trans;
	private EnemyAttribute<TModuleType> enemyAttribute;
	#endregion
	#region Properties
	public Transform Trans {	get { return trans; }
								private set { trans = value; } }
	public EnemyAttribute<TModuleType> EnemyAttribute
	{
		get { return enemyAttribute; }
											private set { enemyAttribute = value; } } 
	#endregion

	public EnemyLoot()
	{
		this.goldQuantity = new MinMaxf(0.5f, 1.0f);
		this.stuffGenerated.Initialize(0f, 1.2f);
		this.goldGenerated.Initialize(100, 130);
		this.consommableGenerated.Initialize(0.2f, 0.4f);
	}

	public void Initialize(EnemyAttribute<TModuleType> attri, Transform transform)
	{
		this.enemyAttribute = attri;
		this.trans = transform;
	}

	public void InstantiateItems()
	{
		this.itemGenerator.InitializeGenerator(this.enemyAttribute);

		this.itemGenerator.Range = 2;
		this.items.Clear();
		this.itemGenerator.Position = this.trans.position;

		//this.InitializeAttributeInitializer(GameObject.FindObjectOfType<APlayer>().Items.AttributeInitializer);
		this.itemGenerator.InitializeStuffGenerator(this.stuffGenerated, this.entityAttribute.Level - 1, this.entityAttribute.Level + 1, e_equipmentQuality.Normal, e_equipmentQuality.God);
		this.itemGenerator.GenerateGold(goldQuantity, this.goldGenerated);
		this.itemGenerator.GenerateItems(this, this.attributeInitializer);
		this.itemGenerator.GenerateConsommable(this, this.consommableGenerated);
	}
}
