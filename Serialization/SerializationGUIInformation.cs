using UnityEngine;
using System.Collections;
using System.IO;
using System;

//public sealed class SerializationGUIInformation
//{
//    #region Attributs
//    //private GUISkin skin;
//    private Texture2D screenshot;
//    private Vector2 scrollPosition;
//    private Rect rect;
//    private float offsetY;
//    private float borderWidth;
//    private bool[] haveClickedOnMyMainButtons;
//    #endregion
//    #region Propriétés
//    public Rect Rect
//    {
//        get { return rect; }
//        set { rect = value; }
//    }
//    public bool[] HaveClickedOnMyMainButtons
//    {
//        get { return haveClickedOnMyMainButtons; }
//        private set { haveClickedOnMyMainButtons = value; }
//    }
//    //public GUISkin Skin {	get { return skin; }
//    //						private set { skin = value; } }
//    public float OffsetY
//    {
//        get { return offsetY; }
//        private set { offsetY = value; }
//    }
//    public float BorderWidth
//    {
//        get { return borderWidth; }
//        set { if (value >= 0) borderWidth = value; }
//    }
//    public Vector2 ScrollPosition
//    {
//        get { return scrollPosition; }
//        set { scrollPosition = value; }
//    }
//    public Texture2D Screenshot
//    {
//        get { return screenshot; }
//        private set { screenshot = value; }
//    }
//    #endregion

//    public void Initialize(int buttonArraySize)
//    {
//        this.screenshot = new Texture2D(200, 200);
//        this.scrollPosition = Vector2.zero;
//        this.HaveClickedOnMyMainButtons = new bool[buttonArraySize];
//        this.offsetY = 0.05f;
//    }

//    public void RectInitialize()
//    {
//        rect = new Rect(0.75f, 0.7f, 0.2f, 0.05f);
//    }

//    public void ScreenShotInitialize(byte[] bytes)
//    {
//        this.screenshot.LoadImage(bytes);
//    }

//    public void MainInteractions(SerializationInformation info)
//    {
//        bool thereIsALastSave = info.Directories.Count > 0;
//        if (thereIsALastSave)
//        {
//            if (GUIExtension.ButtonAndHoverToChangeText(MultiResolutions.Rectangle(new Rect(this.Rect)),
//                    MultiResolutions.Font(20) + "CONTINUER</size>", MultiResolutions.Font(25) + "<b><color=white>CONTINUER</color></b></size>"))
//                this.UpdateClickedButtons(e_mainMenuButton.Continue);
//        }

//        if (GUIExtension.ButtonAndHoverToChangeText(MultiResolutions.Rectangle(new Rect(this.Rect.x, this.Rect.y + this.OffsetY, this.Rect.width, this.Rect.height)),
//            MultiResolutions.Font(20) + "NOUVEAU</size>", MultiResolutions.Font(25) + "<b><color=white>NOUVEAU</color></b></size>"))
//                this.UpdateClickedButtons(e_mainMenuButton.Nouveau);

//        else if (GUIExtension.ButtonAndHoverToChangeText(MultiResolutions.Rectangle(new Rect(this.Rect.x, this.Rect.y + this.OffsetY * 2, this.Rect.width, this.Rect.height)),
//            MultiResolutions.Font(20) + "CHARGER</size>", MultiResolutions.Font(25) + "<b><color=white>CHARGER</color></b></size>"))
//            this.UpdateClickedButtons(e_mainMenuButton.Charger);

//        else if (GUIExtension.ButtonAndHoverToChangeText(MultiResolutions.Rectangle(new Rect(this.Rect.x, this.Rect.y + this.OffsetY * 3, this.Rect.width, this.Rect.height)),
//            MultiResolutions.Font(20) + "CRÉDITS</size>", MultiResolutions.Font(25) + "<b><color=white>CRÉDITS</color></b></size>"))
//            this.UpdateClickedButtons(e_mainMenuButton.Credits);

//        else if (GUIExtension.ButtonAndHoverToChangeText(MultiResolutions.Rectangle(new Rect(this.Rect.x, this.Rect.y + this.OffsetY * 4, this.Rect.width, this.Rect.height)),
//             MultiResolutions.Font(20) + "QUITTER</size>", MultiResolutions.Font(25) + "<b><color=white>QUITTER</color></b></size>"))
//            Application.Quit();
//    }

