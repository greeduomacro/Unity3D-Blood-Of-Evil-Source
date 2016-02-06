using UnityEngine;
using System.Collections;

public class WeaponTwoHandedCrossbow<TModuleType> : AWeaponTwoHandedRange<TModuleType> where TModuleType : APlayer
{

	public WeaponTwoHandedCrossbow()
	{
		this.weaponType = e_weaponType.Crossbow;
	}
}
