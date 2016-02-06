using UnityEngine;
using System.Collections;

//[System.Serializable]
//public class AEntityData : ILoadableData
//{
//    #region Attributs
//    protected int level;
//    protected float[] attributes;
//    protected MinMaxi damage;
//    protected float defence;
//    protected CurrentMaxf life;
//    protected CurrentMaxf mana;
//    protected CurrentTimeTimer attackSpeed;
//    protected CurrentTimeTimer castSpeed;
//    ///protected SkillManager skillMgr;
//    protected float moveSpeed;
//    protected Vector3 spawnPoint;
//    protected float skillEffectPercent;
//    protected byte skillRemain;
//    protected float skillTimer;
//    protected float skillTimerMultPercent;
//    protected float castSpeedPercent;
//    protected Vector3 position;
//    protected Quaternion rotation;
//    #endregion
//    #region Properties
//    public int Level { get { return level; } private set { level = value; } }
//    public MinMaxi Damage { get { return damage; } private set { damage = value; } }
//    public float Defence { get { return defence; } private set { defence = value; } }
//    public CurrentMaxf Life { get { return life; } private set { life = value; } }
//    public CurrentMaxf Mana { get { return mana; } private set { mana = value; } }
//    public CurrentTimeTimer AttackSpeed { get { return attackSpeed; } private set { attackSpeed = value; } }
//    public CurrentTimeTimer CastSpeed { get { return castSpeed; } private set { castSpeed = value; } }
//    public float MoveSpeed { get { return moveSpeed; } private set { moveSpeed = value; } }
//    public Vector3 SpawnPoint { get { return spawnPoint; } private set { spawnPoint = value; } }
//    public float SkillEffectPercent { get { return skillEffectPercent; } private set { skillEffectPercent = value; } }
//    public byte SkillRemain { get { return skillRemain; } private set { skillRemain = value; } }
//    public float SkillTimer { get { return skillTimer; } private set { skillTimer = value; } }
//    public float SkillTimerMultPercent { get { return skillTimerMultPercent; } private set { skillTimerMultPercent = value; } }
//    public float CastSpeedPercent { get { return skillTimerMultPercent; } private set { castSpeedPercent = value; } }
//    public float[] Attributes { get { return attributes; } private set { attributes = value; } }
//    public Vector3 Position
//    {
//        get { return position; }
//        private set { position = value; }
//    }
//    public Quaternion Rotation
//    {
//        get { return rotation; }
//        private set { rotation = value; }
//    }
//    #endregion

//    public AEntityData()
//    {
//        attributes = new float[(int)e_entityAttribute.SIZE];
//    }

//    void ILoadableData.Load(object obj)
//    {
//        AEntityAttribute attri = obj as AEntityAttribute;

//        this.level = attri.Level;
//        this.attributes = attri.attributes;
//        this.damage = attri.Damage;
//        this.defence = attri.Defence;
//        this.life = attri.Life;
//        this.mana = attri.Mana;
//        this.attackSpeed = attri.AttackSpeed;
//        this.castSpeed = attri.CastSpeed;
//        this.moveSpeed = attri.MoveSpeed;
//        this.spawnPoint = attri.SpawnPoint;
//        this.skillTimer = attri.SkillTimer;
//        this.skillTimerMultPercent = attri.SkillTimerMultPercent;
//        this.castSpeedPercent = attri.CastSpeedPercent;

//        //this.position = attri.Position;
//        //this.rotation = attri.Rotation;
//    }

//}