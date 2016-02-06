using UnityEngine;
using System.Collections;

public sealed class LifeFood<TModuleType> : AFood<TModuleType> where TModuleType : APlayer
{
	public LifeFood()
	{
		this.consommableType = e_consommableType.Life;

		this.name += "vie";
	}
}
