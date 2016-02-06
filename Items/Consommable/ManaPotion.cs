using UnityEngine;
using System.Collections;

public sealed class ManaPotion<TModuleType> : APotion<TModuleType> where TModuleType : APlayer
{
	public ManaPotion()
	{
		this.consommableType = e_consommableType.Mana;

		this.name += "mana";
	}

	public override void Use(PlayerAttribute<TModuleType> user)
	{
		user.ManaCurrent += user.Mana.Max * this.percentOfCare;
	}
}
