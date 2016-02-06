using UnityEngine;
using System.Collections;

public abstract class ASkillPassif<TModuleType> : ASkillAttribute<TModuleType> where TModuleType : APlayer
{
	public ASkillPassif()	{
		category = e_skillCategory.Passive;
	}
}
