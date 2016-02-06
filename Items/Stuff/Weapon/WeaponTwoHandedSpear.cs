using UnityEngine;
using System.Collections;

public class WeaponTwoHandedSpear<TModuleType> : AWeaponTwoHandedRange<TModuleType> where TModuleType : APlayer
{
	public WeaponTwoHandedSpear()
	{
		this.weaponType = e_weaponType.Spear;
	}
}
