using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public sealed class EquipmentPossibleAttributeInitialize {
	private List<EquipmentPossibleAttribute> possibleAttribute;
	
	#region Properties
	public List<EquipmentPossibleAttribute> PossibleAttribute {	get { return possibleAttribute; }
																private set { possibleAttribute = value; } } 
	#endregion

	public EquipmentPossibleAttributeInitialize()
	{
		possibleAttribute = new List<EquipmentPossibleAttribute>();
	}

	public void Add(e_entityAttribute attri, float proba, Vector2 val)	{
		possibleAttribute.Add(new EquipmentPossibleAttribute(attri, proba, val));
	}
}
