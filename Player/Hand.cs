using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour {

	//[SerializeField]
	//private GameObject weaponObject;
	//public GameObject WeaponObject { get { return weaponObject; } }
	//private PhysicalStuff weapon;
	//public PhysicalStuff Weapon { get { return weapon; } }
	//private PhysicalCast cast;
	//public PhysicalCast Cast { get { return cast; } }

	//Transform transfWeapon;
	//Transform transf;

	//void Start () {
	//    if (weaponObject)
	//    {
	//        weapon = weaponObject.GetComponent<PhysicalStuff>();
	//        cast = weaponObject.GetComponent<PhysicalCast>();

	//        transfWeapon = weaponObject.transform;        
	//    }

	//    transf = transform;
	//}
	
	//void Update () {
	//    if (weaponObject)
	//    {
	//        transfWeapon.position = transf.position;
	//        transfWeapon.rotation = transf.rotation;
	//    }
	//}

	//public void Equip(GameObject weapon)
	//{
	//    weaponObject = weapon;
	//    this.weapon = weaponObject.GetComponent<PhysicalStuff>();
	//    cast = weaponObject.GetComponent<PhysicalCast>();
	//    Destroy(weaponObject.GetComponent<InitializeCast>());
	//    transfWeapon = weaponObject.transform;
	//}

	//public void Unequip()
	//{
	//    Destroy(weaponObject);
	//}
}
