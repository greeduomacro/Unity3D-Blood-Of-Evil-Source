using UnityEngine;
using System.Collections;

//public enum e_saveAndLoadMenu
//{
//    Sauvegarder,
//    Charger,
//    Parametres,
//    Commandes,
//    Aide,
//    Quitter,

//    SIZE,
//}

//public enum e_quitMenu
//{
//    None,
//    Menu_Principal,
//    Bureau,
//}


//public class SaveInGame<TModuleType> : AGUIWindow<TModuleType>
//{
//    private GUIWindowManager windowsMgr;
//    [SerializeField]	private GUISkin skin;
//    private EventsEnterAndTabPressed events;
//    private SerializationGUIInformation gui;
//    private SerializationInformation info;
//    private SerializationInteraction interactions;

//    private e_quitMenu quitMenu;
//    private string partyName;

//    void Awake()
//    {
//        this.windowsMgr = Player.gameObject.GetComponent<GUIWindowManager>() as GUIWindowManager;
//        this.GUIWindowInitialization(new Rect(0.12f, 0.2f, 1 - 0.24f, 0.6f), true);

//        this.gui = new SerializationGUIInformation();
//        this.events = new EventsEnterAndTabPressed();
//        this.info = new SerializationInformation();
//        this.interactions = GameObject.FindObjectOfType<SerializationInteraction>();

//        this.gui.Initialize(((int)e_saveAndLoadMenu.SIZE));
//        this.info.Initialize();

//        this.partyName = "";
//    }

//    public override void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.Escape))
//            this.windowsMgr.OpenGUI(e_GUIWindow.Save_And_Load);
//    }

//    public override void OnGUIDrawWindow(int windowID)
//    {
//        GUI.skin = this.skin;

//        this.Border();
//        this.LeftMenu();
//        this.MenuInteraction();
//    }

//    void MenuInteraction()
//    {
//        if (this.gui.HaveClickedOnMyMainButtons[(int)e_saveAndLoadMenu.Sauvegarder])
//        {
//            if (this.SaveInteraction())
//            {
//                this.info.InitializeLoadingDatas();
//                SerializationManager.Instance.SaveGame(DirectoryFunction.CombinePath(this.info.RepositoryPath, this.info.PartyName));
//            }
//        }

//        if (this.gui.HaveClickedOnMyMainButtons[(int)e_saveAndLoadMenu.Charger])
//            if (this.LoadInteraction())	
//                this.interactions.LoadParty(GameObject.Find("Player"), GameObject.Find("Game Manager"), this.info, false);

//        if (this.gui.HaveClickedOnMyMainButtons[(int)e_saveAndLoadMenu.Quitter])
//            this.QuitInteraction();
//    }

//    void QuitInteraction()
//    {
//        if (GUI.Button(MultiResolutions.Rectangle(new Rect(0.21f, 0.15f, 0.19f, 0.05f)), "Menu Principal"))
//            this.quitMenu = e_quitMenu.Menu_Principal;

//        else if (GUI.Button(MultiResolutions.Rectangle(new Rect(0.21f, 0.20f, 0.19f, 0.05f)), "Bureau"))
//            this.quitMenu = e_quitMenu.Bureau;

//        if (this.quitMenu != e_quitMenu.None)
//        {
//            GUI.Box(MultiResolutions.Rectangle(new Rect(0.2f, 0.43f, 0.56f, 0.05f)),
//                    MultiResolutions.Font(17) + "Toute progression non sauvegardée sera perdue</size>");

//            if (Input.GetKeyDown(KeyCode.Tab) ||
//                GUI.Button(MultiResolutions.Rectangle(0.55f, 0.48f, 0.06f, 0.1f),
//                MultiResolutions.Font(15) + "<color=white>(Tab) Non</color></size>"))
//                this.gui.HaveClickedOnMyMainButtons[(int)e_mainMenuButton.Quitter] = false;

