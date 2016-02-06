using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//[System.Serializable]
//public class SceneData
//{
//    #region Attributs
//    private string levelName;
//    private List<DoorData> doorDatas;
//    private List<EnemyData> enemiesData;
//    #endregion
//    #region Properties
//    public string LevelName { get { return levelName; } private set { levelName = value; } }
//    public List<DoorData> DoorDatas { get { return doorDatas; } private set { doorDatas = value; } }
//    public List<EnemyData> EnemiesData {	get { return enemiesData; }
//                                            private set { enemiesData = value; } }
//    #endregion
//    //private List<ChestData> chestDatas;
    

//    public SceneData()
//    {
//        doorDatas = new List<DoorData>();
//        enemiesData = new List<EnemyData>();
//        //chestDatas = new List<ChestData>();
//    }

//    public void Save()
//    {
//        this.levelName = Application.loadedLevelName;
        
//        this.doorDatas.Clear();
//        this.enemiesData.Clear();
//        //chestDatas.Clear();

//        ADoor[] doors = GameObject.FindObjectsOfType<ADoor>();
//        foreach (var door in doors)
//            this.doorDatas.Add(new DoorData(door));

//        //EnemyAttribute[] enemies = GameObject.FindObjectsOfType<EnemyAttribute>();
//        //foreach (var enemy in enemies)
//        //    this.enemiesData.Add(new EnemyData(enemy));
//    }

//    public void Load()
//    {
//        //EnemyAttribute[] enemies = GameObject.FindObjectsOfType<EnemyAttribute>();

//        //for (byte i = 0; i < enemies.Length; i++)
//        //    enemies[i].Load(this.enemiesData[i]);
//        //foreach (var door in this.doorDatas)
			
//    }
//}
