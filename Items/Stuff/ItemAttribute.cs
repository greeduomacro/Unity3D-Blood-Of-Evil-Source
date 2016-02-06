using UnityEngine;
using System.Collections;

public class ItemAttribute
{
	public float	value;
	public e_entityAttribute whichAttribute;

	public ItemAttribute(float val, e_entityAttribute eq)
	{
		this.value = val;
		this.whichAttribute = eq;
	}
}
