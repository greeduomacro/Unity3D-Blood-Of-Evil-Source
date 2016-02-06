using UnityEngine;
using System.Collections;

//public class PlayerControllerGUI : MonoBehaviour {

//    PlayerController controller;
//    public bool findDoor { get; set; }
//    public bool findWeapon { get; set; }
//    public bool findKey { get; set; }
//    public bool findLockedDoor { get; set; }
//    public bool findCast { get; set; }
//    public bool findChest { get; set; }
//    public bool findGold { get; set; }
//    public string itemName { get; set; }

//    void Start () {
//        controller = GetComponent<PlayerController>();
//    }

//    void OnGUI()
//    {
//        var oldMat = GUI.matrix;
//        GUI.matrix = MultiResolutions.GetGUIMatrix();
//        if (findDoor)
//            GUI.Label(MultiResolutions.Rectangle(0.5f, 0.5f, 0.25f, 0.5f), controller.Take + ": Interaction");
//        if (findLockedDoor)
//            GUI.Label(MultiResolutions.Rectangle(0.5f, 0.5f, 0.25f, 0.5f), "Locked");
//        if (findWeapon)
//            GUI.Label(MultiResolutions.Rectangle(0.5f, 0.5f, 0.25f, 0.5f), controller.Take + ": Pick up " + itemName);
//        if (findKey)
//            GUI.Label(MultiResolutions.Rectangle(0.5f, 0.5f, 0.25f, 0.5f), controller.Take + ": Pick up " + itemName);
//        if (findCast)
//            GUI.Label(MultiResolutions.Rectangle(0.5f, 0.5f, 0.25f, 0.5f), controller.Take + ": Pick up " + itemName);
//        if (findChest)
//            GUI.Label(MultiResolutions.Rectangle(0.5f, 0.5f, 0.25f, 0.5f), controller.Take + ": " + itemName);
//        if (findGold)
//            GUI.Label(MultiResolutions.Rectangle(0.5f, 0.5f, 0.25f, 0.5f), controller.Take + ": " + itemName);

//        GUI.matrix = oldMat;
//    }
//}
