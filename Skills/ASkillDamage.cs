using UnityEngine;
using System.Collections;

public abstract class ASkillDamage<TModuleType> : Skill<TModuleType> where TModuleType : APlayer
{
	protected MinMaxi damage;
	protected e_elementaryDamage elementaryDamage;
	
	public ASkillDamage()
	{
		this.damage = new MinMaxi(0, 0);
		this.elementaryDamage = e_elementaryDamage.Physical;
		this.category = e_skillCategory.Damage;
	}

	public string GetDescription(string whichLevel, int lvl, AEntityAttribute<TModuleType> playerAttri)
	{
		return base.GetDescription(whichLevel, lvl) + 
			"<b><color=red>Damage type</color></b> : " + this.elementaryDamage.ToString() +
			"\n<b><color=red>Damage</color></b> : (" + GetMinDamage(lvl, playerAttri).ToString("F0") + "-" + GetMaxDamage(lvl, playerAttri).ToString("F0") + ")";
	}

	public string GetDPSSPS(int lvl, AEntityAttribute<TModuleType> playerAttri)
	{
		return "\n<b><color=red>DPS</color></b> : " + GetDPSString(lvl, playerAttri) +
			"\n<b><color=red>SPS</color></b> : " + GetSKSString(playerAttri) +
			"</size>";
	}

	public float GetDPSFloat(int lvl, AEntityAttribute<TModuleType> playerAttri)
	{
		return (GetMinDamage(lvl, playerAttri) + GetMinDamage(lvl, playerAttri)) * 0.5f * GetSKSFloat(playerAttri);
	}

	public string GetDPSString(int lvl, AEntityAttribute<TModuleType> playerAttri)
	{
		return GetDPSFloat(lvl, playerAttri).ToString("F2");
	}

	public abstract int GetMinDamage(int lvl, AEntityAttribute<TModuleType> playerAttri);
	public abstract int GetMaxDamage(int lvl, AEntityAttribute<TModuleType> playerAttri);
}
