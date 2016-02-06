using UnityEngine;
using System.Collections;

public class WeaponTwoHandedStaff<TModuleType> : AWeaponTwoHandedMelee<TModuleType> where TModuleType : APlayer
{
	public WeaponTwoHandedStaff()
	{
		this.weaponType = e_weaponType.Staff;
	}
}
