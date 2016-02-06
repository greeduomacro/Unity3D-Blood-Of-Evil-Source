using UnityEngine;
using System.Collections;

public abstract class AWeaponTwoHandedRange<TModuleType> : AWeaponRange<TModuleType> where TModuleType : APlayer
{
	public AWeaponTwoHandedRange()
	{
		this.equipmentEmplacement = e_equipmentEmplacement.Both_Hand;
		this.importanceOAttributefValue *= 2.0f;
	}
}
