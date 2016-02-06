using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class SkillFilter
{
    public const byte FiltreDestruction = 1 << 0;
    public const byte FiltreHeal = 1 << 1;
    public const byte FiltrePower = 1 << 2;
    public const byte FiltreAll = 1 << 3;
}

public class SkillManagerGUI<TModuleType> : AGUIWindow<TModuleType> where TModuleType : APlayer
{

    private Inventory<TModuleType> inventory;
    private ItemManager<TModuleType> itemMgr;
    private Vector2 scrollPosition;

    //private Equipment equipment;
    //private AEntityAttribute player;
    //private ObjectAttributeToString objectAttributeToString;
    [HideInInspector]
    private int itemSelectedIndex;
    private bool rightClick;
    private byte filtre;

    void Awake()
    {
        //A CORRIGERitemMgr = Player.gameObject.GetComponent<APlayer>().Items;
        scrollPosition = Vector2.zero;

        //equipment = itemMgr.Equipment;

        //player = gameObject.GetComponent<AEntityAttribute>();

        //objectAttributeToString = new ObjectAttributeToString();

        GUIWindowInitialization(new Rect(0, 0, 1, 1));
        filtre = SkillFilter.FiltreAll;
    }

    void Start()
    {
        inventory = itemMgr.Inventory;

    }

    public override void Update()
    {
        rightClick = Input.GetMouseButton(1);
    }

    public override void OnGUIDrawWindow(int windowID)
    {
        Filter();
        Liste();
        Modifieur();

        /*if (itemSelected)
            ShowItemAttributes(itemSelectedIndex);*/
    }

    void Filter()
    {
        GUI.Box(MultiResolutions.Rectangle(0.8f, 0, 0.20f, 1.0f), "");
        GUI.Label(MultiResolutions.Rectangle(0.88f, 0.30f, 1f, 1f), MultiResolutions.Font(20.0f) + "<b><color=red>Filters</color></b></size>");
        if (GUI.Button(MultiResolutions.Rectangle(0.8f, 0.35f, 0.2f, 0.05f), MultiResolutions.Font(15.0f) + (((filtre & ItemExtension.FiltreAll) != 0) ? "<b><color=red>ALL</color></b>" : "ALL") + "</size>"))
            filtre = SkillFilter.FiltreAll;
        if (GUI.Button(MultiResolutions.Rectangle(0.8f, 0.40f, 0.2f, 0.05f), MultiResolutions.Font(15.0f) + (((filtre & ItemExtension.FiltreWeapon) != 0) ? "<b><color=red>DESTRUCTION</color></b>" : "DESTRUCTION") + "</size>"))
            filtre = SkillFilter.FiltreDestruction;
        if (GUI.Button(MultiResolutions.Rectangle(0.8f, 0.45f, 0.2f, 0.05f), MultiResolutions.Font(15.0f) + (((filtre & ItemExtension.FiltreClothe) != 0) ? "<b><color=red>HEAL</color></b>" : "HEAL") + "</size>"))
            filtre = SkillFilter.FiltreHeal;
        if (GUI.Button(MultiResolutions.Rectangle(0.8f, 0.50f, 0.2f, 0.05f), MultiResolutions.Font(15.0f) + (((filtre & ItemExtension.FiltreKeys) != 0) ? "<b><color=red>POWER</color></b>" : "POWER") + "</size>"))
            filtre = SkillFilter.FiltrePower;
    }

