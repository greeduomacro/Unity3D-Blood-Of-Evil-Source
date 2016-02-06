using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public enum e_mainMenuButton
{
	Continue,
	Nouveau,
	Charger,
	Credits,
	Quitter,

	SIZE,
}

//public class MainMenu : MonoBehaviour {
//    [SerializeField]	private GameObject player;
//    [SerializeField]	private GameObject gameManager;
//    [SerializeField]	private GUISkin skin;
//    private EventsEnterAndTabPressed events;
//    private SerializationGUIInformation gui;
//    private SerializationInformation info;
//    private SerializationInteraction interactions;

//    void Awake()
//    {
//        this.gui = new SerializationGUIInformation();
//        this.events = new EventsEnterAndTabPressed();
//        this.info = new SerializationInformation();

//        this.gui.Initialize(((int)e_mainMenuButton.SIZE));
//        this.info.Initialize();
			
//        this.gameManager = GameObject.Instantiate(this.gameManager) as GameObject;
//        this.interactions = GameObject.FindObjectOfType<SerializationInteraction>();
//    }

//    void OnGUI()
//    {
//        var oldMat = GUI.matrix;

//        GUI.skin = this.skin;
//        GUI.matrix = MultiResolutions.GetGUIMatrix();
//        GUI.skin.button.alignment = TextAnchor.MiddleRight;
//        this.gui.RectInitialize();

//        this.gui.MainInteractions(this.info);

//        GUI.skin.button.alignment = TextAnchor.MiddleCenter;
//        this.BorderInteraction();
//        GUI.matrix = oldMat;
//    }

//    void BorderInteraction()
//    {
//        this.gui.BorderWidth = 0.22f;

//        if (this.gui.HaveClickedOnMyMainButtons[(int)e_mainMenuButton.Continue])
//            if (this.gui.ContinueBorderInteraction(this.events))
//                this.interactions.ContinueParty(this.player, this.gameManager, this.info);

//        if (this.gui.HaveClickedOnMyMainButtons[(int)e_mainMenuButton.Nouveau])
//            if (this.gui.NewBorderInteraction(this.info, this.events))
//                this.interactions.NewParty(this.player, this.gameManager, this.info);

//        if (this.gui.HaveClickedOnMyMainButtons[(int)e_mainMenuButton.Charger])
//            if (this.LoadBorderInteraction())
//                this.interactions.LoadParty(this.player, this.gameManager, this.info, true);
//    }

//    void Update()
//    {
//        events.Update();
//    }

//    bool LoadBorderInteraction()
//    {
//        this.gui.BorderWidth = 0.3f;
//        float posXWithBorderWidth = this.gui.Rect.x - this.gui.BorderWidth;

//        this.gui.LoadBorder(posXWithBorderWidth);
//        this.gui.ScrollLoadInformation(posXWithBorderWidth, new Rect(posXWithBorderWidth + 0.03f, 0.42f, 0.22f, 0.4f), this.info);

//        if (null != this.info.LoadingDataToDisplay)
//            this.gui.DisplayLoadInformation(posXWithBorderWidth, new Rect(posXWithBorderWidth + 0.03f, 0.05f, 0.22f, 0.25f), this.info);

//        if (this.gui.LoadInteraction(new Rect(posXWithBorderWidth, this.gui.Rect.y + 0.15f, this.gui.BorderWidth, 0.1f), this.events, this.info, true))
//            return true;
		
//        return false;
//    }

//}