//            else if (Input.GetKeyDown(KeyCode.Return) ||
//                     GUI.Button(MultiResolutions.Rectangle(0.35f, 0.48f, 0.06f, 0.1f),
//                     MultiResolutions.Font(15) + "<color=white>(Enter) Oui</color></size>"))
//            {
//                if (quitMenu == e_quitMenu.Menu_Principal)
//                {
//                    Application.LoadLevel(0);
//                    // A REVOIR 5.0 Destroy(GameObject.Find("Game Manager"));
//                    // A REVOIR 5.0 Destroy(GameObject.Find("Player"));
//                }
//                else
//                    Application.Quit();
//            }
//        }	
//    }

//    bool LoadInteraction()
//    {
//        GUI.Box(MultiResolutions.Rectangle(new Rect(0.21f, 0.07f, 0.19f, 0.35f)), "");

//        GUI.skin.button.alignment = TextAnchor.MiddleRight;
//        this.gui.Rect = new Rect(0.51f, 0.07f, 0.19f, 0.25f);
//        this.gui.BorderWidth = 0.3f;
//        float posXWithBorderWidth = this.gui.Rect.x - this.gui.BorderWidth;

//        this.gui.ScrollLoadInformation(posXWithBorderWidth, new Rect(0.125f, 0.07f, 0.22f, 0.25f), this.info);

//        if (null != this.info.LoadingDataToDisplay)
//            this.gui.DisplayLoadInformation(posXWithBorderWidth, new Rect(posXWithBorderWidth + 0.25f, 0.07f, 0.22f, 0.25f), this.info);
//        GUI.skin.button.alignment = TextAnchor.MiddleCenter;

//        if (this.gui.LoadInteraction(new Rect(posXWithBorderWidth + 0.1f, this.gui.Rect.y + 0.40f, this.gui.BorderWidth, 0.1f), this.events, this.info, false))
//            return true;

//        return false;
//    }

//    bool SaveInteraction()
//    {
//        GUI.Box(MultiResolutions.Rectangle(new Rect(0.21f, 0.09f, 0.19f, 0.35f)), "");

//        if (GUI.Button(MultiResolutions.Rectangle(new Rect(0.25f, 0.04f, 0.19f, 0.05f)), MultiResolutions.Font(20) + "[NOUVELLE SAUVEGARDE]</size>"))
//        {
//            if (this.partyName.Length > 0)
//            {
//                if (!info.Directories.Exists(x => x == DirectoryFunction.CombinePath(this.info.RepositoryPath, this.partyName)))
//                {
//                    this.info.PartyName = this.partyName;
//                    DirectoryFunction.CreateRepository(DirectoryFunction.CombinePath(this.info.RepositoryPath, this.partyName));
//                    DirectoryFunction.CreateRepository(DirectoryFunction.CombinePath(DirectoryFunction.CombinePath(this.info.RepositoryPath, this.partyName), SaveInformation.level));

//                    SerializationManager.Instance.SaveGame(DirectoryFunction.CombinePath(this.info.RepositoryPath, this.info.PartyName));

//                    LoadingData loadingData = new LoadingData();
//                    loadingData = SerializationTemplate.Load<LoadingData>(DirectoryFunction.CombinePath(
//                    DirectoryFunction.CombinePath(this.info.RepositoryPath, this.partyName), SaveInformation.loadingData));
//                    this.info.LoadingDatas.Add(loadingData);
//                    this.info.Directories.Add(this.partyName);

//                    this.info.InitializeLoadingDatas();
//                }
//                else
//                    ServiceLocator.Instance.ErrorDisplayStack.Add("The party name for save already exist", e_errorDisplay.Warning);
//            }
//            else
//                ServiceLocator.Instance.ErrorDisplayStack.Add("The party name for save is empty", e_errorDisplay.Warning);
//        }

//        GUI.Label(MultiResolutions.Rectangle(0.45f, 0.04f, 0.15f, 0.03f),
//        MultiResolutions.Font(17) + "<color=white>Nom de la partie : </color></size>");
//        this.partyName = GUI.TextField(MultiResolutions.Rectangle(0.45f + 0.10f, 0.04f, 0.15f, 0.03f), this.partyName, 25).Trim();

//        GUI.skin.button.alignment = TextAnchor.MiddleRight;
//        this.gui.Rect = new Rect(0.51f, 0.07f, 0.19f, 0.25f);
//        this.gui.BorderWidth = 0.3f;
//        float posXWithBorderWidth = this.gui.Rect.x - this.gui.BorderWidth;

