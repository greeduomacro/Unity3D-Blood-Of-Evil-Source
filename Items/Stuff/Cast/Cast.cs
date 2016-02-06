using UnityEngine;
using System.Collections;

public class Cast<TModuleType> : AStuff<TModuleType> where TModuleType : APlayer
{

	private Skill<TModuleType> skill;
	public Skill<TModuleType> Skill { get { return skill; } private set { skill = value; } }

	public Cast(string name, Skill<TModuleType> skill)
    {
		this.equipmentCategory = e_equipmentCategory.Cast;
		this.equipmentEmplacement = e_equipmentEmplacement.Left_Hand;
		this.mesh = "Cast";
        this.name = name;
        this.skill = skill;

		if (this.skill.category == e_skillCategory.Destruction)
			this.filtreSkill = SkillFilter.FiltreDestruction;
		else if (this.skill.category == e_skillCategory.Heal)
			this.filtreSkill = SkillFilter.FiltreHeal;
		else if (this.skill.category == e_skillCategory.Power)
			this.filtreSkill = SkillFilter.FiltrePower;

		filtre = ItemExtension.FiltreAll;
    }

	public void Action(GameObject user, AEntityAttribute<TModuleType> playerAttribute, ASkillManager<TModuleType> mgr)
    {
		if (this.equipped != e_equipmentEquipped.NOT_EQUIPPED && this.skill.CanUseSkill(playerAttribute))
        { 
            if (skill.category == e_skillCategory.Destruction)
                mgr.PlayASkill(user, playerAttribute, "Fireball");
            if (skill.category == e_skillCategory.Heal)
				mgr.PlayASkill(user, playerAttribute, "Simple Heal");
            if (skill.category == e_skillCategory.Power)
				mgr.PlayASkill(user, playerAttribute, "earScream");
        }
            //skill.Effect(user, playerAttribute);
    }
}
