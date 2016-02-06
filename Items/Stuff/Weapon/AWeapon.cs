using UnityEngine;
using System.Collections;

[System.Serializable]
public abstract class AWeapon<TModuleType> : AStuff<TModuleType> where TModuleType : APlayer
{
	#region Attributes
	protected e_weaponCategory weaponCategory;
	protected e_weaponType weaponType;
	#endregion
	#region Properties
	public e_weaponCategory WeaponCategory {	get { return weaponCategory; }
												private set { weaponCategory = value; } }
	public e_weaponType WeaponType {	get { return weaponType; }
										private set { weaponType = value; } }
	#endregion

	public AWeapon()
	{
		this.filtre = ItemExtension.FiltreWeapon;
		this.equipmentCategory = e_equipmentCategory.Weapon;
		this.weaponType = e_weaponType.Sword;
		this.weaponCategory = e_weaponCategory.Melee;
		this.price = 100 * (((int)this.equipmentQuality) + 1);
		this.weight = 15.0f;

		this.AddFixedAttribute(e_entityAttribute.Damage_Minimal, new Vector2(28, 30));
		this.AddFixedAttribute(e_entityAttribute.Damage_Maximal, new Vector2(31, 38));
		this.AddFixedAttribute(e_entityAttribute.Attack_Speed_Percent, new Vector2(1.2f / importanceOAttributefValue, 0.7f / importanceOAttributefValue));

		this.AddRandomAttribute(e_entityAttribute.Strength, 20, new Vector2(2, 4));
		this.AddRandomAttribute(e_entityAttribute.Dexterity, 20, new Vector2(2, 4));
		this.AddRandomAttribute(e_entityAttribute.Damage, 20, new Vector2(6, 8));
		this.AddRandomAttribute(e_entityAttribute.Damage_Minimal, 50, new Vector2(8, 12));
		this.AddRandomAttribute(e_entityAttribute.Damage_Maximal, 35, new Vector2(8, 12));
		this.AddRandomAttribute(e_entityAttribute.Attack_Speed_PercentPercent, 15, new Vector2(0.08f, 0.13f));

		this.equipmentEmplacement = e_equipmentEmplacement.Right_Hand;
	}
}
