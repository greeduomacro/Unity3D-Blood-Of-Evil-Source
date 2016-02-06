using UnityEngine;
using System.Collections;

public class SimpleHealSkill<TModuleType> : ASkillCare<TModuleType> where TModuleType : APlayer
{
	public SimpleHealSkill()
	{
		name = "Simple Heal";
		description.Description = "The user recover some health point every seconds";
		manaCost = 7;
		levelRequiered = 1;
		countdown = 5f;
		type = e_skillType.Simple;
        category = e_skillCategory.Heal;
		this.care = new MinMaxf(10, 15);
		icon = ServiceLocator.Instance.TextureManager.GetSkillTexture("simpleHeal");
	}

	public override void Effect(GameObject user, AEntityAttribute<TModuleType> playerAttri)
	{
		base.Effect(user, playerAttri);

		GameObject simpleCareObject = ServiceLocator.Instance.ObjectManager.Instantiate("SimpleHeal");

		SimpleHeal<TModuleType> simpleHeal = simpleCareObject.GetComponent<SimpleHeal<TModuleType>>();

        simpleHeal.Care = new MinMaxf(care.min, care.max);
		simpleHeal.UserAttribute = playerAttri;
	}

	public override void Update(GameObject user, AEntityAttribute<TModuleType> playerAttri)
	{
		base.Update(user, playerAttri);
	}

	public override void LevelUp(GameObject user, AEntityAttribute<TModuleType> playerAttri)
	{
		base.LevelUp(user, playerAttri);

		if (learned)
		{
			care.min = GetMinCare(level.Current, playerAttri);
            care.min = GetMaxCare(level.Current, playerAttri);
		}
	}

	public override int GetMinCare(int lvl, AEntityAttribute<TModuleType> playerAttri)
	{
		return (int)(10 + lvl * 7 * playerAttri.SkillEffectPercent);
	}

	public override int GetMaxCare(int lvl, AEntityAttribute<TModuleType> playerAttri)
	{
		return (int)(15 + lvl * 10 * playerAttri.SkillEffectPercent);
	}

	public override int GetManaCost(int lvl)	{
		return manaCost;
	}

	public override void SetDescription(AEntityAttribute<TModuleType> playerAttri)
	{
		description.LevelInformation =
        GetDescription("Current Level", level.Current, playerAttri) +
		GetManaCostCountdown(manaCost, playerAttri) +
		GetSKS(playerAttri) + "\n\n</size>" +

        ((level.Current < level.Max) ?
        GetDescription("Next Level", level.Current + 1, playerAttri) +
        GetManaCostCountdown(GetManaCost(level.Current + 1), playerAttri) +
		GetSKS(playerAttri) + "\n\n</size>"
		: "") +

        GetDescription("Last Level", level.Max, playerAttri) +
        GetManaCostCountdown(GetManaCost(level.Max), playerAttri) + GetSKS(playerAttri) + "</size>";
	}
	//care update, mana cost au level up
}
