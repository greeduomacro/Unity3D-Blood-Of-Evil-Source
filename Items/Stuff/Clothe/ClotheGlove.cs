using UnityEngine;
using System.Collections;

public class ClotheGlove<TModuleType> : AClothe<TModuleType> where TModuleType : APlayer
{
	public ClotheGlove()
	{
		this.clotheCategory = e_clotheCategory.Belt;
		this.equipmentEmplacement = e_equipmentEmplacement.Belt;
		this.importanceOAttributefValue *= 0.7f;
		this.weight = 7f;
	}
}
