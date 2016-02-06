using UnityEngine;
using System.Collections;

public class ShoulderPosition : MonoBehaviour {

    [SerializeField]
    private GameObject   body;

    Transform           transf;
    Transform           bodyTransform;

	void Start ()
    {
        transf = transform;
        bodyTransform = body.transform;
	}
	
	void Update ()
    {
        transf.position = bodyTransform.position + bodyTransform.up;
        transf.eulerAngles = new Vector3(transf.eulerAngles.x, bodyTransform.eulerAngles.y, transf.eulerAngles.z);
	}
}
