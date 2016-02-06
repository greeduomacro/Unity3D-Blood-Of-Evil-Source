using UnityEngine;
using System.Collections;

public sealed class ClotheTrousers<TModuleType> : AClothe<TModuleType> where TModuleType : APlayer
{
	public ClotheTrousers()
	{
		this.clotheCategory = e_clotheCategory.Trousers;
		this.equipmentEmplacement = e_equipmentEmplacement.Trousers;
		this.importanceOAttributefValue *= 1.3f;
		this.weight = 18;
	}
}
