//using UnityEngine;
//using System.Collections;

//public enum e_interactionCase
//{
//    Skill_1,
//    Skill_2,
//    Skill_3,
//    Skill_4,
//    Skill_5,
//    Skill_6,
//    Skill_7,
//    Skill_8,
//    Skill_9,
//    Skill_10,
//    Skill_11,
//    Skill_12,
//    Skill_13,
//    Skill_14,
//    Skill_15,
//    Skill_16,
//    Skill_17,
//    Skill_18,
//    Skill_19,
//    Skill_20,
//    SIZE
//}

//public class InteractionCaseManager<TModuleType> : AGUIWindow<TModuleType> where TModuleType : APlayer
//{
//    private InteractionCase<TModuleType>[] cases;
//    //private ASkillManager skillMgr;
//    private PlayerAttribute<TModuleType> playerAttri;
//    private GUIWindowManager guiWindowManager;

//    void Awake() {
//        this.cases = new InteractionCase<TModuleType>[(int)(e_interactionCase.SIZE)];

//        for (short i =0; i < (int)(e_interactionCase.SIZE); i++)
//            this.cases[i] = new InteractionCase<TModuleType>();

//        this.GUIWindowInitialization(new Rect(0.20f, 0.89f, 0.64f, 0.11f), true, false);
//        guiWindowManager = Player.gameObject.GetComponent<GUIWindowManager<TModuleType>>() as GUIWindowManager<TModuleType>;
//    }

//    void MyUpdate(ASkillManager<TModuleType> skillMgr)
//    {
//        for (short i =0; i < (int)(e_interactionCase.SIZE); i++)
//            this.cases[i].Effect(skillMgr);
//    }

//    public void InitializeByPlayerAttribute(PlayerAttribute<TModuleType> playerAtt)	{
//        //this.skillMgr = gameObject.GetComponent<ASkillManager>() as ASkillManager;
//        this.playerAttri = playerAtt;

//        //cases[(int)(e_interactionCase.Skill_1)].key = KeyCode.Alpha1;
//        //cases[(int)(e_interactionCase.Skill_1)].skill = skillMgr.Get("Fireball");

//        //cases[(int)(e_interactionCase.Skill_2)].key = KeyCode.Alpha2;
//        //cases[(int)(e_interactionCase.Skill_2)].skill = skillMgr.Get("War Scream");

//        //cases[(int)(e_interactionCase.Skill_3)].key = KeyCode.Alpha3;
//        //cases[(int)(e_interactionCase.Skill_3)].skill = skillMgr.Get("Simple Heal");
//    }
	
//    // Update is called once per frame

//    public override void OnGUIDrawWindow(int windowID)
//    {
//        if (Time.timeScale == 0)
//            GUI.Box(MultiResolutions.Rectangle(this.InitPosition), "");

//        //for (short i =0; i < 2; i++)
//        //	GUI.Box(MultiResolutions.Rectangle(initPosition.x, initPosition.y + 0.01f, initPosition.width - 0.04f, initPosition.height - 0.01f), "");

//        //for (short i =0; i < (int)(e_interactionCase.SIZE); i++)
//        //{
//        //	if (GUI.Button(MultiResolutions.Rectangle(initPosition.x + i * 0.030f, initPosition.y + 0.01f, 0.030f, 0.062f), cases[i].GetContent()))
//        //		cases[i].Effect(skillMgr);
				
//        //	GUI.skin.label.alignment = TextAnchor.UpperCenter;
//        //	GUI.Label(MultiResolutions.Rectangle(initPosition.x + i * 0.030f, initPosition.y + 0.01f, 0.030f, 0.062f), "<size=10>" + cases[i].key.ToString() + "</size>");
//        //	GUI.skin.label.alignment = TextAnchor.UpperLeft;
//        //}
//        if (playerAttri.Player.Attributes.Characteristics.CharacteristicRemain > 0)
//        {
//            //Debug.Log("wseriously");
//            for (int d = 0; d < 3; d++)
//                GUI.Box(MultiResolutions.Rectangle(this.InitPosition.x + 0.6f, this.InitPosition.y + 0.060f + 0.01f, 0.02f, 0.040f), MultiResolutions.Font(20) + "<color=#00FF00FF><b>+</b></color></size>");
//            if (GUI.Button(MultiResolutions.Rectangle(this.InitPosition.x + 0.6f, this.InitPosition.y + 0.060f + 0.01f, 0.02f, 0.040f), ""))
//                this.guiWindowManager.OpenGUI(e_GUIWindow.Characteristic);		
//        }

//        GUI.backgroundColor = Color.yellow;
//        GUI.Button(MultiResolutions.Rectangle(this.InitPosition.x, this.InitPosition.y + 0.060f + 0.01f, 0.6f, 0.040f), "<color=yellow>" + MultiResolutions.Font(12) + Mathf.RoundToInt(this.playerAttri.Experience.Current) + " / " + Mathf.RoundToInt(this.playerAttri.Experience.Max) + " (level : " + playerAttri.Level + ")</size></color>");
//        //GUI.DragWindow(new Rect(0, 0, 10000, 10000));
//    }
//}

//public class InteractionCase<TModuleType> where TModuleType : APlayer
//{
//    //private KeyCode key;
//    private Skill skill;
//    //AConsommable consommable;

//    public InteractionCase()
//    {
//        this.skill = null;
//        //consommable = null;
//    }

//    public void Effect(ASkillManager<TModuleType> skillMgr)
//    {
//        /*if (Input.GetKeyDown(key))
//        {
//            if (null != this.skill)
//                skillMgr.PlayASkill(this.skill.name);
//            //if (null ! consommable)
//            //conso.effectpareil pour le consommable
//        }*/
//    }

//    public Texture2D GetContent()
//    {
//        if (null != this.skill)
//            return this.skill.icon;
//        //conso
//        return new Texture2D(0,0);
//    }
//}
