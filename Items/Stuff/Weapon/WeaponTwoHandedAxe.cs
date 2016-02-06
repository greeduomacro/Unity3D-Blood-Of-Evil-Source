using UnityEngine;
using System.Collections;

public class WeaponTwoHandedAxe<TModuleType> : AWeaponTwoHandedMelee<TModuleType> where TModuleType : APlayer
{

	public WeaponTwoHandedAxe()
	{
		this.weaponType = e_weaponType.Axe;
	}
}
