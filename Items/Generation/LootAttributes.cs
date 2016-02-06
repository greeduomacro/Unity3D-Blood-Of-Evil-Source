using UnityEngine;
using System.Collections;

public sealed class LootAttributes
{
	#region Attributes
	private float itemQuantityPercent;
	private float itemRarityPercent;
	private float goldAmountPercent;
	private float goldQuantityPercent;
	private float goldRarityPercent;
	#endregion
	#region Properties
	public float ItemQuantityPercent {	get { return itemQuantityPercent; }
										private set { if (value >= 0) itemQuantityPercent = value; } }
	public float ItemRarityPercent {	get { return itemRarityPercent; }
										private set { if (value >= 0) itemRarityPercent = value; } }
	public float GoldQuantityPercent {	get { return goldQuantityPercent; }
										private set { if (value >= 0) goldQuantityPercent = value; } }
	public float GoldRarityPercent	{	get { return goldRarityPercent; }
										private set { if (value >= 0) goldRarityPercent = value; }	}
	public float GoldAmountPercent	{	get { return goldAmountPercent; }
										private set { if (value >= 0) goldAmountPercent = value; }	}
	#endregion


	public void Initialize(float itemQuantityPercent, float itemRarityPercent, float goldAmountPercent, float goldQuantityPercent, float goldRarityPercent)
	{
		this.itemQuantityPercent = itemQuantityPercent;
		this.itemRarityPercent = itemRarityPercent;
		this.goldAmountPercent = goldAmountPercent;
		this.goldQuantityPercent = goldQuantityPercent;
		this.goldRarityPercent = goldRarityPercent;
	}
}
