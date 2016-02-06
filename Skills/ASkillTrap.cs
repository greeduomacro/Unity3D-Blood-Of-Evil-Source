using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class ASkillTrap<TModuleType> : Skill<TModuleType> where TModuleType : APlayer
{
	protected List<GameObject> traps;
	protected CurrentMaxi trapPlaced;

	public ASkillTrap()
	{
		this.trapPlaced = new CurrentMaxi(0, 0);
		category = e_skillCategory.Trap;
	}

	bool CanPlaceTrap()	{
		bool canPlaceTrap = trapPlaced.Current< trapPlaced.Max;

		if (canPlaceTrap)
			++trapPlaced.Current;

		return canPlaceTrap;
	}

	public override bool CanUseSkill(AEntityAttribute<TModuleType> playerAttri)
	{
		return trapPlaced.Current < trapPlaced.Max && base.CanUseSkill(playerAttri);
	}

	public override void Effect(GameObject user, AEntityAttribute<TModuleType> playerAttri)
	{
		base.Effect(user, playerAttri);
		++trapPlaced.Current;
	}

	public abstract int GetMaxTrapPlaced(int lvl, AEntityAttribute<TModuleType> playerAttri);

	//for each object create, they have to have a reference to our ASkilltrap
	//because when they will be destroy they will change the number of trap placed
	//c'est a dire que tout mes trap object devront avoir en parametre notre sort
	//dès qu'il sont détruit il feront --trapPlaced.max;
}
