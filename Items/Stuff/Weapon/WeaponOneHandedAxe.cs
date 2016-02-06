using UnityEngine;
using System.Collections;

public class WeaponOneHandedAxe<TModuleType> : AWeaponOneHandedMelee<TModuleType> where TModuleType : APlayer
{
	public WeaponOneHandedAxe()
	{
		this.weaponType = e_weaponType.Axe;
		
	}
}
