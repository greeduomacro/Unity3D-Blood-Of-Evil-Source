using UnityEngine;
using System.Collections;

public sealed class ClotheBelt<TModuleType> : AClothe<TModuleType> where TModuleType : APlayer
{
	public ClotheBelt()	{
		this.importanceOAttributefValue *= 0.5f;
		this.clotheCategory = e_clotheCategory.Belt;
		this.equipmentEmplacement = e_equipmentEmplacement.Belt;
		this.weight = 5;
	}
}
