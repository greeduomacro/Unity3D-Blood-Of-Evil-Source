using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour {
	#region Attributes
	[SerializeField] private float distance= 10.0f;
	[SerializeField] private float height= 5.0f;
	[SerializeField] private float heightDamping= 2.0f;
	[SerializeField] private float rotationDamping= 3.0f;
	[SerializeField] private Transform target;
	[SerializeField] private bool isLookingTarget;
	private Transform trans;
	#endregion
	#region Properties
	public Transform Target { get { return target; } set { target = value; } }
	public float Distance { get { return distance; } set { if (value >= 0) distance = value; } }
	public float Height { get { return height; } set { if (value >= 0) height = value; } }
	public float HeightDamping { get { return heightDamping; } set { if (value >= 0) heightDamping = value; } }
	public float RotationDamping { get { return rotationDamping; } set { if (value >= 0) rotationDamping = value; } }
	public bool IsLookingTarget { get { return isLookingTarget; } set { isLookingTarget = value; } }
	#endregion

	public SmoothFollow()
	{
		this.isLookingTarget = true;
	}

	void Awake()
	{
		trans = transform;
	}
	
	void  LateUpdate (){
		if (!this.target)
			return;

		float wantedRotationAngle = this.target.eulerAngles.y;
		float wantedHeight = this.target.position.y + height;
		float currentRotationAngle= this.trans.eulerAngles.y;
		float currentHeight = this.trans.position.y;

		currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, this.rotationDamping * Time.deltaTime);
		currentHeight = Mathf.Lerp(currentHeight, wantedHeight, this.heightDamping * Time.deltaTime);

		Quaternion currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);
		this.trans.position = target.position;
		this.trans.position -= currentRotation * Vector3.forward * this.distance;
		this.trans.position = new Vector3(this.trans.position.x, currentHeight, this.trans.position.z);

		if (true == this.isLookingTarget)
			this.trans.LookAt(this.target);
	}
}
