using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleChest<TModuleType> : AChest<TModuleType> where TModuleType : APlayer
{
	#region Attributes
	private Transform trans;
	#endregion
	#region Properties
	public Transform Trans {	get { return trans; }
								private set { trans = value; } }
	#endregion

	public void Initialize(Transform transform)
	{
		this.goldQuantity = new MinMaxf(1f, 1.5f);
		this.trans = transform;
		this.stuffGenerated.Initialize(1, 5);
		this.goldGenerated.Initialize(300, 450);
		this.consommableGenerated.Initialize(2, 3);
	}

	public void Update()
	{
		if (this.CanOpen())
		{	
			this.itemGenerator.Range = 2f;
			this.itemGenerator.Position = this.trans.position;

			//this.InitializeAttributeInitializer(GameObject.FindObjectOfType<ItemManager>().AttributeInitializer);
			this.itemGenerator.InitializeGenerator(this.entityAttribute);
			this.itemGenerator.InitializeStuffGenerator(this.stuffGenerated, this.entityAttribute.Level - 1, this.entityAttribute.Level + 1, e_equipmentQuality.Normal, e_equipmentQuality.God);
			this.itemGenerator.GenerateGold(this.goldQuantity, this.goldGenerated);
			this.itemGenerator.GenerateConsommable(this, this.consommableGenerated);

			this.Open();
		}
	}
}
