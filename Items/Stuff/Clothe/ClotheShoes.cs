using UnityEngine;
using System.Collections;

public sealed class ClotheShoes<TModuleType> : AClothe<TModuleType> where TModuleType : APlayer
{
	public ClotheShoes()	{
		this.clotheCategory = e_clotheCategory.Shoes;
		this.equipmentEmplacement = e_equipmentEmplacement.Shoes;
		this.importanceOAttributefValue *= 1.0f;

		this.AddFixedAttribute(e_entityAttribute.Move_Speed_Percent, new Vector2(15f, 30f));

		this.AddRandomAttribute(e_entityAttribute.Move_Speed, 20, new Vector2(2, 6));
		this.AddRandomAttribute(e_entityAttribute.Move_Speed_Percent, 20, new Vector2(7.5f, 15f));
		this.weight = 9;
	}
}
