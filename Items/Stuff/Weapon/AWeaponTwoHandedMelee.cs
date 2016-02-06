using UnityEngine;
using System.Collections;

public abstract class AWeaponTwoHandedMelee<TModuleType> : AWeaponMelee<TModuleType> where TModuleType : APlayer
{

	public AWeaponTwoHandedMelee()
	{
		this.equipmentEmplacement = e_equipmentEmplacement.Both_Hand;
		this.importanceOAttributefValue *= 2.8f;
	}
}
