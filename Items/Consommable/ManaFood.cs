using UnityEngine;
using System.Collections;

public sealed class ManaFood<TModuleType> : AFood<TModuleType> where TModuleType : APlayer
{
	public ManaFood()
	{
		this.consommableType = e_consommableType.Mana;

		this.name += "mana";
	}
}