    void Liste()
    {
        GUI.Box(MultiResolutions.Rectangle(0.59f, 0, 0.20f, 1.0f), "");
        GUI.Label(MultiResolutions.Rectangle(0.67f, 0.3f, 1f, 1f), MultiResolutions.Font(20) + "<b><color=red>List</color></b></size>");
        GUI.Box(MultiResolutions.Rectangle(0.61f, 0.35f, 0.17f, 0.45f), "");

        int filtreLength = 0;

        for (short i =0; i < inventory.Items.Count; i++)
			if ((((filtre & ItemExtension.FiltreAll) != 0) || (filtre & inventory.Items[i].FiltreSkill) != 0) && inventory.Items[i] is Cast<TModuleType>)
                ++filtreLength;

        itemSelectedIndex = 0;
        scrollPosition = GUI.BeginScrollView(MultiResolutions.Rectangle(0.61f, 0.35f, 0.17f, 0.45f), scrollPosition,
            MultiResolutions.Rectangle(0f, 0f, 0.18f, 0.05f * filtreLength));

        int[] indexes = new int[((int)e_itemCategory.SIZE)];
        for (int offset = 0, i = 0; i < inventory.Items.Count; i++)
        {
            if (((filtre & ItemExtension.FiltreAll) != 0) || (filtre == inventory.Items[i].FiltreSkill))
            {
                string equippedString = "";
                //var stuff = inventory.stuffs[indexes[((int)e_itemCategory.Stuff)]] as AStuff;
				var stuff = inventory.Items[i] as Cast<TModuleType>;


                if (stuff != null)
                {
					equippedString = StuffGUI<TModuleType>.GetEquippedString(stuff);

                    if (GUI.Button(MultiResolutions.Rectangle(0, offset * 0.05f, 0.17f, 0.05f), MultiResolutions.Font(16) +
                        equippedString + inventory.Items[i].Name + "</size>"))
                    {
						AStuff<TModuleType> stuffEquipped = inventory.Items[i] as AStuff<TModuleType>;
						if (null != stuffEquipped && stuffEquipped is Cast<TModuleType> && stuff.Selected && stuffEquipped.equipmentEmplacement != e_equipmentEmplacement.Both_Hand)
                        {
                            if (rightClick)
                                stuff.equipmentEmplacement = e_equipmentEmplacement.Left_Hand;
                            else
                                stuff.equipmentEmplacement = e_equipmentEmplacement.Right_Hand;

                            itemMgr.Equip(stuff);
                            stuff.Selected = false;
                        }
                        inventory.Select(i);
                    }
                    offset++;
                }

                if (inventory.Items[i].Selected)
                    itemSelectedIndex = i;
            }
            inventory.Items[i].ModifyItemCategoryIndexes(ref indexes);
        }
        GUI.EndScrollView();
    }

    void Modifieur()
    {
        GUI.Box(MultiResolutions.Rectangle(0, 0.85f, 1.0f, 0.15f), "");
        GUI.Label(MultiResolutions.Rectangle(0.7f, 0.85f, 1, 1), MultiResolutions.Font(24) +
            "<color=red><b>Poids</b></color> : " + inventory.CurrentWeight + "/" + inventory.MaxWeight +
            "\t\t<color=red><b>Or</b></color> : " + inventory.Gold + "</size>");
    }

/*    void ShowItemAttributes(int itemSelectedIndex)
    {
        AStuff stuff = inventory.items[itemSelectedIndex] as AStuff;
        AStuff equippedStuff = equipment.equipmentSlots[((int)stuff.equipmentEmplacement)].Item as AStuff;
        string text = StuffGUI.GetItemColor(stuff) +
            "<b>" +
            MultiResolutions.Font(13) +
            StringExtension.FirstLetterMaj(inventory.items[itemSelectedIndex].name) +
            "\n</size>" + MultiResolutions.Font(12) +
            itemGUI.GetSubEquipmentCategory(stuff) +
            "</size></b></color>\n" +
            MultiResolutions.Font(12);

        if (equippedStuff != null && stuff.equipmentCategory == equippedStuff.equipmentCategory)
            text += stuff.GetTheWhiteAttributesCompared(equippedStuff, player) + "</size>";
        else
            text += stuff.GetTheWhiteAttributes(player) + "</size>";

        if (equippedStuff != null && stuff.equipmentEmplacement == equippedStuff.equipmentEmplacement && stuff != equippedStuff && equippedStuff.equipped != e_equipmentEquipped.NOT_EQUIPPED)
            text += objectAttributeToString.GetTheBlueAttributesCompared(equippedStuff);
        else
            text += stuff.GetTheBlueAttributes();

        ItemInteraction(text, stuff);
    }*/

