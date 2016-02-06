using UnityEngine;
using System.Collections;

public class ADoor : MonoBehaviour {

	//[SerializeField]
	//private Animator animator;
	//[SerializeField]
	//private GameObject physicalDoor;
	//protected Transform transf;
	//protected Transform transfDoor;
	//[SerializeField]
	//protected float openSpeed;
	//public float OpenSpeed { get { return openSpeed; } private set { if (value > 0) openSpeed = value; } }

	//[SerializeField]
	//private int keyID = -1;
	//public int KeyID { get { return keyID; } private set { if (value < -1) { keyID = -1; } else { keyID = value; } } }
	//[SerializeField]
	//private int doorID = -1;
	//public int DoorID { get { return doorID; } private set { if (value < -1) { doorID = -1; locked = false; } else { doorID = value; locked = true; } } }

	//[SerializeField]
	//protected bool open;
	//public bool Open { get { return open; } private set { open = value; } }
	//private bool locked;
	//public bool Locked { get { return locked; } private set { locked = value; } }

	//protected void DoorStart ()
	//{
	//    transf = transform;
	//    transfDoor = physicalDoor.transform;
	//    locked = keyID != -1;        
	//}

	//public void Interaction(Inventory inventory)
	//{
	//    if (keyID == -1 || !locked)
	//        open = !open;
	//    else
	//    {
	//        Keys key;
	//        foreach (var item in inventory.Items)
	//        {
	//            key = item as Keys;
	//            if (key != null && key.ID == keyID)
	//            {
	//                inventory.RemoveItem(item);
	//                locked = false;
	//                open = !open;
	//                return;
	//            }
	//        }
	//    }

	//    animator.SetBool("Open", open);
	//}

	//public bool PlayerHasKey(Inventory inventory)
	//{
	//    Keys key;
	//    foreach (var item in inventory.Items)
	//    {
	//        key = item as Keys;
	//        if (key != null && key.ID == keyID)
	//            return true;
	//    }

	//    return false;
	//}
    
	//public void SetDoor(DoorData data)
	//{
	//    open = data.IsOpen;
	//    locked = data.IsLocked;
	//    keyID = data.KeyId;
	//    doorID = data.DoorId;
	//    openSpeed = data.OpenSpeed;
	//}

	//protected virtual void Update(){}
}
