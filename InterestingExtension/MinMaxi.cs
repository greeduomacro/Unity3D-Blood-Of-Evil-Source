using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class MinMaxi
{
	#region Data Attributes
	private int min;
	private int max;
	#endregion
	#region Data Properties
	public int Min
	{
		get { return min; }
		set { min = value; }
	}
	public int Max
	{
		get { return max; }
		set { max = value; }
	}
	#endregion
	#region Builder
	public MinMaxi()
	{
		this.min = 0;
		this.max = 0;
	}
	public MinMaxi(int mini, int maxi)
	{
		min = mini;
		max = maxi;
	}
	#endregion
	#region Functions
	public void Initialize(int mini, int maxi)
	{
		min = mini;
		max = maxi;
	}
	public float Ratio()
	{
		return this.min / this.max;
	}
	public float Percent()
	{
		return this.Ratio() * 100.0f;
	}
	public int RandomBetweenValues()
	{
		return UnityEngine.Random.Range(this.min, this.max + 1);
	}
	#endregion
}

[Serializable]
public class MinMaxf
{
	#region Builder
	public float min;
	public float max;
	#endregion
	#region Builder
	public MinMaxf(float mini, float maxi)
	{
		this.min = mini;
		this.max = maxi;
	}
	public MinMaxf()
	{
		this.min = this.max = 0.0f;
	}
	#endregion
	#region Functions
	public void Initialize(float mini, float maxi)
	{
		this.min = mini;
		this.max = maxi;
	}
	public float Ratio()
	{
		return this.min / this.max;
	}
	public float Percent()
	{
		return this.Ratio() * 100.0f;
	}
	public float RandomBetweenValues()
	{
		return UnityEngine.Random.Range(this.min, this.max);
	}
	#endregion
}
