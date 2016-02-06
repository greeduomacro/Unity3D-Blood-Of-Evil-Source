using UnityEngine;
using System.Collections;

public sealed class ClotheHelmet<TModuleType> : AClothe<TModuleType> where TModuleType : APlayer
{
	public ClotheHelmet()	{
		this.importanceOAttributefValue *= 1.2f;
		this.clotheCategory = e_clotheCategory.Helmet;
		this.equipmentEmplacement = e_equipmentEmplacement.Helmet;
		this.weight = 13;
	}
}
