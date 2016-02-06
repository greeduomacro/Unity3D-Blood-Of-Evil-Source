using UnityEngine;
using System.Collections;

public sealed class CastHeal<TModuleType> : ACast<TModuleType> where TModuleType : APlayer
{
	public CastHeal()	{
		this.castCategory = e_castCategory.Heal;
		this.manaCost = Random.Range(15, 25);
		this.weight = 3;
	}
}
