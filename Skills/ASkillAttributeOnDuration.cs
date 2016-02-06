using UnityEngine;
using System.Collections;

public abstract class ASkillAttributeOnDuration<TModuleType> : ASkillAttribute<TModuleType> where TModuleType : APlayer
{
	protected float timeEffect;
    protected bool effectIsActive;

	public abstract int GetTimeEffect(int lvl, AEntityAttribute<TModuleType> playerAttri, e_entityAttribute attri);
}
