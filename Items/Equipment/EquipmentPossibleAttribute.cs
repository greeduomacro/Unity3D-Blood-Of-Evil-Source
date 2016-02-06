using UnityEngine;
using System.Collections;

[System.Serializable]
public class EquipmentPossibleAttribute {
	public e_entityAttribute attribute {get;private set;}
	public float probability { get; private set; }
	public float value { get; set; }
	public Vector2 values { get; private set; }

	public EquipmentPossibleAttribute() {}
	public EquipmentPossibleAttribute(e_entityAttribute attri, float proba, Vector2 val) {
		attribute = attri;
		probability = proba;
		values = val;//Random.Range(val.x + 0f, val.y + 0f);
	}

	public EquipmentPossibleAttribute(e_entityAttribute attri, Vector2 val)	{
		attribute = attri;
		values = val;//Random.Range(val.x + 0f, val.y + 0f);
	}
}
