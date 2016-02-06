using UnityEngine;
using System.Collections;

public abstract class AWeaponOneHandedMelee<TModuleType> : AWeaponMelee<TModuleType> where TModuleType : APlayer
{

	public AWeaponOneHandedMelee()
	{
		this.equipmentEmplacement = e_equipmentEmplacement.Right_Hand;
	}
}
