using UnityEngine;
using System.Collections;

public class FPSCamera : MonoBehaviour {

    [SerializeField]
    private GameObject shoulderParent;
    [SerializeField]
    private GameObject shoulder;
    [SerializeField]
    private float mouseSensitivity = 1.0f;
    public float MouseSensitivity { get { return mouseSensitivity; } set { if (value > 0) mouseSensitivity = value; } }
    [SerializeField]
    private bool reverseYAxis = false;
    [SerializeField]
    private GameObject player;

    Transform transfShoulderParent;
    Transform transfShoulder;
    Transform transf;
    Transform parentTransform;

	void Start ()
    {
        transf = transform;
        parentTransform = player.transform;
        transfShoulderParent = shoulderParent.transform;
        transfShoulder = shoulder.transform;
	}
	
	void Update ()
    {
        if (Time.timeScale == 0)
            return;

        //Cursor.visible = true;
        transf.Rotate(Vector3.up, mouseSensitivity * Input.GetAxis("Mouse X"), Space.World);
        parentTransform.rotation = Quaternion.Lerp(parentTransform.rotation, transf.rotation, 0.5f);

        parentTransform.eulerAngles = new Vector3(0, parentTransform.eulerAngles.y, 0);

        float mouseDeltaY;

        if (reverseYAxis)
            mouseDeltaY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        else
            mouseDeltaY = -Input.GetAxis("Mouse Y") * mouseSensitivity;

        if (mouseDeltaY < 0 && transf.eulerAngles.x + mouseDeltaY < 270 && transf.eulerAngles.x > 180)
            transf.eulerAngles = new Vector3(270, transf.eulerAngles.y, transf.eulerAngles.z);
        else if (mouseDeltaY > 0 && transf.eulerAngles.x + mouseDeltaY > 45 && transf.eulerAngles.x < 180)
            transf.eulerAngles = new Vector3(45, transf.eulerAngles.y, transf.eulerAngles.z);
        else
            transf.eulerAngles += new Vector3(mouseDeltaY, 0, 0);

        if (transf.eulerAngles.x < 180)
            transfShoulderParent.eulerAngles = new Vector3(transf.eulerAngles.x / 2 , transfShoulderParent.eulerAngles.y, transfShoulderParent.eulerAngles.z);
        else
            transfShoulderParent.eulerAngles = new Vector3(-(360 - transf.eulerAngles.x) / 2, transfShoulderParent.eulerAngles.y, transfShoulderParent.eulerAngles.z);

        transf.position = transfShoulder.up + transfShoulder.position + 0.25f * transfShoulder.forward;
    }
}