	void ItemInteraction(string text, AStuff<TModuleType> stuff)
    {
        Rect rect = GUILayoutUtility.GetRect(new GUIContent(text), "box");
        rect.x = Screen.width * 0.41f;
        rect.height -= Screen.height * 0.015f;
        rect.y = Screen.height * 0.85f - rect.height;
        rect.width = Screen.width * 0.13f;

        for (short i =0; i < 2; i++)
            GUI.Box(rect, "");
        GUI.Box(rect, text);

        float posX = (rect.x + rect.width) / Screen.width;
        float posY = rect.y / Screen.height;

        for (short i =0; i < 3; i++)
            GUI.Box(MultiResolutions.Rectangle(posX, posY, 0.06f, 0.025f *
                (((inventory.Items[itemSelectedIndex].FiltreSkill == ItemExtension.FiltreClothe ||
                inventory.Items[itemSelectedIndex].FiltreSkill == ItemExtension.FiltreWeapon)
                 ? 1 : 0) + ((stuff.equipped == e_equipmentEquipped.NOT_EQUIPPED) ? 3 : 1) +
                 (((stuff.equipped == e_equipmentEquipped.NOT_EQUIPPED) &&
                 (stuff.equipmentEmplacement == e_equipmentEmplacement.Left_Hand ||
                 stuff.equipmentEmplacement == e_equipmentEmplacement.Right_Hand)) ? 1 : 0))), "");

        if (inventory.Items[itemSelectedIndex].FiltreSkill == ItemExtension.FiltreClothe || inventory.Items[itemSelectedIndex].FiltreSkill == ItemExtension.FiltreWeapon)
        {
            if (stuff.equipped == e_equipmentEquipped.NOT_EQUIPPED)
            {
                if (stuff.equipmentEmplacement == e_equipmentEmplacement.Left_Hand || stuff.equipmentEmplacement == e_equipmentEmplacement.Right_Hand)
                {
                    if (GUI.Button(MultiResolutions.Rectangle(posX, posY, 0.06f, 0.025f), MultiResolutions.Font(14) + "<color=#00FF00><b>Equiper(L)</b></color>" + "</size>"))
                    {
                        stuff.equipmentEmplacement = e_equipmentEmplacement.Left_Hand;
                        itemMgr.Equip(stuff);
                    }
                    posY += 0.025f;
                    if (GUI.Button(MultiResolutions.Rectangle(posX, posY, 0.06f, 0.025f), MultiResolutions.Font(14) + "<color=#00FF00><b>Equiper(R)</b></color>" + "</size>"))
                    {
                        stuff.equipmentEmplacement = e_equipmentEmplacement.Right_Hand;
                        itemMgr.Equip(stuff);
                    }
                    posY += 0.025f;
                }
                else
                {
                    if (GUI.Button(MultiResolutions.Rectangle(posX, posY, 0.06f, 0.025f), MultiResolutions.Font(14) + "<color=#00FF00><b>Equiper</b></color>" + "</size>"))
                        itemMgr.Equip(stuff);
                    posY += 0.025f;
                }
            }
            else
            {
                if (GUI.Button(MultiResolutions.Rectangle(posX, posY, 0.06f, 0.025f), MultiResolutions.Font(14) + "<color=#00FF00><b>Déséquiper</b></color>" + "</size>"))
                    itemMgr.Unequip(stuff);
                posY += 0.025f;
            }
        }
    }

}


    //private SkillManager skillMgr;
    //private Rect selectedPart;
    //private Vector2 scrollPosition;
    //private AEntityAttribute playerAttribute;
    //private float timerToClickOnPlus;

    //void Awake () {
    //    skillMgr = gameObject.GetComponent<SkillManager>();
    //    playerAttribute = gameObject.GetComponent<AEntityAttribute>();
    //    scrollPosition = Vector2.zero;

    //    GUIWindowInitialization(new Rect(0.225f, 0.005f, 0.56f, 0.99f), true);
    //}

//void BloodOfEvilGUI()
//    {
//        Border();
//        SkillTitle();
//        SelectedSkill();

//        Rect rectOfScrollView = new Rect(initPosition.x + 0.08f, initPosition.y, 0.30f, initPosition.height);
//        scrollPosition = GUI.BeginScrollView(MultiResolutions.Rectangle(rectOfScrollView),
//            scrollPosition, MultiResolutions.Rectangle(0, 0,
//            skillMgr.GetMaxLevelRequiered() * 0.015f - 0.005f + 0.023f * 1.1f,
//            initPosition.height * 0.02f));

