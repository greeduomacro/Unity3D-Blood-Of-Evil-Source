using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class LaserTest : MonoBehaviour {
	private LineRenderer lr;
	private Transform trans;
	public float distance;
	void Start () {
		trans = transform;
		lr = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		RaycastHit hit;

		lr.SetVertexCount(2); //1 debut, 2 fin
		lr.SetPosition(0, trans.position);	//ou commence le laser

		if (Physics.Raycast(trans.position, trans.forward, out hit, distance))
			lr.SetPosition(1, hit.point);	//ou termine le laser
		else
			lr.SetPosition(1, trans.position + trans.forward * distance);	//ou termine le laser
	
	}
}
