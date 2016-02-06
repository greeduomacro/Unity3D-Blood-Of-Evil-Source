using UnityEngine;
using System.Collections;

public class BarbarianSkills<TModuleType> : ASkillManager<TModuleType> where TModuleType : APlayer
{
	public override void Initialize(GameObject user, AEntityAttribute<TModuleType> attributes)
	{
		FireballSkill<TModuleType> skillFireBall = new FireballSkill<TModuleType>();
		SimpleHealSkill<TModuleType> skillHeal = new SimpleHealSkill<TModuleType>();
		WarScreamSkill<TModuleType> skillScream = new WarScreamSkill<TModuleType>();

		// AREGLE PLUSTARD
		//skillFireBall.LevelUp(user, attributes);
		//skillHeal.LevelUp(user, attributes);
		//skillScream.LevelUp(user, attributes); ;

		//this.Add(skillFireBall);
		//this.Add(skillHeal);
		//this.Add(skillScream);
		//this.Add(new FearScreamSkill<TModuleType>());
	}
}
