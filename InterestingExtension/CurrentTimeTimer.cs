using UnityEngine;
using System.Collections;

[System.Serializable]
public class CurrentTimeTimer
{
	#region Data Attributes
	private float current;
	private float timer;
	#endregion
	#region Properties
	public float Current
	{
		get { return current; }
		set { current = value; }
	}
	public float Timer
	{
		get { return timer; }
		set { if (value >= 0) timer = value; }
	}
	#endregion
	#region Builder
	public CurrentTimeTimer()
	{
		this.current = this.timer = 0.0f;
	}
	public CurrentTimeTimer(float cur, float time)
	{
		this.current = cur;
		this.timer = time;
	}
	#endregion
	#region Functions
	public void Initialize(int cur, float time)
	{
		this.current = cur;
		this.timer = time;
	}
	public void Update()
	{
		this.current += Time.deltaTime;
	}
	public bool CurrentTimeIsBiggerThanTimer()
	{
		return this.current >=  this.timer;
	}
	public void Reset()
	{
		this.current = 0;
	}
	public bool CanDoMyAction()
	{
		if (this.CurrentTimeIsBiggerThanTimer())
		{
			this.Reset();
			return true;
		}

		return false;
	}
	#endregion
}