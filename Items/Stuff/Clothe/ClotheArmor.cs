using UnityEngine;
using System.Collections;

public sealed class ClotheArmor<TModuleType> : AClothe<TModuleType> where TModuleType : APlayer
{
	public ClotheArmor() {
		this.importanceOAttributefValue *= 1.8f;
		this.clotheCategory = e_clotheCategory.Armor;
		this.weight = 25f;
	}
}
