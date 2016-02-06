using UnityEngine;
using System.Collections;

public class WeaponTwoHandedLance<TModuleType> : AWeaponTwoHandedMelee<TModuleType> where TModuleType : APlayer
{
	public WeaponTwoHandedLance()
	{
		this.weaponType = e_weaponType.Lance;
	}
}
