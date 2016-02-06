using UnityEngine;
using System.Collections;

[System.Serializable]
public class EquipmentAttribute
{
	private float	value;
	private e_entityAttribute whichAttribute;

	#region Properties
	public float Value {	get { return value; } 
							set { if (0 < value) this.value = value; } }
	public e_entityAttribute WhichAttribute {	get { return whichAttribute; }
												set { whichAttribute = value; } }
	#endregion

	public EquipmentAttribute() {}

	public EquipmentAttribute(float val, e_entityAttribute eq)
	{
		value = val;
		whichAttribute = eq;
	}
}
