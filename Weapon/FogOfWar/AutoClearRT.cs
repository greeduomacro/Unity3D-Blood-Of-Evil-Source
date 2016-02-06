using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class AutoClearRT : MonoBehaviour {
	private bool noClearAfterStart = false;

	void Start () {
		GetComponent<Camera>().clearFlags = CameraClearFlags.Color;
	}
	
	// Update is called once per frame
	void Update () {
		if (!noClearAfterStart)
			GetComponent<Camera>().clearFlags = CameraClearFlags.Depth;
	}
}
