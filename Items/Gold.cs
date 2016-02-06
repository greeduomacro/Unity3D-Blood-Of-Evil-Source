using UnityEngine;
using System.Collections;

public sealed class Gold : MonoBehaviour {
	public string goldName;
	public int quantity;

	public void Initialize(int quantity, string name)
	{
		this.quantity = quantity;
		this.goldName = name;
	}

	public string GetName()
	{
		return this.goldName + ": <color=yellow><b>" + this.quantity + "</b></color>";
	}
}
