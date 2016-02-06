using UnityEngine;
using System.Collections;

//public class MenuInGame<TModuleType> : AGUIWindow<TModuleType> where TModuleType : APlayer
//{
//    private GUIWindowManager windowsMgr;

//    void Awake()	{
//        this.windowsMgr =Player.gameObject.GetComponent<GUIWindowManager>() as GUIWindowManager;

//        this.GUIWindowInitialization(new Rect(0.425f, 0.10f, 0.15f, 0.25f), true);
//    }

//    public override void OnGUIDrawWindow(int windowID)
//    {
//        GUI.matrix = MultiResolutions.GetGUIMatrix();

//        for (short i =0; i < 3; i++)
//            GUI.Box(MultiResolutions.Rectangle(InitPosition),
//                MultiResolutions.Font(20) + "<color=red><b>Main Menu</b></color></size>");

//        if (GUI.Button(MultiResolutions.Rectangle(this.InitPosition.x, this.InitPosition.y + 0.1f, this.InitPosition.width, 0.05f),
//            MultiResolutions.Font(18) + "<color=yellow><b>Inventaire</b></color></size>"))
//            this.windowsMgr.OpenGUI(e_GUIWindow.Inventory);

//        if (GUI.Button(MultiResolutions.Rectangle(this.InitPosition.x, this.InitPosition.y + 0.15f, this.InitPosition.width, 0.05f),
//        MultiResolutions.Font(18) + "<color=yellow><b>Sorts</b></color></size>"))
//            this.windowsMgr.OpenGUI(e_GUIWindow.Skill);

//        if (GUI.Button(MultiResolutions.Rectangle(this.InitPosition.x, this.InitPosition.y + 0.20f, this.InitPosition.width, 0.05f),
//        MultiResolutions.Font(18) + "<color=yellow><b>Caractéristiques</b></color></size>"))
//            this.windowsMgr.OpenGUI(e_GUIWindow.Characteristic);

//        this.isActive = GUIExtension.ExitButton(this.InitPosition);
//        GUI.DragWindow(new Rect(0, 0, 10000, 10000));
//    }
//}