//    public bool ContinueBorderInteraction(EventsEnterAndTabPressed events)
//    {
//        float posXWithBorderWidth = this.Rect.x - this.BorderWidth;

//        GUI.Box(MultiResolutions.Rectangle(posXWithBorderWidth, 0, this.BorderWidth, 1), "");

//        GUI.Box(MultiResolutions.Rectangle(posXWithBorderWidth, this.Rect.y - 0.15f, this.BorderWidth, 0.1f),
//                MultiResolutions.Font(17) + "<color=white>Reprendre la dernière sauvegarde ?</color></size>");

//        if (GUI.Button(MultiResolutions.Rectangle(posXWithBorderWidth + 0.015f + 0.12f, this.Rect.y + 0.15f, 0.06f, 0.1f),
//            MultiResolutions.Font(15) + "<color=white>(Tab) Non</color></size>") || events.TabPressed)
//            HaveClickedOnMyMainButtons[(int)e_mainMenuButton.Continue] = false;

//        else if (GUI.Button(MultiResolutions.Rectangle(posXWithBorderWidth + 0.015f, this.Rect.y + 0.15f, 0.06f, 0.1f),
//                 MultiResolutions.Font(15) + "<color=white>(Enter) Oui</color></size>") || events.EnterPressed)
//            return true;

//        return false;
//    }

//    public bool NewBorderInteraction(SerializationInformation info, EventsEnterAndTabPressed events)
//    {
//        float posXWithBorderWidth = this.Rect.x - this.BorderWidth;

//        GUI.Box(MultiResolutions.Rectangle(posXWithBorderWidth, 0, this.BorderWidth, 1), "");

//        GUI.Label(MultiResolutions.Rectangle(posXWithBorderWidth + 0.005f, this.Rect.y - 0.05f, 0.15f, 0.03f),
//            MultiResolutions.Font(17) + "<color=white>Nom de la partie : </color></size>");
//        info.PartyName = GUI.TextField(MultiResolutions.Rectangle(posXWithBorderWidth + 0.11f, this.Rect.y - 0.045f, 0.10f, 0.03f), info.PartyName, 25).Trim();

//        GUI.Box(MultiResolutions.Rectangle(posXWithBorderWidth, this.Rect.y - 0.15f, this.BorderWidth, 0.1f),
//                MultiResolutions.Font(17) + "<color=white>Lancer une nouvelle partie ?</color></size>");

//        if (GUI.Button(MultiResolutions.Rectangle(posXWithBorderWidth + 0.015f + 0.12f, this.Rect.y + 0.15f, 0.06f, 0.1f),
//            MultiResolutions.Font(15) + "<color=white>(Tab) Non</color></size>") || events.TabPressed)
//            this.HaveClickedOnMyMainButtons[(int)e_mainMenuButton.Nouveau] = false;

//        else if (GUI.Button(MultiResolutions.Rectangle(posXWithBorderWidth + 0.015f, this.Rect.y + 0.15f, 0.06f, 0.1f),
//                 MultiResolutions.Font(15) + "<color=white>(Enter) Oui</color></size>") || events.EnterPressed)
//        {
//            if (info.PartyName.Length > 0)
//            {
//                if (!info.Directories.Exists(x => x == DirectoryFunction.CombinePath(info.RepositoryPath, info.PartyName)))
//                    return true;
//                else
//                    ServiceLocator.Instance.ErrorDisplayStack.Add("The party name already exist", e_errorDisplay.Warning);
//            }
//            else
//                ServiceLocator.Instance.ErrorDisplayStack.Add("The party name is empty", e_errorDisplay.Warning);
//        }
//        return false;
//    }

