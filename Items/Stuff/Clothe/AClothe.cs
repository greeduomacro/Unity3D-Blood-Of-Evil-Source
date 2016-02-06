using UnityEngine;
using System.Collections;

[System.Serializable]
public abstract class AClothe<TModuleType> : AStuff<TModuleType> where TModuleType : APlayer
{
	//public int armor;
	#region Attributes
	protected e_clotheCategory clotheCategory;
	#endregion
	#region Properties
	public e_clotheCategory ClotheCategory {	get { return clotheCategory; }
												private set { clotheCategory = value; } }
	#endregion

	public AClothe()
	{
		this.levelRequiered = 5;
		this.filtre = ItemExtension.FiltreClothe;
		this.equipmentCategory = e_equipmentCategory.Clothe;
		this.price = 50 * (((int)this.equipmentQuality) + 1);

		this.clotheCategory = e_clotheCategory.Armor;
		//clotheCategory = (e_clotheCategory)Random.Range(0, (int)(e_clotheCategory.SIZE));
		this.equipmentEmplacement = e_equipmentEmplacement.Armor;
		this.weight = 18f;
		AttributeInitializer<TModuleType>.AddFixedAttribute(e_entityAttribute.Defence, new Vector2(50, 75), this);

		this.AddRandomAttribute(e_entityAttribute.Vitality, 20, new Vector2(2, 4));
		this.AddRandomAttribute(e_entityAttribute.Endurance, 20, new Vector2(2, 4));
		this.AddRandomAttribute(e_entityAttribute.Life, 20, new Vector2(25, 32));
		this.AddRandomAttribute(e_entityAttribute.Life_Percent, 50, new Vector2(3f, 4.5f));
		this.AddRandomAttribute(e_entityAttribute.Mana, 35, new Vector2(13, 16));
		this.AddRandomAttribute(e_entityAttribute.Resistance, 50, new Vector2(2, 4));
		this.AddRandomAttribute(e_entityAttribute.Mana_Percent, 50, new Vector2(3f, 5f));
		this.AddRandomAttribute(e_entityAttribute.Energy, 50, new Vector2(2, 4));

		//EquipmentAtt

		//switch (clotheCategory)
		//{
		//    case e_clotheCategory.Armor :		equipmentEmplacement = e_equipmentEmplacement.Armor;	break;
		//    case e_clotheCategory.Belt :		equipmentEmplacement = e_equipmentEmplacement.Belt;		break;
		//    case e_clotheCategory.Glove :		equipmentEmplacement = e_equipmentEmplacement.Glove;	break;
		//    case e_clotheCategory.Helmet:		equipmentEmplacement = e_equipmentEmplacement.Helmet;	break;
		//    case e_clotheCategory.Shoes :		equipmentEmplacement = e_equipmentEmplacement.Shoes;	break;
		//    case e_clotheCategory.Troussers :	equipmentEmplacement = e_equipmentEmplacement.Trousers;	break;
		//}
	}
}
