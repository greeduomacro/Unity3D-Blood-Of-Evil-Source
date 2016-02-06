using UnityEngine;
using System.Collections;

public enum e_errorDisplay
{
	Warning,
	Critical,
	Error,
}

public sealed class ErrorDisplay
{
    public string description { get; private set; }
    private float timeToDisplay = 3.0f;
    public float TimeToDisplay{ get { return timeToDisplay; } set { if (value > 0) timeToDisplay = value; } }
	private e_errorDisplay errorDisplay = e_errorDisplay.Warning;
    public e_errorDisplay ErrorDisplayType { get { return errorDisplay; } set { errorDisplay = value; } }
    private float currentTime;
    public float CurrentTime { get { return currentTime; } set { if (value >= 0) currentTime = value; } }
	private float fontSize = 18f;
    public float FontSize { get { return fontSize; } set { if (value > 0) fontSize = value; } }


	public ErrorDisplay() { }
	public ErrorDisplay(string desc, e_errorDisplay err)
	{
		description = desc;
		errorDisplay = err;
	}
}
