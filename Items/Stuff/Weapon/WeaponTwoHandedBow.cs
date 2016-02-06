using UnityEngine;
using System.Collections;

public class WeaponTwoHandedBow<TModuleType> : AWeaponTwoHandedRange<TModuleType> where TModuleType : APlayer
{
	public WeaponTwoHandedBow()
	{
		this.weaponType = e_weaponType.Bow;
	}
}
