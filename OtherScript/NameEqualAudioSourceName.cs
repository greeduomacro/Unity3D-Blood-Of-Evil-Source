using UnityEngine;
using System.Collections;

public class Rename : MonoBehaviour {
	void Awake()
	{
		gameObject.name = gameObject.GetComponent<AudioSource>().clip.name;
	}
}
