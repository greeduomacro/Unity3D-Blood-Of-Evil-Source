using UnityEngine;
using System.Collections;

public class FollowPlayerOrientation : MonoBehaviour {

    private GameObject player;
    private Transform transf;
    private Transform playerTransf;

	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("PlayerBody");
        playerTransf = player.transform;
        transf = transform;
	}
	
	void Update ()
    {
        transf.forward = playerTransf.forward;	    
	}
}
