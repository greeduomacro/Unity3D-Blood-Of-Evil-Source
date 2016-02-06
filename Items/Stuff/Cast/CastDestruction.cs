using UnityEngine;
using System.Collections;

public sealed class CastDestruction<TModuleType> : ACast<TModuleType> where TModuleType : APlayer
{
	public CastDestruction()	{
		this.castCategory = e_castCategory.Destruction;
		this.manaCost = Random.Range(35, 42);
		this.weight = 13;
	}
}
