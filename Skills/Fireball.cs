using UnityEngine;
using System.Collections;

public class Fireball<TModuleType> : APhysicSkill<TModuleType> where TModuleType : APlayer
{
	[HideInInspector]
    public float moveSpeed { get { return moveSpeed; } set { if (value > 0) moveSpeed = value; } }

	void Awake()	{
		base.MyAwake();
		Destroy(gameObject, 4);	
	}	

	void Update () {
		trans.position += trans.forward * Time.deltaTime * moveSpeed;
	}

	void OnTriggerEnter(Collider col){
        if (col.transform.root.tag == targetTag)
		{
            base.UpdateEntityAttribute(col.transform.root.gameObject);
			Destroy(gameObject);
			//col.transform.position = col.transform.position + trans.forward * 3;
		}
	}
}
