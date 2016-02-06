using UnityEngine;
using System.Collections;

public sealed class LifePotion<TModuleType> : APotion<TModuleType> where TModuleType : APlayer
{
	public LifePotion()
	{
		this.consommableType = e_consommableType.Life;

		this.name += "vie";
	}

	public override void Use(PlayerAttribute<TModuleType> user)
	{
		user.LifeCurrent += user.Life.Max * this.percentOfCare;
	}
}
