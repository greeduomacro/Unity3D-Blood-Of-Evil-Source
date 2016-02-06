using UnityEngine;
using System.Collections;

public static class Vector3Extension {

	public static float GetOnePerMaxOfVectorComponent(this Vector3 size)
	{
		float onePerMaxOfVectorComponent = 1 / size.x;

		if (onePerMaxOfVectorComponent < 1 / size.y)
			onePerMaxOfVectorComponent = 1 / size.y;
		if (onePerMaxOfVectorComponent < 1 / size.z)
			onePerMaxOfVectorComponent = 1 / size.z;

		return onePerMaxOfVectorComponent;
	}
}
