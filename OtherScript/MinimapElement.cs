using UnityEngine;
using System.Collections;

public class MinimapElement : MonoBehaviour
{
	#region Builder
	void Start ()
	{
		ServiceLocator.Instance.RotateSubElementOfMinimap.TransformArray.Add(gameObject.GetComponent<RectTransform>());
	}
	#endregion
}
