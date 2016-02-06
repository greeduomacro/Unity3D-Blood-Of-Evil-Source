using UnityEngine;
using System.Collections;

public enum EStuffType
{
    SWORD,
    DOUBLE_HANDED_SWORD,
}

public class PhysicalStuff : MonoBehaviour {

    [SerializeField]
    private EStuffType weaponType;
    public EStuffType WeaponType { get { return weaponType; } }
    [SerializeField]
    private float range;
    public float Range { get { return range; } }

	void Start ()
    {
	}
	
	void Update () {

	}
}
