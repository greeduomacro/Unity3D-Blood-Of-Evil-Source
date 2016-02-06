using UnityEngine;
using System.Collections;

public class FearScreamSkill<TModuleType> : ASkillDamage<TModuleType> where TModuleType : APlayer
{

    public FearScreamSkill()
    {
        name = "FearScream";
        description.Description = "Scare enemies for 30 seconds";
        manaCost = 20;
        levelRequiered = 1;
        countdown = 120f;
        type = e_skillType.Simple;
        category = e_skillCategory.Destruction;
    }

	public override void Effect(GameObject user, AEntityAttribute<TModuleType> playerAttri)
    {
        base.Effect(user, playerAttri);

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (var enemy in enemies)
        {
            if (Vector3.Distance(user.transform.position, enemy.transform.position) <= 15.0f)
            {
				//EnemyIA AI = enemy.GetComponent<EnemyIA>();
				//if (AI)
				//    AI.IsFeared = true;
            }
        }
    }

	public override int GetMinDamage(int lvl, AEntityAttribute<TModuleType> playerAttri)
    {
        return 0;
    }

	public override int GetMaxDamage(int lvl, AEntityAttribute<TModuleType> playerAttri)
    {
        return 0;
    }
}
