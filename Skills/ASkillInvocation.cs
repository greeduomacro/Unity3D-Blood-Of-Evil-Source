using UnityEngine;
using System.Collections;

public abstract class ASkillInvocation<TModuleType> : Skill<TModuleType> where TModuleType: APlayer
{
	protected GameObject invocation;
    protected MinMaxi invocationNumber;
    protected CurrentMaxf life;
    protected e_skillInvocationCategory invocationCategory;

	public ASkillInvocation()
	{
		this.life = new CurrentMaxf(0.0f, 0.0f);
		this.invocationNumber = new MinMaxi(0, 0);
		category = e_skillCategory.Invocation;
	}

	public string GetDescription(string whichLevel, int lvl, AEntityAttribute<TModuleType> playerAttri)
	{
		return base.GetDescription(whichLevel, lvl) +
			"<b><color=red>Invocation Maximum : </color></b> : " + GetMaxInvocationNumber(lvl, playerAttri).ToString() +
			"\n<b><color=red>life : </color></b> : " + GetMaxLife(lvl, playerAttri).ToString("F0");
	}

	public abstract int GetMaxInvocationNumber(int lvl, AEntityAttribute<TModuleType> playerAttri);
	public abstract int GetMaxLife(int lvl, AEntityAttribute<TModuleType> playerAttri);
}