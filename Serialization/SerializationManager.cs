using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

//public sealed class SerializationManager : MonoBehaviour {
//    #region Attributs
//    private static SerializationManager  instance;
//    public static SerializationManager  Instance
//    {
//        get 
//        { 
//            if (instance == null)
//                Debug.LogError("class not initialized yet");

//            return instance;
//        }
//        private set { instance = value; }
//    }

//    [SerializeField]
//    private float minutesPerSave;
//    private string repositoryPath;
//    private string path;
//    private string fileName;

//    private GameData gameData;
//    private SceneData sceneData;
//    #endregion
//    //public SceneData SceneData { get { return sceneData; } set { sceneData = value; } }
//    public void Initialize(string repositoryPath, string fileName, bool isANewParty)
//    {
//        this.repositoryPath = repositoryPath;
//        this.fileName = fileName;
//        this.path = DirectoryFunction.CombinePath(this.repositoryPath, this.fileName);

//        DirectoryFunction.CreateRepository(this.repositoryPath);
//        DirectoryFunction.CreateRepository(this.path);
//        DirectoryFunction.CreateRepository(DirectoryFunction.CombinePath(this.path, SaveInformation.level));

//        this.gameData = new GameData();
//        this.sceneData = new SceneData();

//        if (!isANewParty)
//            this.ReadingSaveFiles();

//        this.StartCoroutine(this.SaveEveryMinutes());
//    }

//    void Start()
//    {
//        Instance = this;
//    }

//    private IEnumerator SaveEveryMinutes()
//    {
//        while (true)
//        {
//            yield return new WaitForSeconds(this.minutesPerSave * 60);
//            this.SaveGame(path);
//        }
//    }

//    public void LoadGame()
//    {
//        this.gameData.Load();
//        this.sceneData.Load();
//    }
	
//    public void SaveGame(string path)
//    {
//        this.gameData.Save();
//        this.sceneData.Save();
//        this.WrittingSaveFiles(((path != "") ? path : this.path));

//        Debug.Log("save");
//    }

//    private void WrittingSaveFiles(string path)
//    {
//        Application.CaptureScreenshot(DirectoryFunction.CombinePath(path, SaveInformation.image));

//        SerializationTemplate.Save<GameData>(DirectoryFunction.CombinePath(path, SaveInformation.file), this.gameData);
//        //SerializationTemplate.Save<LoadingData>(DirectoryFunction.CombinePath(path, SaveInformation.loadingData),
//            //GameObject.FindObjectOfType<PlayerAttribute>().LoadingData);
//        SerializationTemplate.Save<SceneData>(DirectoryFunction.CombinePath(DirectoryFunction.CombinePath(path, SaveInformation.level), Application.loadedLevelName), this.sceneData);
//    }

//    private void ReadingSaveFiles()
//    {
//        this.gameData = SerializationTemplate.Load<GameData>(DirectoryFunction.CombinePath(path, SaveInformation.file));

//        //if (null == this.gameData)
//        //	this.gameData = new GameData();
	
//        this.sceneData = SerializationTemplate.Load<SceneData>(DirectoryFunction.CombinePath(
//            DirectoryFunction.CombinePath(path, SaveInformation.level), this.gameData.PlayerData.LoadingData.SceneName));
		
//        this.LoadGame();
//    }
//}