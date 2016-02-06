using UnityEngine;
using System.Collections;

public class CurrentMaxi
{
	#region Data Attributes
	private int current;
	private int max;
	#endregion
	#region Data Properties
	public int Current
	{
		get { return current; }
		set { if (value >= 0) current = value; }
	}
	public int Max
	{
		get { return max; }
		set { if (value >= 0) { max = value; } }
	}
	#endregion
	#region Builder
	public CurrentMaxi(int cur, int maxi)
	{
		this.current = cur;
		this.max = maxi;
	}
	public CurrentMaxi()
	{
		this.current = this.max = 0;
	}
	#endregion
	#region Functions
	public bool CurrentIsBiggerThanMax()
	{
		return this.current >= this.max;
	}
	public bool LevelUp()
	{
		bool levelUp = this.CurrentIsBiggerThanMax();

		if (levelUp)
			this.current -= this.max;

		return levelUp;
	}
	public void Initialize(int cur, int maxi)
	{
		this.current = cur;
		this.max = maxi;
	}
	public float Ratio()
	{
		return this.current / this.max;
	}
	public float Percent()
	{
		return this.Ratio() * 100.0f;
	}
	public float RandomBetweenValues()
	{
		return Random.Range(this.current, this.max + 1);
	}
	#endregion
}


[System.Serializable]
public class CurrentMaxf
{
	#region Data Attributes
    private float current;
    private float max;
	#endregion
	#region Data Properties
	public float Current
	{
		get { return current; }
		set { current = value; }
	}
	public float Max
	{
		get { return max; }
		set { if (value >= 0) { max = value; } }
	}
	#endregion
	#region Builder
	public CurrentMaxf(float cur, float maxi)
	{
		this.current = cur;
		this.max = maxi;
	}
	public CurrentMaxf()
	{
		this.current = this.max = 0.0f;
	}
	#endregion
	#region Functions
	public void Initialize(float cur, float maxi)
	{
		this.current = cur;
		this.max = maxi;
	}
	public float Ratio()
	{
		return this.current / this.max;
	}
	public float Percent()
	{
		return this.Ratio() * 100.0f;
	}
	public float RandomBetweenValues()
	{
		return Random.Range(this.current, this.max);
	}
	#endregion
}
