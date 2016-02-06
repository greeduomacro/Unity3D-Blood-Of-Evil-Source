using UnityEngine;
using System.Collections;

public class ChangeMaterialColor : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
