using UnityEngine;
using System.Collections;

public class ElevatorShaft : MonoBehaviour {

    void OnTriggerEnter(Collider col)
    {
        if (col.transform.root.tag == "Player")
        {
            col.GetComponent<Collider>().transform.root.parent = gameObject.transform;
            col.GetComponent<Collider>().transform.position = new Vector3(col.GetComponent<Collider>().transform.position.x,
                                                            gameObject.transform.position.y,
                                                            col.GetComponent<Collider>().transform.position.z);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.transform.root.tag == "Player")
        {
            col.GetComponent<Collider>().transform.root.parent = null;
            col.GetComponent<Collider>().transform.position = new Vector3(  col.GetComponent<Collider>().transform.position.x,
                                                            gameObject.transform.position.y,
                                                            col.GetComponent<Collider>().transform.position.z);
        }
    }
}
