using UnityEngine;
using System.Collections;

//public class LoadLevelWhenTriggered : ALoadLevel {
//    private ADoor door;
//    void Awake()
//    {
//        door = transform.parent.GetComponent<ADoor>();
//    }

//    void OnTriggerEnter(Collider collision)
//    {
//        if (collision.transform.root.tag == "Player" && door.Open)
//        {
//            int doorID = door.DoorID;
			
//            LoadLevel();

//            GameObject[] doors = GameObject.FindGameObjectsWithTag("Door");

//            foreach (GameObject dodor in doors)
//            {
//                Debug.Log("its work its fantastic");
//                if (door.GetComponent<ADoor>().DoorID == doorID)
//                {
//                    GameObject player = GameObject.Find("Player");
//                    Debug.Log(player);
//                    player.transform.position = dodor.transform.position - Vector3.forward * 5;
//                    //player.transform.forward = dodor.transform.forward;

//                    break;
//                }
//            }
//        }
//    }
//}