//        this.gui.ScrollLoadInformation(posXWithBorderWidth, new Rect(0.125f, 0.09f, 0.22f, 0.25f), this.info);

//        if (null != this.info.LoadingDataToDisplay)
//            this.gui.DisplayLoadInformation(posXWithBorderWidth, new Rect(posXWithBorderWidth + 0.25f, 0.09f, 0.22f, 0.25f), this.info);
//        GUI.skin.button.alignment = TextAnchor.MiddleCenter;

//        if (this.gui.LoadInteraction(new Rect(posXWithBorderWidth + 0.1f, this.gui.Rect.y + 0.40f, this.gui.BorderWidth, 0.1f), this.events, this.info, false))
//        {
//            this.info.PartyName = this.info.LoadingDataToDisplay.PartyName;
//            return true;
//        }

//        return false;
//    }

//    void LeftMenu()
//    {
//        GUI.skin.button.alignment = TextAnchor.MiddleRight;

//        if (GUIExtension.ButtonAndHoverToChangeText(MultiResolutions.Rectangle(new Rect(0, 0.25f, 0.19f, 0.05f)),
//            MultiResolutions.Font(20) + "SAUVEGARDER</size>", MultiResolutions.Font(25) + "<b><color=white>SAUVEGARDER</color></b></size>"))
//            this.gui.UpdateClickedButtons(e_saveAndLoadMenu.Sauvegarder);

//        if (GUIExtension.ButtonAndHoverToChangeText(MultiResolutions.Rectangle(new Rect(0, 0.30f, 0.19f, 0.05f)),
//                 MultiResolutions.Font(20) + "CHARGER</size>", MultiResolutions.Font(25) + "<b><color=white>CHARGER</color></b></size>"))
//            this.gui.UpdateClickedButtons(e_saveAndLoadMenu.Charger);


//        if (GUIExtension.ButtonAndHoverToChangeText(MultiResolutions.Rectangle(new Rect(0, 0.35f, 0.19f, 0.05f)),
//            MultiResolutions.Font(20) + "PARAMÈTRES</size>", MultiResolutions.Font(25) + "<b><color=white>PARAMÈTRES</color></b></size>"))
//            this.gui.UpdateClickedButtons(e_saveAndLoadMenu.Parametres);

//        if (GUIExtension.ButtonAndHoverToChangeText(MultiResolutions.Rectangle(new Rect(0, 0.40f, 0.19f, 0.05f)),
//             MultiResolutions.Font(20) + "COMMANDES</size>", MultiResolutions.Font(25) + "<b><color=white>COMMANDES</color></b></size>"))
//            this.gui.UpdateClickedButtons(e_saveAndLoadMenu.Commandes);

//        if (GUIExtension.ButtonAndHoverToChangeText(MultiResolutions.Rectangle(new Rect(0, 0.45f, 0.19f, 0.05f)),
//             MultiResolutions.Font(20) + "AIDES</size>", MultiResolutions.Font(25) + "<b><color=white>AIDES</color></b></size>"))
//            this.gui.UpdateClickedButtons(e_saveAndLoadMenu.Aide);

//        if (GUIExtension.ButtonAndHoverToChangeText(MultiResolutions.Rectangle(new Rect(0, 0.50f, 0.19f, 0.05f)),
//            MultiResolutions.Font(20) + "QUITTER</size>", MultiResolutions.Font(25) + "<b><color=white>QUITTER</color></b></size>"))
//            this.gui.UpdateClickedButtons(e_saveAndLoadMenu.Quitter);

//        GUI.skin.button.alignment = TextAnchor.MiddleLeft;
//    }

//    void Border()
//    {
//        for (byte i = 0; i < 3; i++)
//        {
//            GUI.Box(MultiResolutions.Rectangle(this.InitPosition.x, this.InitPosition.y, 0.2f, this.InitPosition.height), "");
//            GUI.Box(MultiResolutions.Rectangle(this.InitPosition.x + 0.2f, this.InitPosition.y, 0.56f, this.InitPosition.height), "");
//        }
//    }

//}
