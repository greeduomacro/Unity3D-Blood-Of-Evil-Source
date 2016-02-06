using UnityEngine;
using System.Collections;

public sealed class ClotheShield<TModuleType> : AClothe<TModuleType> where TModuleType : APlayer
{
	public ClotheShield()
	{
		this.clotheCategory = e_clotheCategory.Shield;
		this.equipmentEmplacement = e_equipmentEmplacement.Left_Hand;
		this.importanceOAttributefValue *= 1.4f;
		this.AddFixedAttribute(e_entityAttribute.Block_Chance, new Vector2(28f, 35f));
		this.AddRandomAttribute(e_entityAttribute.Block_Chance, 20, new Vector2(5f, 8f));
		this.weight = 20;
	}
}
