using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class ASkillManager<TModuleType> : AModule<TModuleType> where TModuleType : APlayer
{
	#region Attributes
	protected SortedDictionary<e_skillCategory, SortedDictionary<string, Skill<TModuleType>>> skills;
	//protected AEntityAttribute playerAttribute;
	#endregion
	#region Properties
	public SortedDictionary<e_skillCategory, SortedDictionary<string, Skill<TModuleType>>> Skills
	{
		get { return skills; }
																				private set { skills = value; } }
	//public AEntityAttribute PlayerAttribute {	get { return playerAttribute; }
	//											private set { playerAttribute = value; } }
	#endregion

	public override void Initialize(TModuleType player)
	{
		base.SetModule(player, "Scripts/Behaviour/Skills");
		//AR REGLE PLUS TARDthis.Initialize(player.gameObject, player.Attributes);
	}

	public override void Update() { }//AR REGLE PLUS TARDthis.Update(Player.gameObject, base.ModuleManager.Attributes); }

	public abstract void Initialize(GameObject user, AEntityAttribute<TModuleType> attributes);

	public ASkillManager()	 {
		this.skills = new SortedDictionary<e_skillCategory, SortedDictionary<string, Skill<TModuleType>>>();

		for (short i =0; i < (int)e_skillCategory.SIZE; i++)
			this.skills[(e_skillCategory)i] = new SortedDictionary<string, Skill<TModuleType>>();
	}

	public void Update(GameObject gameObject, AEntityAttribute<TModuleType> entityAttribute)
	{
		foreach (KeyValuePair<e_skillCategory, SortedDictionary<string, Skill<TModuleType>>> skillsTmp in this.skills)
			foreach (KeyValuePair<string, Skill<TModuleType>> skill in skillsTmp.Value)
				if (skill.Value.learned)
					skill.Value.Update(gameObject, entityAttribute);
	}

	//a refaire en objet si je set la category ca change un attributcolor
	public string GetColorCategory(e_skillCategory category)
	{
		if (category == e_skillCategory.Aura) return "<color=yellow>";
		else if (category == e_skillCategory.Buff) return "<color=orange>";
		else if (category == e_skillCategory.Care) return "<color=#00FF00FF>";
		else if (category == e_skillCategory.Curse) return "<color=red>";
		else if (category == e_skillCategory.Damage) return "<color=#5555FFFF>";
		else if (category == e_skillCategory.Invocation) return "<color=purple>";
		else if (category == e_skillCategory.Passive) return "<color=green>";
		else if (category == e_skillCategory.Trap) return "<color=#FF5555FF>";
		else if (category == e_skillCategory.Skill_Item) return "<color=#FF7777>";

		return "";
	}

	public bool CanAdd(Skill<TModuleType> skill)
	{
		//if (playerAttribute.level < skill.levelRequiered)
		//    ServiceLocator.Instance.ErrorDisplayStack.Add("You don't have the level to learn the skill : " + skill.name, e_errorDisplay.Error);

		if (this.skills[skill.category].ContainsKey(skill.name))
			base.ModuleManager.ServiceLocator.ErrorDisplayStack.Add("You already learn the skill : " + skill.name, e_errorDisplay.Error);

		return !skills[skill.category].ContainsKey(skill.name); //&& playerAttribute.level >=skill.levelRequiered;
	}

	public bool Add(Skill<TModuleType> skill)
	{
		bool canAddSkill = this.CanAdd(skill);
		
		if (canAddSkill)
			this.skills[skill.category].Add(skill.name, skill);

		return canAddSkill;
	}

	public void Remove(Skill<TModuleType> skill)
	{
		this.skills[skill.category].Remove(skill.name);
	}

	public Skill<TModuleType> Get(string skillName)
	{
		foreach (KeyValuePair<e_skillCategory, SortedDictionary<string, Skill<TModuleType>>> skillsTmp in this.skills)
			foreach (KeyValuePair<string, Skill<TModuleType>> skill in skillsTmp.Value)
				if (skill.Key == skillName)
					return skill.Value;

		return null;
	}

	public void Select(Skill<TModuleType> skillParam)
	{
		foreach (KeyValuePair<e_skillCategory, SortedDictionary<string, Skill<TModuleType>>> skillsTmp in this.skills)
			foreach (KeyValuePair<string, Skill<TModuleType>> skill in skillsTmp.Value)
				skill.Value.selected = false;

		skillParam.selected = true;
	}

	public Skill<TModuleType> Get(e_skillCategory category, string skillName)
	{
		if (this.skills[category].ContainsKey(skillName))
			return this.skills[category][skillName];

		return null;
	}

	public void PlayASkill(GameObject user, AEntityAttribute<TModuleType> entityAttribute, string skillName)	{
		Skill<TModuleType> skill = this.Get(skillName);

		if (null != skill)
			if (skill.learned)
				if (skill.CanUseSkill(entityAttribute))
					skill.Effect(user, entityAttribute);
	}

	public int GetMaxLevelRequiered()	{
		int maxLength = 0;

		foreach (KeyValuePair<e_skillCategory, SortedDictionary<string, Skill<TModuleType>>> skillsTmp in skills)
			foreach (KeyValuePair<string, Skill<TModuleType>> skill in skillsTmp.Value)
				if (skill.Value.levelRequiered > maxLength)
					maxLength = skill.Value.levelRequiered;

		return maxLength;	
	}
}
