using UnityEngine;
using System.Collections;

public abstract class ASkillCare<TModuleType> : Skill<TModuleType> where TModuleType : APlayer
{
	protected MinMaxf care;

	public ASkillCare()	{
		category = e_skillCategory.Care;
	}

	public string GetDescription(string whichLevel, int lvl, AEntityAttribute<TModuleType> playerAttri)
	{
		return base.GetDescription(whichLevel, lvl) +
			"<b><color=red>Care</color></b> : (" + GetMinCare(lvl, playerAttri).ToString("F0") + "-" + GetMaxCare(lvl, playerAttri).ToString("F0") + ")";
	}

	public abstract int GetMinCare(int lvl, AEntityAttribute<TModuleType> playerAttri);
	public abstract int GetMaxCare(int lvl, AEntityAttribute<TModuleType> playerAttri);
}