//        int x = 0;
//        bool anySelected = false;
//        //int selectedIndex;
//        Rect rectPlus = new Rect(0, 0, 0, 0);
//        Skill skillPlus = new Skill();
//        timerToClickOnPlus += Time.deltaTime;
//        foreach (KeyValuePair<e_skillCategory, SortedDictionary<string, Skill>> skillsTmp in skillMgr.skills)
//        {
//            foreach (KeyValuePair<string, Skill> skill in skillsTmp.Value)
//            {
//                if (skill.Value.learned)
//                    GUI.backgroundColor = new Color(88f / 255f, 130f / 255f, 255f / 255f);
//                else if (playerAttribute.level < skill.Value.levelRequiered)
//                    GUI.backgroundColor = Color.red;
//                else
//                    GUI.backgroundColor = new Color(0, 1, 0);

//                Rect rectOfIcon = new Rect(skill.Value.levelRequiered * 0.015f - 0.005f,
//                    x * 0.11f + 0.027f,
//                    0.023f * 1.1f,
//                    0.050f * 1.1f);
//                if (GUI.Button(MultiResolutions.Rectangle(rectOfIcon), skill.Value.icon))
//                    skillMgr.Select(skill.Value);

//                GUI.backgroundColor = Color.black;

//                GUI.skin.label.alignment = TextAnchor.UpperCenter;
//                string levelRequiered = skill.Value.levelRequiered.ToString();
//                GUI.Label(MultiResolutions.Rectangle(rectOfIcon.x, rectOfIcon.y - 0.027f, rectOfIcon.width, rectOfIcon.height),
//                MultiResolutions.Font(14) +
//                ((playerAttribute.level < skill.Value.levelRequiered) ? "<color=red>" : "<color=#00FF00FF>") +
//                levelRequiered +
//                "</color></size>");

//                string levelCurrentPerMax = skill.Value.level.current.ToString() + " / " + skill.Value.level.max.ToString();
//                GUI.Label(MultiResolutions.Rectangle(rectOfIcon.x - rectOfIcon.width * 0.5f,
//                rectOfIcon.y + rectOfIcon.height, rectOfIcon.width * 2, rectOfIcon.height),
//                MultiResolutions.Font(12) +
//                ((skill.Value.level.current >= skill.Value.level.max) ? "<color=red>" : "<color=white>") +
//                levelCurrentPerMax + "</color></size>");
//                GUI.skin.label.alignment = TextAnchor.UpperLeft;

//                string skillName = skill.Value.name;
//                //GUI.Box(MultiResolutions.Rectangle(rectOfIcon.x, rectOfIcon.y, rectOfIcon.width * 2, rectOfIcon.height), "");
//                GUIExtension.OnHoverText(MultiResolutions.Rectangle(rectOfIcon.x + rectOfScrollView.x + dragPosition.x / Screen.width,
//                rectOfIcon.y + rectOfScrollView.y + dragPosition.y / Screen.height, rectOfIcon.width, rectOfIcon.height),
//                MultiResolutions.Rectangle(-rectOfScrollView.x - skillName.Length * 0.003f + 0.0115f - dragPosition.x / Screen.width, -rectOfScrollView.y - 0.005f - dragPosition.y / Screen.height, skillName.Length * 0.006f, .03f),
//                MultiResolutions.Font(14) + skillMgr.GetColorCategory(skill.Value.category) + "<b>" + skillName + "</b></color></size>");

//                if (skill.Value.selected)
//                {
//                    anySelected = true;
//                    //selectedIndex = x;
//                    rectPlus = new Rect(rectOfIcon.x + rectOfIcon.width - rectOfIcon.width * 0.4f,
//                        rectOfIcon.y + rectOfIcon.height - rectOfIcon.height * 0.4f,
//                        rectOfIcon.width * 0.4f,
//                        rectOfIcon.height * 0.4f);
//                    skillPlus = skill.Value;
//                }
//            }
//            x++;

//            if (anySelected && playerAttribute.level >= (skillPlus.levelRequiered + skillPlus.level.current))
//            {
//                Debug.Log(timerToClickOnPlus > 0.3f);
//                GUI.Button(MultiResolutions.Rectangle(rectPlus), "<b><color=#00FF00FF>+</color></b>");
//                if (GUIExtension.IsClicked(MultiResolutions.Rectangle(rectPlus.x + rectOfScrollView.x, rectPlus.y + rectOfScrollView.y, rectPlus.width, rectPlus.height))
//                )//&& timerToClickOnPlus > 0.3f)
//                {
//                    Debug.Log("plus");
//                    skillPlus.LevelUp(gameObject, playerAttribute);
//                    timerToClickOnPlus = 0;
//                }
//            }
//        }

