using UnityEngine;
using System.Collections;

public class FireballSkill<TModuleType> : ASkillDamage<TModuleType> where TModuleType : APlayer
{
//    private float moveSpeed;
	private string targetTag;
	private GameObject fireball;

	public FireballSkill()
	{
		name = "Fireball";
		description.Description = "Emits a large fireball dealing fire damage to your enemy";
		manaCost = 20;
		levelRequiered = 1;
		countdown = 0.7f;
		elementaryDamage = e_elementaryDamage.Fire;
		type = e_skillType.Simple;
        category = e_skillCategory.Destruction;
//		moveSpeed = 25f;
		icon = ServiceLocator.Instance.TextureManager.GetSkillTexture("fireball");

		//level.current = 0;		//already define on the ASkill constructor
		//Level.max = 20;			//already define on the ASkill constructor
		//grade = 1;				//already define on the ASkill constructor
		//learned = false;			//already define on the ASkill constructor
		//damage.min				//already define on the ASkill constructor
		//damage.max				//already define on the ASkill constructor
	}

	public override void Effect(GameObject user, AEntityAttribute<TModuleType> playerAttri)
	{
		base.Effect(user, playerAttri);

		fireball = ServiceLocator.Instance.ObjectManager.Instantiate("Fireball", user.transform.position, user.transform.rotation);

		Fireball<TModuleType> fireballScript = fireball.GetComponent<Fireball<TModuleType>>();

		fireballScript.targetTag = targetTag;
		fireballScript.damage = damage.RandomBetweenValues();
		fireballScript.moveSpeed = 25.0f;
		fireballScript.user = playerAttri;
	}

	public override void Update(GameObject user, AEntityAttribute<TModuleType> playerAttri)
	{
		base.Update(user, playerAttri);

		damage.Initialize((int)(30 * level.Current * 0.5f + playerAttri.Mana.Max * 0.1f * playerAttri.SkillEffectPercent),
						  (int)(45 * level.Current * 0.5f + playerAttri.Mana.Max * 0.12f * playerAttri.SkillEffectPercent));

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
				++grade;

            manaCost = GetManaCost(level.Current);
		}
	}

	public override int GetManaCost(int level)	{
		return  20 + ((6 - grade) * (level - 1));
	}

	public override int GetMinDamage(int lvl, AEntityAttribute<TModuleType> playerAttri)
	{
        return (int)(damage.Min - level.Current * 15f + lvl * 15f * playerAttri.SkillEffectPercent);
	}

	public override int GetMaxDamage(int lvl, AEntityAttribute<TModuleType> playerAttri)
	{
        return (int)(damage.Max - level.Current * 22.5f + lvl * 22.5f * playerAttri.SkillEffectPercent);
	}

	public override void SetDescription(AEntityAttribute<TModuleType> playerAttri)
	{
		description.LevelInformation =
        GetDescription("Current Level", level.Current, playerAttri) +
		GetManaCostCountdown(manaCost, playerAttri) +
        GetDPSSPS(level.Current, playerAttri) + "\n\n" +

        ((level.Current < level.Max) ?
        GetDescription("Next Level", level.Current + 1, playerAttri) +
        GetManaCostCountdown(GetManaCost(level.Current + 1), playerAttri) +
        GetDPSSPS(level.Current + 1, playerAttri) + "\n\n"
		: "") +

        GetDescription("Last Level", level.Max, playerAttri) +
        GetManaCostCountdown(GetManaCost(level.Max), playerAttri) +
        GetDPSSPS(level.Max, playerAttri); //+ "</size>";
	}
}
