using UnityEngine;
using System.Collections;

//public class WeaponPlacer : MonoBehaviour {

//    [SerializeField]
//    private GameObject   player;
//    [SerializeField]
//    private GameObject gameManager;
//    ObjectManager       objectManager;

//    AStuff              leftWeaponLastFrame;
//    AStuff              rightWeaponLastFrame;
//    AStuff              bothHandLastFrame;

//    //EquipmentSlot       leftHandSlot;
//    //EquipmentSlot       rightHandSlot;
//    //EquipmentSlot       bothHandSlot;

//    [SerializeField]
//    private Hand leftHand;
//    [SerializeField]
//    private Hand rightHand;

//    Animator            animator;

//    void Start ()
//    {
//        gameManager = GameObject.Find("Game Manager");
//        //ItemManager im = player.GetComponent<ItemManager>();

//        //leftHandSlot = im.Equipment.EquipmentSlots[(int)e_equipmentEmplacement.Left_Hand];
//        //rightHandSlot = im.Equipment.EquipmentSlots[(int)e_equipmentEmplacement.Right_Hand];
//        //bothHandSlot = im.Equipment.EquipmentSlots[(int)e_equipmentEmplacement.Both_Hand];

//        objectManager = gameManager.GetComponent<ObjectManager>();
//        animator = gameObject.transform.root.GetComponent<Animator>();
//    }
	
//    void Update ()
//    {
//    //    if (leftWeaponLastFrame != leftHandSlot.Item)
//    //        ModifyPhysicalEquipment(leftHand, leftHandSlot.Item);
//    //    if (rightWeaponLastFrame != rightHandSlot.Item)
//    //        ModifyPhysicalEquipment(rightHand, rightHandSlot.Item);
//    //    if (bothHandLastFrame != bothHandSlot.Item)
//    //        ModifyPhysicalEquipment(rightHand, bothHandSlot.Item);
//    //    leftWeaponLastFrame = leftHandSlot.Item;
//    //    rightWeaponLastFrame = rightHandSlot.Item;
//    //    bothHandLastFrame = bothHandSlot.Item;
//    }

//    void ModifyPhysicalEquipment(Hand hand, AStuff toCreate)
//    {
//        hand.Unequip();
//        animator.SetBool("IsDoubleHandedSword", false);

//        if (toCreate == null)
//            return;

//        if (toCreate.equipmentCategory == e_equipmentCategory.Weapon)
//        {
//            AWeapon newWeapon = toCreate as AWeapon;
//            if (newWeapon.WeaponType == e_weaponType.Sword && (newWeapon.equipmentEmplacement == e_equipmentEmplacement.Left_Hand || newWeapon.equipmentEmplacement == e_equipmentEmplacement.Right_Hand))
//            {
//                GameObject go = objectManager.Instantiate("Sword");
//                hand.Equip(go);
//            }
//            else if (newWeapon.WeaponType == e_weaponType.Sword && (newWeapon.equipmentEmplacement == e_equipmentEmplacement.Both_Hand))
//            {
//                animator.SetBool("IsDoubleHandedSword", true);
//                GameObject go = objectManager.Instantiate("LongSword");
//                hand.Equip(go);
//            }
//        }
//        if (toCreate.equipmentCategory == e_equipmentCategory.Cast)
//        {
//            GameObject go = objectManager.Instantiate("Cast");
//            go.GetComponent<PhysicalCast>().Cast = toCreate as Cast;
//            hand.Equip(go);
//        }
//    }
////}
