using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class ASkillAttribute<TModuleType> : Skill<TModuleType> where TModuleType : APlayer
{
	protected List<EquipmentAttribute> fixedAttributes;
	protected List<EquipmentPossibleAttribute> fixedGeneratorAttributes;
	protected float attributeImportance;

	public ASkillAttribute()	{
		fixedAttributes = new List<EquipmentAttribute>();
		fixedGeneratorAttributes = new List<EquipmentPossibleAttribute>();
	}

	public string GetDescription(string whichLevel, int lvl, AEntityAttribute<TModuleType> playerAttri)
	{
		string result = base.GetDescription(whichLevel, lvl);

		foreach (var attri in fixedAttributes)
			result += "<b><color=red>" + attri.WhichAttribute.ToString().Replace("_", "") + "</color></b> : " + GetAttribute(lvl, playerAttri, attri.WhichAttribute).ToString("F2");

		return result;
	}

	public abstract float GetAttribute(int lvl, AEntityAttribute<TModuleType> playerAttri, e_entityAttribute attri);
	public abstract float GetImportanceValue(int lvl, AEntityAttribute<TModuleType> playerAttri);
}
