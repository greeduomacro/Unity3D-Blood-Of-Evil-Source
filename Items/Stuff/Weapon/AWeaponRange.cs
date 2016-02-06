using UnityEngine;
using System.Collections;

public abstract class AWeaponRange<TModuleType> : AWeapon<TModuleType> where TModuleType : APlayer
{
	GameObject bullet;

	public AWeaponRange()
	{
		this.weaponCategory = e_weaponCategory.Range;
		this.weight = 23;
		this.AddFixedAttribute(e_entityAttribute.Range, new Vector2(10, 15));
	}
}
