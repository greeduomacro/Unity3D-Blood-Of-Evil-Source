using UnityEngine;
using System.Collections;

public sealed class GoldGenerator<TModuleType> : AItemGenerator<TModuleType> where TModuleType : APlayer
{
	public string name;
	public float amount;

	public void GenerateGoldRarityAndAmount(MinMaxi goldAmount, float goldRarity)
	{
		this.name = "Gold Coin";
		this.amount = goldAmount.RandomBetweenValues();
		if (goldRarity > Random.Range(0f, ((int)Mathf.Pow(3, 1))))
		{
			this.amount *= Random.Range(1.5f, 2.5f);
			this.name = "Gold Purse";
		}
		if (goldRarity > Random.Range(0f, ((int)Mathf.Pow(3, 2))))
		{
			this.amount *= Random.Range(1.5f, 2.5f);
			this.name = "Gold Chest";
		}
	}

	public GameObject GenerateGold()
	{
		GameObject goldItem = ServiceLocator.Instance.ObjectManager.Instantiate(name);

		goldItem.AddComponent<Gold>().Initialize((int)amount, name);
		goldItem.tag = "Gold";

		return goldItem;
	}
}
