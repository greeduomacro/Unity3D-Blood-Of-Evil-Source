using UnityEngine;
using System.Collections;

public class ArmFPSCamera : MonoBehaviour {

    [SerializeField]
    private GameObject fpsCamera;
    Transform cameraTransform;

    Transform transf;

	void Start () {
        transf = transform;
        cameraTransform = fpsCamera.transform;
	}
	
	void Update () {
        transf.position = cameraTransform.position;
        transf.rotation = cameraTransform.rotation;
	}
}
