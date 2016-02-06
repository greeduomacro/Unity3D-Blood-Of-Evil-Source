using UnityEngine;
using System.Collections;

public sealed class CastSkill<TModuleType> : ACast<TModuleType> where TModuleType : APlayer
{
	public CastSkill()	{
		this.castCategory = e_castCategory.Skill;
		this.manaCost = Random.Range(28, 33);
		this.weight = 8f;
	}
}
