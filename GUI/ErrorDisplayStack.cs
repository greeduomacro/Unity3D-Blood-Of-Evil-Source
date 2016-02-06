using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ErrorDisplayStack : AServiceInitializer
{
	#region Data Attributes
	private List<ErrorDisplay> errors;
	#endregion
	#region Data Properties
	public List<ErrorDisplay> Errors
	{
		get { return errors; }
		private set { errors = value; }
	}
	#endregion
	#region Builder
	public override void  Initialize()
	{
		this.errors = new List<ErrorDisplay>();
	}
	#endregion
	#region Unity Functions
	void Update()
	{
		List<ErrorDisplay> errorsRef = new List<ErrorDisplay>();

		for (short i =0; i < this.errors.Count; i++)
		{
			this.errors[i].CurrentTime += Time.deltaTime;

			if (this.errors[i].CurrentTime >= this.errors[i].TimeToDisplay)
				errorsRef.Add(errors[i]);
		}

		foreach (ErrorDisplay error in errorsRef)
			this.errors.Remove(error);
	}
	#endregion
	#region Functions
	public void Add(string text, e_errorDisplay err)	{
		this.errors.Add(new ErrorDisplay(text, err));
	}
	#endregion
}

