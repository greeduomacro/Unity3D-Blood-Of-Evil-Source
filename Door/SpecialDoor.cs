using UnityEngine;
using System.Collections;

public class SpecialDoor : ADoor {

	//[SerializeField]
	//private GameObject door2;
	//[SerializeField]
	//private GameObject door3;
	//[SerializeField]
	//private GameObject door4;
	//[SerializeField]
	//private GameObject door5;
	//[SerializeField]
	//private GameObject door6;

	//Transform transfDoor2;
	//Transform transfDoor3;
	//Transform transfDoor4;
	//Transform transfDoor5;
	//Transform transfDoor6;

	//[SerializeField]
	//private float maxWidthTranslate;
	//[SerializeField]
	//private float maxHeightTranslate;

	//public float MaxWidthTranslate { get { return maxWidthTranslate; } set { if (value > 0) maxWidthTranslate = value; } }
	//public float MaxHeightTranslate { get { return maxHeightTranslate; } set { if (value > 0) maxHeightTranslate = value; } }

	//float   widthTranslate;
	//float   heightTranslate;
	//bool    translateMiddleDoor;

	//void Start ()
	//{
	//    base.DoorStart();
	//    transfDoor2 = door2.transform;
	//    transfDoor3 = door3.transform;
	//    transfDoor4 = door4.transform;
	//    transfDoor5 = door5.transform;
	//    transfDoor6 = door6.transform;
	//}
	
	//protected override void Update ()
	//{
	//    if (open)
	//    {
	//        if (!translateMiddleDoor && widthTranslate < maxWidthTranslate)
	//        {
	//            widthTranslate += Time.deltaTime * openSpeed;
	//            transfDoor.Translate(-transfDoor.right * Time.deltaTime * openSpeed);
	//            transfDoor2.Translate(transfDoor.right * Time.deltaTime * openSpeed);
	//            transfDoor3.Translate(-transfDoor.right * Time.deltaTime * openSpeed);
	//            transfDoor4.Translate(transfDoor.right * Time.deltaTime * openSpeed);
	//            if (widthTranslate >= maxWidthTranslate)
	//                translateMiddleDoor = true;
	//        }
	//        else if (heightTranslate < maxHeightTranslate)
	//        {
	//            heightTranslate += Time.deltaTime * openSpeed;
	//            transfDoor5.Translate(transfDoor5.up * Time.deltaTime * openSpeed);
	//            transfDoor6.Translate(-transfDoor6.up * Time.deltaTime * openSpeed);
	//        }
	//    }
	//    else
	//    {
	//        if (!translateMiddleDoor && widthTranslate > 0)
	//        {
	//            widthTranslate -= Time.deltaTime * openSpeed;
	//            transfDoor.Translate(transfDoor.right * Time.deltaTime * openSpeed);
	//            transfDoor2.Translate(-transfDoor.right * Time.deltaTime * openSpeed);
	//            transfDoor3.Translate(transfDoor.right * Time.deltaTime * openSpeed);
	//            transfDoor4.Translate(-transfDoor.right * Time.deltaTime * openSpeed);
	//        }
	//        else if (heightTranslate > 0)
	//        {
	//            heightTranslate -= Time.deltaTime * openSpeed;
	//            if (heightTranslate <= 0)
	//                translateMiddleDoor = false;

	//            transfDoor5.Translate(-transfDoor5.up * Time.deltaTime * openSpeed);
	//            transfDoor6.Translate(transfDoor6.up * Time.deltaTime * openSpeed);
	//        }
	//    }
	//}
}
