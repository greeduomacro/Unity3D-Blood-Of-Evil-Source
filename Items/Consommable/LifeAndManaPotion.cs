using UnityEngine;
using System.Collections;

public sealed class LifeAndManaPotion<TModuleType> : APotion<TModuleType> where TModuleType : APlayer
{
	public LifeAndManaPotion()
	{
		this.consommableType = e_consommableType.Life_And_Mana;
		this.percentOfCare = 0.2f;

		this.name += "vie et de mana";
	}

	public override void Use(PlayerAttribute<TModuleType> user)
	{
		user.LifeCurrent += user.Life.Max * this.percentOfCare;
		user.ManaCurrent += user.Mana.Max * this.percentOfCare;
	}
}