//        GUI.EndScrollView();
//        GUI.DragWindow(new Rect(0, 0, 10000, 10000));
//    }
    //void OnActiveGUI() {
    //    foreach (KeyValuePair<e_skillCategory, SortedDictionary<string, Skill>> skillsTmp in skillMgr.skills)
    //        foreach (KeyValuePair<string, Skill> skill in skillsTmp.Value)
    //            if (skill.Value.learned)
    //                skill.Value.SetDescription(playerAttribute);
    //}

    //void Border()	{
    //    for (short i =0; i < 2; i++)
    //        GUI.Box(MultiResolutions.Rectangle(initPosition), "");

    //    GUI.Box(MultiResolutions.Rectangle(initPosition.x, initPosition.y, 0.08f, initPosition.height), "");
    //    GUI.Box(MultiResolutions.Rectangle(initPosition.x + 0.08f, initPosition.y, 0.30f, initPosition.height), "");

    //    selectedPart = new Rect(initPosition.x + 0.38f, initPosition.y, 0.18f, initPosition.height);
    //    GUI.Box(MultiResolutions.Rectangle(selectedPart), "");
    //    GUI.Box(MultiResolutions.Rectangle(selectedPart), "");

    //    isActive = GUIExtension.ExitButton(initPosition);
    //}

    //void SkillTitle()	{
    //    for (short i =0; i < (int)(e_skillCategory.SIZE); i++)
    //    {
    //        GUI.Box(MultiResolutions.Rectangle(initPosition.x, initPosition.y + i * 0.11f, initPosition.width - 0.18f, 0.11f), "");
    //        GUI.Label(MultiResolutions.Rectangle(initPosition.x + 0.008f, initPosition.y + i * 0.11f + 0.04f, 0.5f, initPosition.height),
    //        MultiResolutions.Font(19) + skillMgr.GetColorCategory((e_skillCategory)i) + "<b>" + ((e_skillCategory)i).ToString().Replace("_", " ") + "</b></color></size>");
    //    }
    //    //fonction qui recupere la length la plus longue des skills des e_skillCategory
    //}

    //public override void OnGUIDrawWindow(int windowID)
    //{
    //    OnActiveGUI();
    //}

    //void SelectedSkill()
    //{
    //    GUI.Label(MultiResolutions.Rectangle(selectedPart.x + 0.005f, selectedPart.y + 0.04f, selectedPart.width, selectedPart.height),
    //        MultiResolutions.Font(20) + "<b>Remain Skills : " + GUIExtension.GreenRedWhiteColorDependValue(playerAttribute.skillRemain) + "</b></size>");

    //    foreach (KeyValuePair<e_skillCategory, SortedDictionary<string, Skill>> skillsTmp in skillMgr.skills)
    //    {
    //        foreach (KeyValuePair<string, Skill> skill in skillsTmp.Value)
    //        {
    //            GUI.skin.label.wordWrap = true;
    //            if (skill.Value.selected)
    //            {
    //                GUI.skin.box.alignment = TextAnchor.MiddleCenter;
    //                Rect skillNameRect = new Rect(selectedPart.x, selectedPart.y + 0.11f, selectedPart.width, 0.05f);
    //                GUI.Box(MultiResolutions.Rectangle(skillNameRect),
    //                MultiResolutions.Font(20) + skillMgr.GetColorCategory(skill.Value.category) + "<b>" + skill.Value.name + "</b></color></size>");
    //                GUI.skin.box.alignment = TextAnchor.UpperCenter;

    //                Rect skillDescriptionRect = new Rect(skillNameRect.x + 0.01f, skillNameRect.y + skillNameRect.height + 0.01f, selectedPart.width - 0.01f, 0.2f);
    //                GUI.Label(MultiResolutions.Rectangle(skillDescriptionRect),
    //                MultiResolutions.Font(16) + "<color=#FF8000>" + skill.Value.description.description + "</color></size>");

    //                Rect skillCurrentLevelRect = new Rect(skillDescriptionRect.x, skillDescriptionRect.y + 0.11f, skillDescriptionRect.width, 1f);
    //                GUI.Label(MultiResolutions.Rectangle(skillCurrentLevelRect), skill.Value.description.levelInformation);
    //                // ca sera une scroll view par la suite
    //            }
    //        }
    //    }
    //}