//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor;
//using System.Linq;

//using LanguageType = System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, string>>;
//using StringDictionary = System.Collections.Generic.Dictionary<string, string>;
//using System;

//public class LanguageManagerEditor : EditorWindow
//{
//    #region Attributes
//    private LanguageManager languageManager;
//    private e_language languageToModify;
//    private e_languageExtension extension;
//    private LanguageType dataBase;
//    private string newKey = "";
//    private string newValue = "";
//    #endregion
//    #region Properties
//    public LanguageManager LanguageManager { get { return languageManager; } set { languageManager = value; } }
//    #endregion

//    [MenuItem("Manager/Language/Modify Data Base")]
//    static void DataBase()
//    {
//        EditorWindow.GetWindow(typeof(LanguageManagerEditor));
//    }

//    void OnGUI()
//    {
//        this.title = "Database Modification";

//        if (false == Application.isPlaying)
//        {
//            EditorGUILayout.HelpBox("You cant make database modification while the application is not playing", MessageType.Info, true);
//            Repaint();
//            return;
//        }

//        this.InitializeIfDontExist();

//        this.DisplayGUIContent();
//    }

//    private void DisplayGUIContent()
//    {
//        EditorGUILayout.LabelField("");
//        EditorGUILayout.LabelField("Modify the language database : ");
//        EditorGUILayout.LabelField("");
//        this.ChoosenLanguageInteractionAndDisplay();
//        this.ChoosenExtensionInteractionAndDisplay();
//        EditorGUILayout.LabelField("");
//        this.DisplayRemove();
//        this.DisplayAdd();
//        this.DisplayReloadAndSave();
//        EditorGUILayout.EndVertical();
//    }

//    public void DisplayRemove()
//    {
//        EditorGUILayout.BeginVertical();
//        foreach (KeyValuePair<string, string> node in this.dataBase)
//        {
//            EditorGUILayout.BeginHorizontal();
//            this.DisplayLanguageNode(node.Key, node.Value);
//            if (GUILayout.Button("Remove"))
//            {
//                this.dataBase.Remove(node);
//                Debug.Log("Remove");
//                return;
//            }
//            EditorGUILayout.EndHorizontal();
//        }
//        EditorGUILayout.LabelField("");
//        EditorGUILayout.BeginHorizontal();
//    }

//    public void DisplayAdd()
//    {
//        this.DisplayAddLanguageNode();
//        if (GUILayout.Button("Add"))
//        {
//            this.dataBase.Add(new KeyValuePair<string, string>(this.newKey, this.newValue));
//            this.newKey = "";
//            this.newValue = "";
//            Debug.Log("Add");
//        }
//        EditorGUILayout.EndHorizontal();
//    }

//    public void DisplayReloadAndSave()
//    {
//        EditorGUILayout.LabelField("");
//        EditorGUILayout.BeginHorizontal();
//        if (GUILayout.Button("Reload"))
//        {
//            this.dataBase = this.languageManager.LoadEditorLanguageData(this.languageToModify, this.extension);
//            Debug.Log("Reload");
//        }
//        if (GUILayout.Button("Save"))
//        {
//            this.languageManager.SaveEditorLanguageData(this.languageToModify, this.extension, this.dataBase);
//            Debug.Log("Save");
//        }
//        EditorGUILayout.EndHorizontal();
//    }

//    public void DisplayLanguageNode(string textID, string text)
//    {
//        GUILayout.Box("Text ID");
//        EditorGUILayout.LabelField(textID);
//        GUILayout.Box("Text");
//        EditorGUILayout.LabelField(text);
//    }

//    public void DisplayAddLanguageNode()
//    {
//        GUILayout.Box("Text ID");
//        this.newKey = EditorGUILayout.TextField(this.newKey);
//        GUILayout.Box("Text");
//        this.newValue = EditorGUILayout.TextField(this.newValue);
//    }

//    public void ChoosenExtensionInteractionAndDisplay()
//    {
//        EditorGUILayout.BeginHorizontal();
//        EditorGUILayout.LabelField("Choosen Extension : ");
//        EditorGUILayout.LabelField(this.extension.ToString());

//        e_languageExtension languageExtensionTmp = this.extension;
//        this.extension = (e_languageExtension)EditorGUILayout.IntSlider((int)this.extension, 0, (int)e_languageExtension.SIZE - 1);

//        EditorGUILayout.EndHorizontal();
//    }

//    public void ChoosenLanguageInteractionAndDisplay()
//    {
//        EditorGUILayout.BeginHorizontal();
//        EditorGUILayout.LabelField("Choosen Language : ");
//        EditorGUILayout.LabelField(this.languageToModify.ToString());

//        e_language languageTmp = this.languageToModify;
//        this.languageToModify = (e_language)EditorGUILayout.IntSlider((int)this.languageToModify, 0, (int)e_language.SIZE - 1);

//        if (this.languageToModify != languageTmp)
//            this.dataBase = this.languageManager.ChangeEditorLanguageSelection(this.languageToModify, this.extension);

//        EditorGUILayout.EndHorizontal();
//    }

//    public void InitializeIfDontExist()
//    {
//        if (null == this.dataBase)
//        {
//            this.languageManager = GameObject.FindObjectOfType<PlayerLocalData>().LanguageManager;
//            this.languageToModify = this.languageManager.DefaultLanguage;
//            this.dataBase = this.languageManager.DefaultLanguageTexts.ToList();
//            this.extension = this.languageManager.ExtensionEnumeration;
//        }
//    }
//}
