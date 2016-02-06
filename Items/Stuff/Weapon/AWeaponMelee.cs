using UnityEngine;
using System.Collections;

public abstract class AWeaponMelee<TModuleType> : AWeapon<TModuleType> where TModuleType : APlayer
{
	public AWeaponMelee()
	{
		this.weight = 25f;
		this.importanceOAttributefValue *= 1.3f;
		this.weaponCategory = e_weaponCategory.Melee;
	}
}
