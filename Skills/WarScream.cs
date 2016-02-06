using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WarScream<TModuleType> : APhysicSkill<TModuleType> where TModuleType : APlayer
{

    [HideInInspector]
    public float fastCast { get { return fastCast; } set { if (value > 0) fastCast = value; } }
	private List<GameObject> objectsHitted;	
	

	void Awake()	{
		base.MyAwake();
		objectsHitted = new List<GameObject>();
		Destroy(gameObject, 4);	
	}	

	void Update () {
		trans.position += trans.forward * Time.deltaTime * fastCast;
	}

	void OnTriggerEnter(Collider col)	{
		if (col.transform.root.tag == targetTag)
		{
            for (short i =0; i < objectsHitted.Count; i++)
				if (objectsHitted[i] == col.gameObject)
					return;

			GameObject entityCollidedObject = col.transform.root.gameObject;

			base.UpdateEntityAttribute(entityCollidedObject);

			entityCollidedObject.GetComponent<Rigidbody>().velocity = (trans.forward + trans.up) * 10;
            entityCollidedObject.GetComponent<Rigidbody>().useGravity = true;
            //EnemyAttribute attr = entityCollidedObject.GetComponent<EnemyAttribute>();
			//if (attr)
			//    attr.AI.Speed = 0;
			//col.transform.position = col.transform.position + trans.forward * 3;
			objectsHitted.Add(entityCollidedObject);
		
		}
	}
}
