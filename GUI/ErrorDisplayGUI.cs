using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ErrorDisplayGUI : MonoBehaviour
{
	#region Attributes
	private ErrorDisplayStack errorStack;
	#endregion
	#region Properties
	public ErrorDisplayStack ErrorStack
	{
		get { return errorStack; }
		private set { errorStack = value; }
	}
	#endregion
	#region Builder
	void Awake()
	{
		this.errorStack = gameObject.GetComponent<ErrorDisplayStack>();
	}
	#endregion
	#region Unity Functions
	void OnGUI()
	{
		List<ErrorDisplay> errors = this.errorStack.Errors;
		int numberOfErrorDisplay = errors.Count;
		
		if (numberOfErrorDisplay > 5)
			numberOfErrorDisplay = 5;

		for (short i =0; i < numberOfErrorDisplay; i++)
		{
			string color = "<color=white>";

			switch (errors[i].ErrorDisplayType)
			{
				case e_errorDisplay.Error: color = "<color=red>"; break;
				case e_errorDisplay.Warning: color = "<color=orange>"; break;
				case e_errorDisplay.Critical: color = "<color=purple>"; break;
				default: break;
			}

			GUI.Label(MultiResolutions.Rectangle(0, i * 0.1f, 1, 1),
				MultiResolutions.Font(errors[i].FontSize) + color + errors[i].description + "</color></size>");
		}
	}
	#endregion
}
