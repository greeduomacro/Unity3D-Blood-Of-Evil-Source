using UnityEngine;
using System.Collections;

//public class PlayerControllerGUIUpdater : MonoBehaviour {

//    private ItemManager itemMgr = null;

//    private PlayerControllerGUI gui;
//    Inventory playerInventory;

//    void Awake()
//    {
//        //itemMgr = GameObject.FindObjectOfType<ItemManager>();
//        gui = gameObject.GetComponent<PlayerControllerGUI>();
//        playerInventory = itemMgr.Inventory;
//    }

//    public void InteractionGUI(RaycastHit hit)
//    {
//        gui.findDoor = false;
//        gui.findWeapon = false;
//        gui.findKey = false;
//        gui.findLockedDoor = false;
//        gui.findCast = false;
//        gui.findChest = false;
//        gui.findGold = false;
//        if (hit.collider == null)
//            return;
//        if (hit.collider.transform.root.tag == "Door" && hit.distance <= 5)
//        {
//            ADoor door = hit.collider.transform.root.GetComponent<ADoor>();
//            if (!door.Locked || door.PlayerHasKey(playerInventory))
//                gui.findDoor = true;
//            else
//                gui.findLockedDoor = true;
//        }
//        if (hit.collider.transform.root.tag == "Stuff" && hit.distance <= 7) //raycast sur le layer default uniquement
//        {
//            gui.findWeapon = true;
//            gui.itemName = hit.collider.transform.root.GetComponent<CollectItemFromGround>().Item.Name;
//        }
//        if (hit.collider.transform.root.tag == "Key" && hit.distance <= 7)
//        {
//            gui.findKey = true;
//            gui.itemName = hit.collider.transform.root.GetComponent<CollectItemFromGround>().Item.Name;
//        }
//        if (hit.collider.transform.root.tag == "Cast" && hit.distance <= 7)
//        {
//            gui.findCast = true;
//            gui.itemName = hit.collider.transform.root.GetComponent<CollectItemFromGround>().Item.Name;
//        }

//        if (hit.collider.transform.root.tag == "Chest" && hit.distance <= 7)
//        {
//            if (!hit.collider.transform.root.GetComponent<ChestGUI>().Chest.IsOpen)
//                gui.itemName = "Open Uber Chest";
//            else
//                gui.itemName = "This chest have been already open";
//            gui.findChest = true;
//        }
//        if (hit.collider.transform.root.tag == "Gold" && hit.distance <= 7)
//        {
//            gui.findGold = true;
//            gui.itemName = hit.collider.transform.root.GetComponent<Gold>().GetName();
//        }
//    }
//}
