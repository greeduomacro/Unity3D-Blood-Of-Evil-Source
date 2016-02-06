using UnityEngine;
using System.Collections;

public abstract class AEntityAttribute<TModuleType> : AModule<TModuleType> where TModuleType : MonoBehaviour
{
	#region Data Attributes
	protected int level; 
	public float[] attributes;
	protected MinMaxi damage; 
	protected float defence;
	protected CurrentMaxf life;
	protected CurrentMaxf mana;
	protected CurrentTimeTimer attackSpeed;
	protected CurrentTimeTimer castSpeed;
	protected float moveSpeed;
	protected Vector3 spawnPoint;
	protected float skillEffectPercent;
	protected byte skillRemain;
	protected float skillTimer;
	protected float skillTimerMultPercent;
	protected float castSpeedPercent;
	#endregion
	#region Data Properties
	public float[] Attributes
	{
		get { return attributes; }
		private set { attributes = value; }
	}
	public int Level {		get { return level; } 
							private set { if (value >= 0) level = value; } }
	public MinMaxi Damage {	get { return damage; } 
							private set { if (damage.Min >= 0 && damage.Max >= 0) damage = value; } }
	public float Defence {	get { return defence; }
							private set { if (value > 0) defence = value; } }
	public CurrentMaxf Life	{ get { return life; }
							private set { if (value.Current >= 0 && value.Max >= 0) life = value; } }
	public float LifeCurrent {	get { return life.Current; }
								set { life.Current = value; /* base.ModuleManager.Attributes.Resources.Resources[(int)e_PlayerResource.Life].SelfIlluminTime = 0.5f; } */} }
	public CurrentMaxf Mana	{ get { return mana; }
							private set { if (value.Current >= 0 && value.Max >= 0) mana = value; } }
	public float ManaCurrent	{	get { return mana.Current; }
									set { mana.Current = value; /* base.ModuleManager.Attributes.Resources.Resources[(int)e_PlayerResource.Mana].SelfIlluminTime = 0.5f; } */} }
	public CurrentTimeTimer AttackSpeed {	get { return attackSpeed; }
											private set { if (attackSpeed.Timer >= 0) attackSpeed = value; } }
	public CurrentTimeTimer CastSpeed {		get { return castSpeed; }
											private set { if (castSpeed.Timer >= 0) castSpeed = value; } }
	public float MoveSpeed {get { return moveSpeed; }
							private set { if (value >= 0) moveSpeed = value; } }
	public Vector3 SpawnPoint {	get { return spawnPoint; } 
								private set { spawnPoint = value; } }
	public float SkillEffectPercent {	get { return skillEffectPercent; }
										private set { if (value >= 0) skillEffectPercent = value; } }
	public byte SkillRemain {	get { return skillRemain; }
								set { skillRemain = value; } }
	public float SkillTimer	{	get { return skillTimer; }
								set { if (value >= 0) skillTimer = value; } }
	public float SkillTimerMultPercent {	get { return skillTimerMultPercent; }
											private set { if (skillTimerMultPercent >= 0) skillTimerMultPercent = value; } }
	public float CastSpeedPercent {	get { return skillTimerMultPercent; }
									private set { if (skillTimerMultPercent >= 0) skillTimerMultPercent = value; }	}
	#endregion

	public AEntityAttribute()
	{
		this.attributes = new float[((int)e_entityAttribute.SIZE)];
		this.damage = new MinMaxi(8, 12);
		this.life = new CurrentMaxf(0.0f, 0.0f);
		this.mana = new CurrentMaxf(0.0f, 0.0f);
		this.castSpeed = new CurrentTimeTimer(0.0f, 0.0f);
		this.attackSpeed = new CurrentTimeTimer(0.0f, 0.0f);

		this.attributes[((int)e_entityAttribute.Move_Speed)] = 20;
		this.attributes[((int)e_entityAttribute.Move_Speed_Percent)] = 100f;
		this.attributes[((int)e_entityAttribute.Move_Speed_Factor)] = 0.0005f;
		this.attributes[((int)e_entityAttribute.Defence_Percent)] = 100f;
		this.attributes[((int)e_entityAttribute.Fast_Cast_Percent)] = 100f;
		this.attributes[((int)e_entityAttribute.Attack_Speed_PercentPercent)] = 100f;

		this.attributes[((int)e_entityAttribute.Gold_Amount_Percent)] = 100f;
		this.attributes[((int)e_entityAttribute.Gold_Quantity_Percent)] = 100f;
		this.attributes[((int)e_entityAttribute.Gold_Rarity_Percent)] = 100f;
		this.attributes[((int)e_entityAttribute.Item_Quantity_Percent)] = 100f;
		this.attributes[((int)e_entityAttribute.Item_Rarity_Percent)] = 100f;
		this.attributes[((int)e_entityAttribute.Experience_Percent)] = 100f;

		this.skillEffectPercent = 1;
		this.skillTimer = 200;

		this.level = 1;
	}

	public override void Update()
	{
		this.attackSpeed.Update();
		this.castSpeed.Update();
		this.skillTimer += Time.deltaTime;
		this.CastSpeedPercent = this.attributes[((int)e_entityAttribute.Fast_Cast_Percent)] * 0.01f;
		this.skillTimerMultPercent = this.skillTimer * this.CastSpeedPercent;

		this.SetAttackSpeed();

		this.moveSpeed = this.attributes[((int)e_entityAttribute.Move_Speed)] * this.attributes[((int)e_entityAttribute.Move_Speed_Percent)] * 0.01f;

		base.Update();
	}

	public abstract void GetDamaged(float damage);

	void SetAttackSpeed()
	{
		float minSpeed = attributes[((int)e_entityAttribute.Attack_Speed_Percent)];
		if (minSpeed != 0)
			attackSpeed.Timer = minSpeed / ((attributes[((int)e_entityAttribute.Attack_Speed_PercentPercent)] * 0.01f));
		else
			attackSpeed.Timer = 0.5f;
	}
}

