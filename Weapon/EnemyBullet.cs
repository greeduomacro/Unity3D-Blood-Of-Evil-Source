using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour {

    Transform transf;
    [SerializeField]
    private float damage = 20;
    public float Damage { get { return damage; } set { if (value > 0) damage = value; } }

	// Use this for initialization
	void Start () {
        transf = transform;
	}
	
	// Update is called once per frame
	void Update () {
	    Vector3 translate = transf.up * 100 * Time.deltaTime;

        if (translate == Vector3.zero)
            return;

        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(transf.position, transf.up, out hit, translate.magnitude))
        {
			//AEntityAttribute attr = hit.collider.transform.root.GetComponentInChildren<AEntityAttribute>();
			//if (attr)
			//    attr.GetDamaged(damage);
            Destroy(gameObject);
        }
        else
            transf.Translate(translate, Space.World);
	}
}