//    public void DisplayLoadInformation(float posXWithBorderWidth, Rect usedRect, SerializationInformation info)
//    {
//        GUI.DrawTexture(MultiResolutions.Rectangle(usedRect), this.Screenshot);
//        GUI.skin.label.alignment = TextAnchor.UpperCenter;
//        GUI.Label(MultiResolutions.Rectangle(usedRect.x, usedRect.y + 0.25f, usedRect.width, usedRect.height),
//                  MultiResolutions.Font(20) + info.LoadingDataToDisplay.PartyName + ", Nordique, Niveau " +
//                info.LoadingDataToDisplay.Level + "\n" + String.Format("{0:D2}:{1:D2}:{2:D2}", info.Ts.Hours, info.Ts.Minutes, info.Ts.Seconds) +
//                  "\n" + (new DirectoryInfo(DirectoryFunction.CombinePath(info.RepositoryPath, info.PartyName))).LastAccessTime + "</size>");
//        GUI.skin.label.alignment = TextAnchor.UpperLeft;
//    }

//    public void LoadBorder(float posXWithBorderWidth)
//    {
//        GUI.Box(MultiResolutions.Rectangle(posXWithBorderWidth, 0, this.BorderWidth, 1), "");
//        GUI.Box(MultiResolutions.Rectangle(posXWithBorderWidth + 0.03f, 0.42f, 0.22f, 0.4f), "");
//    }

//    public bool LoadInteraction(Rect rectUsed, EventsEnterAndTabPressed events, SerializationInformation info, bool isInMainMenu)
//    {
//        GUI.Box(MultiResolutions.Rectangle(rectUsed),
//            MultiResolutions.Font(17) + "<color=white>" + ((isInMainMenu) ? "Charger une partie ?" : "Toute progression non sauvegardée sera perdu") + "</color></size>");

//        if ((GUI.Button(MultiResolutions.Rectangle(rectUsed.x + 0.165f, rectUsed.y + 0.05f, 0.06f, 0.1f),
//            MultiResolutions.Font(15) + "<color=white>(Tab) Non</color></size>") || events.TabPressed) &&
//            null != info.LoadingDataToDisplay)
//            this.HaveClickedOnMyMainButtons[(int)e_mainMenuButton.Charger] = false;

//        else if (GUI.Button(MultiResolutions.Rectangle(rectUsed.x + 0.075f, rectUsed.y + 0.05f, 0.06f, 0.1f),
//                 MultiResolutions.Font(15) + "<color=white>(Enter) Oui</color></size>") || events.EnterPressed)
//        {
//            if (info.PartyName.Length > 0)
//                return true;
//        }

//        return false;
//    }

//    public void ScrollLoadInformation(float posXWithBorderWidth, Rect scrollRect, SerializationInformation info)
//    {
//        this.ScrollPosition = GUI.BeginScrollView(MultiResolutions.Rectangle(scrollRect),
//                              this.ScrollPosition, MultiResolutions.Rectangle(0, 0, 0.22f, 0.05f * info.LoadingDatas.Count));
//        for (short i =0; i < info.LoadingDatas.Count; i++)
//        {
//            if (GUI.Button(MultiResolutions.Rectangle(0, 0.05f * i, 0.22f, 0.05f), info.LoadingDatas[i].SceneName))
//            {
//                info.LoadingDataToDisplay = info.LoadingDatas[i];
				
//                using (info.Fs = new FileStream(DirectoryFunction.CombinePath(info.RepositoryPath,
//                DirectoryFunction.CombinePath(info.LoadingDataToDisplay.PartyName, SaveInformation.image)), FileMode.Open, FileAccess.Read))
//                {
//                    byte[] imageData = new byte[info.Fs.Length];
//                    info.Fs.Read(imageData, 0, (int)info.Fs.Length);
//                    this.ScreenShotInitialize(imageData);
//                }
//                info.PartyName = info.LoadingDataToDisplay.PartyName;

//                info.Ts = TimeSpan.FromSeconds(info.LoadingDataToDisplay.PlayingTime);
//            }
//        }
//        GUI.EndScrollView();
//    }

//    private void UpdateClickedButtons(e_mainMenuButton whatButton)
//    {
//        this.resetClickedButtons();
//        this.HaveClickedOnMyMainButtons[(int)whatButton] = true;
//    }

//    public void UpdateClickedButtons(e_saveAndLoadMenu whatButton)
//    {
//        this.resetClickedButtons();
//        this.haveClickedOnMyMainButtons[(int)whatButton] = true;
//    }

//    private void resetClickedButtons()
//    {
//        for (byte i = 0; i < this.HaveClickedOnMyMainButtons.Length; i++)
//            this.HaveClickedOnMyMainButtons[i] = false;
//    }
//}
