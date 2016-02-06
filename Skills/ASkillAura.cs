using UnityEngine;
using System.Collections;

public abstract class ASkillAura<TModuleType> : ASkillAttribute<TModuleType> where TModuleType : APlayer
{
    protected float range;

	public ASkillAura()	{
		category = e_skillCategory.Aura;
	}

//	public abstract float GetRange(int lvl, PlayerAttribute playerAttri);
	public override float GetAttribute(int lvl, AEntityAttribute<TModuleType> playerAttri, e_entityAttribute attri) { return 0f; }
}
