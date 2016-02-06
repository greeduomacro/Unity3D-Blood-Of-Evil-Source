using UnityEngine;
using System.Collections;

public class WeaponOneHandedSword<TModuleType> : AWeaponOneHandedMelee<TModuleType> where TModuleType : APlayer
{
	public WeaponOneHandedSword()
	{
		this.weaponType = e_weaponType.Sword;
		this.mesh = "Sword";
		this.filtre = ItemExtension.FiltreWeapon;
	}
}
