using UnityEngine;
using System.Collections;

public class WeaponTwoHandedMass<TModuleType> : AWeaponTwoHandedMelee<TModuleType> where TModuleType : APlayer
{
	public WeaponTwoHandedMass()
	{
		this.weaponType = e_weaponType.Mass;
	}
}
