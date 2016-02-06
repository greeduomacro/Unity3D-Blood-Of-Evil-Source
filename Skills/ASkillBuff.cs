using UnityEngine;
using System.Collections;

public abstract class ASkillBuff<TModuleType> : ASkillAttributeOnDuration<TModuleType> where TModuleType : APlayer
{
	public ASkillBuff()	{
		attributeImportance = 1f;
		category = e_skillCategory.Buff;
	}
}
