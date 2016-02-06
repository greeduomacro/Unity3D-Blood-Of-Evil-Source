using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RotateSubElementOfMinimap : AServiceInitializer 
{
	#region Data Attributes
	private float rotateY = 0.0f;
	#endregion
	#region Attributes
	private List<RectTransform> transformArray;
	#endregion
	#region Data Properties
	public float RotateY
	{
		get { return rotateY; }
		set { rotateY = value; }
	}
	#endregion
	#region Properties
	public List<RectTransform> TransformArray
	{
		get { return transformArray; }
		private set { transformArray = value; }
	}
	#endregion
	#region Builder
	public override void Initialize()
	{
		this.rotateY = 0;
		this.transformArray = new List<RectTransform>();
	}
	#endregion
	#region Unity Functions
	void	Update()
	{
		if (this.rotateY != 0)
		{
			foreach (RectTransform onetransform in this.transformArray)
				onetransform.eulerAngles = new Vector3(onetransform.eulerAngles.x, onetransform.eulerAngles.y + this.rotateY, onetransform.eulerAngles.z);

			this.rotateY = 0.0f;
		}
	}
	#endregion
}
