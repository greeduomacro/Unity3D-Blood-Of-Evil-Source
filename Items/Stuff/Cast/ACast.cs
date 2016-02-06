using UnityEngine;
using System.Collections;

public abstract class ACast<TModuleType> : AStuff<TModuleType> where TModuleType : APlayer
{
	#region Attributes
	protected int manaCost;
	protected e_castCategory castCategory;
	#endregion
	#region Properties
	public int ManaCost {	get { return manaCost; }
							private set { manaCost = value; } }
	public e_castCategory CastCategory {	get { return castCategory; }
											private set { castCategory = value; } }
	#endregion
	//public override void Attack() { }

	public ACast()
	{
		this.equipmentCategory = e_equipmentCategory.Cast;
		this.filtre = ItemExtension.FiltreWeapon;
		this.price = 150 * (((int)equipmentQuality) + 1);


		this.AddRandomAttribute(e_entityAttribute.Mana, 20, new Vector2(10, 14));
		this.AddRandomAttribute(e_entityAttribute.Energy, 20, new Vector2(3, 4));
		this.AddRandomAttribute(e_entityAttribute.Mana_Percent, 20, new Vector2(10, 15));
		this.AddRandomAttribute(e_entityAttribute.Strength, 20, new Vector2(2, 4));
		this.AddRandomAttribute(e_entityAttribute.Dexterity, 20, new Vector2(2, 4));
		this.AddRandomAttribute(e_entityAttribute.Vitality, 20, new Vector2(2, 4));
		this.AddRandomAttribute(e_entityAttribute.Damage, 20, new Vector2(6, 8));

		this.importanceOAttributefValue *= 2.3f;
		AttributeInitializer<TModuleType>.AddFixedAttribute(e_entityAttribute.Fast_Cast_Percent, new Vector2(0.8f / this.importanceOAttributefValue, 1.2f / this.importanceOAttributefValue), this);
		this.weight = 12;

		this.equipmentEmplacement = e_equipmentEmplacement.Both_Hand;
	}
}
