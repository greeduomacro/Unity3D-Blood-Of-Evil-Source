using UnityEngine;
using System.Collections;

public abstract class ASkillInvocationDamage<TModuleType> : ASkillInvocation<TModuleType> where TModuleType : APlayer
{
    protected MinMaxi damage;
    protected CurrentTimeTimer attackSpeed;

	public ASkillInvocationDamage()
	{
		this.damage = new MinMaxi();
		this.attackSpeed = new CurrentTimeTimer();
	}

	public override void Update(GameObject user, AEntityAttribute<TModuleType> playerAttri)
	{
		base.Update(user, playerAttri);
		attackSpeed.Update();
	}

	public override int GetMaxInvocationNumber(int lvl, AEntityAttribute<TModuleType> playerAttri)
	{
		return invocationNumber.Max;
	}

	public override int GetMaxLife(int lvl, AEntityAttribute<TModuleType> playerAttri)
	{
		return (int)(life.Max);
	}

	public abstract int GetMinDamage(int lvl, AEntityAttribute<TModuleType> playerAttri);
	public abstract int GetMaxDamage(int lvl, AEntityAttribute<TModuleType> playerAttri);
	public string GetDPS(int lvl, AEntityAttribute<TModuleType> playerAttri)
	{
		return ((GetMinDamage(lvl, playerAttri) + GetMaxDamage(lvl, playerAttri)) * 0.5f / attackSpeed.Timer).ToString("F2");
	}
}
