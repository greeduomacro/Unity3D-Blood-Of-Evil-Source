using UnityEngine;
using System.Collections;

public class WeaponTwoHandedScepter<TModuleType> : AWeaponTwoHandedMelee<TModuleType> where TModuleType : APlayer
{
	public WeaponTwoHandedScepter()
	{
		this.weaponType = e_weaponType.Scepter;
	}
}