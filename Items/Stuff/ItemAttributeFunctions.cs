using System;
using System.Collections;
using UnityEngine;

public class ItemAttributeFunctions : MonoBehaviour {
	public delegate void functionDelegate(float value);

	public functionDelegate[] function;

	void Awake() {
		this.function = new functionDelegate[Enum.GetNames(typeof(e_entityAttribute)).Length];

		this.function[(int)(e_entityAttribute.Life)] = Life;
		this.function[(int)(e_entityAttribute.Life_Percent)] = LifePercent;
	}

	void Life(float value)	{
		//gameObject.GetComponent<PlayerLifeScript>().life += value;
	}

	void LifePercent(float value)	{
		//gameObject.GetComponent<PlayerLifeScript>().lifePercent += value;
	}
}
