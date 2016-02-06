using UnityEngine;
using System.Collections;

public class WeaponOneHandedScepter<TModuleType> : AWeaponOneHandedMelee<TModuleType> where TModuleType : APlayer
{
	public WeaponOneHandedScepter()
	{
		this.weaponType = e_weaponType.Scepter;
	}
}
