using UnityEngine;
using System.Collections;

public sealed class ObjectAttributeCompared
{
	public string name;
	public float difference;
	public float value;

	public ObjectAttributeCompared() { }
	public ObjectAttributeCompared(ObjectAttributeCompared copy)
	{
		name = copy.name;
		difference = copy.difference;
		value = copy.value;
	}
	public ObjectAttributeCompared(ObjectAttribute copy)
	{
		name = copy.name;
		value = copy.value;
	}
	public ObjectAttributeCompared(string n, float dif, float val)
	{
		name = n;
		value = val;
		difference = dif;
	}
}

public sealed class ObjectAttribute
{
	public string name;
	public float value;

	public ObjectAttribute() { }
	public ObjectAttribute(ObjectAttributeCompared copy)
	{
		name = copy.name;
		value = copy.value;
	}
	public ObjectAttribute(string n, float val)
	{
		name = n;
		value = val;
	}
}
