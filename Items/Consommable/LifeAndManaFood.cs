using UnityEngine;
using System.Collections;

public sealed class LifeAndManaFood<TModuleType> : AFood<TModuleType> where TModuleType : APlayer
{
	public LifeAndManaFood()
	{
		this.consommableType = e_consommableType.Life_And_Mana;
		this.percentOfCare = 0.75f;

		this.name += "vie et de mana";
	}
}