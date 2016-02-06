using UnityEngine;
using System.Collections;

public class WeaponTwoHandedSword<TModuleType> : AWeaponTwoHandedMelee<TModuleType> where TModuleType : APlayer
{
	public WeaponTwoHandedSword()
	{
		this.weaponType = e_weaponType.Sword;
		this.mesh = "LongSword";
	}
}
