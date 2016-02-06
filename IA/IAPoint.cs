using UnityEngine;
using System.Collections;

public class IAPoint : MonoBehaviour {
	void Awake()
	{
		RaycastHit hit;

		if (Physics.Raycast(transform.position, -Vector3.up * 100, out hit))
			transform.position = hit.point;
		else
			transform.position = transform.position - Vector3.up * 100;
	}
}
