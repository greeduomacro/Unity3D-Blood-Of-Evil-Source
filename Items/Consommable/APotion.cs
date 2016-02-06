using UnityEngine;
using System.Collections;

public abstract class APotion<TModuleType> : AConsommable<TModuleType> where TModuleType : APlayer
{
	public APotion()
	{
		this.consommableCategory = e_consommableCategory.Potion;
		this.timeRequieredToUseIt = 5f;
		this.percentOfCare = 0.3f;

		this.weight = 2f;
		this.name = "Potion de ";
		this.description = "Vous soigne instantanément";
		this.price = 50;
	}
}
