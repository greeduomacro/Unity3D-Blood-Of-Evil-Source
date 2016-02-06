using UnityEngine;
using System.Collections;

public class BarbarianAttribute<TModuleType> : PlayerAttribute<TModuleType> where TModuleType : APlayer
{
	public BarbarianAttribute()
	{
		base.attributes[(int)(e_entityAttribute.Strength)] = 5;
		base.attributes[(int)(e_entityAttribute.Resistance)] = 3;
		base.attributes[(int)(e_entityAttribute.Vitality)] = 4;
		base.attributes[(int)(e_entityAttribute.Energy)] = 2;

		//base.ModuleManager.Characteristics.SetAttributesWithCharacteristics();
		this.LifeAndManaToMax();
	}
	public override void LevelUp()
	{
		base.LevelUp();
		++base.attributes[(int)(e_entityAttribute.Strength)];
		++base.attributes[(int)(e_entityAttribute.Vitality)];
	}
}
