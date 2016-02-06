using UnityEngine;
using System.Collections;

public class Skill<TModuleType> where TModuleType : APlayer
 {
    public e_skillCategory category { get; protected set; }
    public e_skillType type { get; protected set; }
    protected CurrentMaxi level;
    protected SkillDescription description;
    public string name { get; protected set; }
    protected float countdown;
    protected int manaCost;
    protected int grade;
    public int levelRequiered { get; protected set; }
    public bool learned { get; protected set; }
    public Texture2D icon { get; protected set; }
    public bool selected { get; set; }

	public Skill()	{
		grade = 1;
		//level.current = 1;
		learned = true;
		this.level = new CurrentMaxi(0, 20);
		description = new SkillDescription();
		icon = new Texture2D(0, 0);
	}


	virtual public void Effect(GameObject user, AEntityAttribute<TModuleType> playerAttri)
	{
		//if user.tag == player on get le player attribute sinon on get l'enemi attribute

		playerAttri.ManaCurrent -= manaCost;
		playerAttri.SkillTimer = 0;
	}

	virtual public void Update(GameObject user, AEntityAttribute<TModuleType> playerAttri) { }
	virtual public void SetDescription(AEntityAttribute<TModuleType> playerAttri) { }

	virtual public void LevelUp(GameObject user, AEntityAttribute<TModuleType> playerAttri)
	{
		//if user.tag == player on get le player attribute sinon on get l'enemi attribute

		if (CanLevelUp(playerAttri))
		{
			++level.Current;
			--playerAttri.SkillRemain;
			learned = true;
		}
	}

	public bool CanLevelUp(AEntityAttribute<TModuleType> playerAttri)
	{
		return level.Current < level.Max && playerAttri.SkillRemain > 0;
	}

	virtual public bool CanUseSkill(AEntityAttribute<TModuleType> playerAttri)
	{
		if (playerAttri.ManaCurrent < manaCost)
			ServiceLocator.Instance.ErrorDisplayStack.Add("You don't have enough mana", e_errorDisplay.Warning);

		if (type == e_skillType.Simple)
		{
			if (playerAttri.SkillTimerMultPercent < countdown)
				ServiceLocator.Instance.ErrorDisplayStack.Add("You can't use this skill now, you have to wait", e_errorDisplay.Warning);

			return playerAttri.ManaCurrent >= manaCost && playerAttri.SkillTimerMultPercent >= countdown;
		}
		else if (type == e_skillType.Anti_Cast)
		{
			if (playerAttri.SkillTimer < countdown)
				ServiceLocator.Instance.ErrorDisplayStack.Add("You can't use this skill now, you have to wait", e_errorDisplay.Warning);

			return playerAttri.ManaCurrent >= manaCost && playerAttri.SkillTimer >= countdown;
		}

		return false;
	}

	virtual public string GetDescription(string whichLevel, int level) {
		return MultiResolutions.Font(20) + "<b><color=#642EFE><i>" + whichLevel + "</i></color></b> (" + level + ")</size>\n" +
		MultiResolutions.Font(15);
	}

	public string GetManaCostCountdown(int mana, AEntityAttribute<TModuleType> playerAttri)
	{
		return "\n<b><color=red>Mana cost</color></b> : " + mana +
		"\n<b><color=red>Countdown</color></b> : " + GetFastCastString(playerAttri);
	}

	public float GetFastCastFloat(AEntityAttribute<TModuleType> playerAttri)
	{
		if (type == e_skillType.Simple)
			return (countdown / playerAttri.CastSpeedPercent);
		else if (type == e_skillType.Anti_Cast)
			return countdown;
		return 0;
	}

	public string GetFastCastString(AEntityAttribute<TModuleType> playerAttri)
	{
		return GetFastCastFloat(playerAttri).ToString("F2");
	}

	public float GetSKSFloat(AEntityAttribute<TModuleType> playerAttri)
	{
		if (type == e_skillType.Simple)
			return (1 / (countdown / playerAttri.CastSpeedPercent));
		else if (type == e_skillType.Anti_Cast)
			return (1 / countdown);

		return 0;
	}

	public string GetSKSString(AEntityAttribute<TModuleType> playerAttri)
	{
		return GetSKSFloat(playerAttri).ToString("F2");
	}

	virtual public int GetManaCost(int lvl)	{
		return manaCost;
	}

	public string GetSKS(AEntityAttribute<TModuleType> playerAttri)
	{
		return "\n<b><color=red>SPS</color></b> : " + GetSKSString(playerAttri) + "\n";
	}
}
