using UnityEngine;
using System.Collections;

public sealed class ConsommableGenerator<TModuleType> : AItemGenerator<TModuleType> where TModuleType : APlayer
{
	public ConsommableGenerator()
	{
		this.probabilities = new float[((int)e_consommableProbability.SIZE)];

		this.probabilities[((int)e_consommableProbability.Life_Potion)] = 1;
		this.probabilities[((int)e_consommableProbability.Life_Food)] = 1;
		this.probabilities[((int)e_consommableProbability.Mana_Food)] = 1;
		this.probabilities[((int)e_consommableProbability.Mana_Potion)] = 1;
		this.probabilities[((int)e_consommableProbability.Life_And_Mana_Potion)] = 0.5f;
		this.probabilities[((int)e_consommableProbability.Life_And_Mana_Food)] = 0.5f;
	}

	public void GenerateConsommable(AItemContainer<TModuleType> itemContainer)
	{
		e_consommableProbability whichConsommable = (e_consommableProbability)ServiceLocator.Instance.ProbabilityManager.GetProbabilityIndex(this.probabilities);

		itemContainer.AddItem(this.GenerateConsommable(whichConsommable));
	}

	AConsommable<TModuleType> GenerateConsommable(e_consommableProbability consommable)
	{
		AConsommable<TModuleType> consommableGenerated = null;

		switch (consommable)
		{
			case e_consommableProbability.Life_Potion: consommableGenerated = new LifePotion<TModuleType>(); break;
			case e_consommableProbability.Mana_Potion: consommableGenerated = new ManaPotion<TModuleType>(); break;
			case e_consommableProbability.Life_Food: consommableGenerated = new LifeFood<TModuleType>(); break;
			case e_consommableProbability.Mana_Food: consommableGenerated = new ManaFood<TModuleType>(); break;
			case e_consommableProbability.Life_And_Mana_Food: consommableGenerated = new LifeAndManaFood<TModuleType>(); break;
			case e_consommableProbability.Life_And_Mana_Potion: consommableGenerated = new LifeAndManaPotion<TModuleType>(); break;
			default: break;
		}

		return consommableGenerated;
	}
}
