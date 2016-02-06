using UnityEngine;
using System.Collections;

public class WeaponOneHandedMass<TModuleType> : AWeaponOneHandedMelee<TModuleType> where TModuleType : APlayer
{

	public WeaponOneHandedMass()
	{
		this.weaponType = e_weaponType.Mass;
	}
}
