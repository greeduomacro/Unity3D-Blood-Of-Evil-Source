using UnityEngine;
using System.Collections;

public class LaserWeapon : MonoBehaviour {
	public GameObject gunBarrel;
	public Transform trans;
	// Use this for initialization
	void Start () {
		trans = transform;
		gunBarrel = trans.GetChild(0).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
			Fire();
	}

	void Fire()
	{
		RaycastHit hit;

		if (Physics.Raycast(gunBarrel.transform.position, gunBarrel.transform.forward, out hit))
			Debug.Log(hit.collider.gameObject.name);
	}
}
