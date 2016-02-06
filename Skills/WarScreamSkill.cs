using UnityEngine;
using System.Collections;

public class WarScreamSkill<TModuleType> : ASkillDamage<TModuleType> where TModuleType : APlayer
{
//	private float moveSpeed;
	private string targetTag;
	private GameObject warScream;

	public WarScreamSkill()
	{
		name = "War Scream";
		description.Description = "Emits wing that repulse violently all the enemies that it encounter";
		manaCost = 30;
		levelRequiered = 7;
		countdown = 1.8f;
		elementaryDamage = e_elementaryDamage.Wing;
		type = e_skillType.Simple;
		category = e_skillCategory.Power;
//		moveSpeed = 40f;
		icon = ServiceLocator.Instance.TextureManager.GetSkillTexture("fusrohdah");

		damage.Initialize((int)(80 + 40 * level.Current),
						  (int)(120 + 60 * level.Current));
	}

	public override void Effect(GameObject user, AEntityAttribute<TModuleType> playerAttri)
	{
		base.Effect(user, playerAttri);

		warScream = ServiceLocator.Instance.ObjectManager.Instantiate("WarScream", user.transform.position, user.transform.rotation);

		WarScream<TModuleType> warScreamScript = warScream.GetComponent<WarScream<TModuleType>>();

		warScreamScript.targetTag = targetTag;
		warScreamScript.damage = damage.RandomBetweenValues();
		warScreamScript.fastCast = 25.0f;
		warScreamScript.user = playerAttri;
	}

	public override void Update(GameObject user, AEntityAttribute<TModuleType> playerAttri)
	{
		base.Update(user, playerAttri);

		if (user.tag == "PlayerInfo")
			targetTag = "Enemy";
		else
			targetTag = "Player";
	}

	public override void LevelUp(GameObject user, AEntityAttribute<TModuleType> playerAttri)
	{
		base.LevelUp(user, playerAttri);

		if (learned)
		{
			if (level.Current % 5 == 0)
				manaCost -= 5;
		}

        damage.Initialize((int)(80 + 40 * level.Current * playerAttri.SkillEffectPercent),
						  (int)(120 + 60 * level.Current * playerAttri.SkillEffectPercent));
	}

	public override int GetManaCost(int level)	{
		return 60 - Mathf.CeilToInt(level / 5);
	}

	public override int GetMinDamage(int level, AEntityAttribute<TModuleType> playerAttri)
	{
		return (int)(80 + 40 * level * playerAttri.SkillEffectPercent);
	}

	public override int GetMaxDamage(int level, AEntityAttribute<TModuleType> playerAttri)
	{
		return (int)(120 + 60 * level * playerAttri.SkillEffectPercent);
	}

	public override void SetDescription(AEntityAttribute<TModuleType> playerAttri)
	{
		description.LevelInformation =
            GetDescription("Current Level", level.Current, playerAttri) +
			GetManaCostCountdown(manaCost, playerAttri) +
            GetDPSSPS(level.Current, playerAttri) +

            ((level.Current < level.Max) ?
            GetDescription("Next Level", level.Current + 1, playerAttri) +
            GetManaCostCountdown(GetManaCost(level.Current + 1), playerAttri) +
            GetDPSSPS(level.Current + 1, playerAttri) 
			: "") + "\n\n" +

            GetDescription("Last Level", level.Max, playerAttri) +
            GetManaCostCountdown(GetManaCost(level.Max), playerAttri) +
            GetDPSSPS(level.Max, playerAttri);
		}
	//damage update, mana cost au level up
}
