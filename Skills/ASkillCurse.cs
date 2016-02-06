using UnityEngine;
using System.Collections;

public abstract class ASkillCurse<TModuleType> : ASkillAttributeOnDuration<TModuleType> where TModuleType : APlayer
{
	public ASkillCurse()	{
		attributeImportance = -1f;
		category = e_skillCategory.Curse;
	}
}
