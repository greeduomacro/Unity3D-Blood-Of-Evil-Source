using UnityEngine;
using System.Collections;

public sealed class WeaponPhysical<TModuleType> : AWeapon<TModuleType> where TModuleType : APlayer
{
	public WeaponPhysical()
	{
		//weaponCategory = e_weaponCategory.CAC;
		this.weight = 25f;
		this.importanceOAttributefValue *= 1.3f;
	}
}
