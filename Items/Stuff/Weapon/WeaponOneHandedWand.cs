using UnityEngine;
using System.Collections;

public class WeaponOneHandedWand<TModuleType> : AWeaponOneHandedMelee<TModuleType> where TModuleType : APlayer
{

	public WeaponOneHandedWand()
	{
		this.weaponType = e_weaponType.Wand;
	}
}
