using UnityEngine;
using System.Collections;

public enum e_PlayerResource
{
	Life,
	Mana,
	Experience,

	SIZE
};

public class PlayerAttribute<TModuleType> : AEntityAttribute<TModuleType> where TModuleType : APlayer
{
	#region Attributes
	private PlayerResource<TModuleType> resources;				//module
	private PlayerCharacteristics<TModuleType> characteristics;	//module
	//private MinMaxf endurance;
	private CurrentMaxf experience;
	//private float timerForPotion;
	#endregion
	#region Properties
	public PlayerCharacteristics<TModuleType> Characteristics
	{
		get { return characteristics; }
		private set { characteristics = value; }
	}
	public PlayerResource<TModuleType> Resources
	{
		get { return resources; }
		private set { resources = value; }
	}
	//public MinMaxf Endurance {	get { return endurance; } 
	//                            private set { if (value.min >= 0 && value.max >= 0) endurance = value; } }
	//public float EnduranceMin {	get { return endurance.min; }
	//                                set { if (value >= 0) endurance.min = value; } }
	public CurrentMaxf Experience { get { return experience; } 
								private set { if (value.Current >= 0 && value.Max >= 0) experience = value; } }
	public float ExperienceCurrent {	get { return experience.Current; }
										set { if (value >= 0) experience.Current = value; resources.Resources[(int)e_PlayerResource.Experience].SelfIlluminTime = 0.5f; } }
	//public float TimerForPotion	{	get { return timerForPotion; }
	//                                set { if (value >= 0) timerForPotion = value; } }
	#endregion
	#region Builder
	public PlayerAttribute() { }
	public override void Initialize(TModuleType player)
	{
		base.SetModule(player, "Scripts/Behaviour/Attributes");

		this.life = new CurrentMaxf(438, 438);
		this.mana = new CurrentMaxf(150, 150);
		this.experience = new CurrentMaxf(0.0f, 100.0f);
		//this.endurance.min = 100;

		base.attributes[((int)e_entityAttribute.Move_Speed)] = 100;

		this.skillRemain = 3;
		//this.timerForPotion = 200;

		base.Modules = new AModule<TModuleType>[]
		{
			(this.resources = new PlayerResource<TModuleType>()),
			(this.characteristics = new PlayerCharacteristics<TModuleType>()),
		};

		base.Initialize(base.ModuleManager);
	}
	#endregion
	#region Virtual Functions
	public virtual void LevelUp()
	{
		++this.level;

		this.LifeAndManaToMax();
		//this.endurance.min = this.endurance.max;

		this.experience.Current -= this.experience.Max;
		this.experience.Max = (this.experience.Max * 1.2f) + 50;
		++this.skillRemain;
		this.Characteristics.CharacteristicRemain += 10;
	}
	#endregion
	#region Override Functions
	public override void GetDamaged(float damage)
	{
		//float damageDealed = damage - base.ModuleManager.Attributes.attributes[(int)e_entityAttribute.Defence] * 0.1f;

		//if (damageDealed > 0)
		//	base.ModuleManager.Attributes.Life.Current -= damageDealed;
	}
	public override void Update()
	{
		base.Update();
		
		this.LostRessource();

		//this.SetAttributesWithCharacteristics();
		
		this.Regeneration();

		this.Attack();

		if (this.experience.Current >= this.experience.Max)
			this.LevelUp();

		if (this.life.Current <= 0)
			this.Death();
	}
	#endregion
	#region Functions
	public void LifeAndManaToMax()
	{
		this.life.Current = this.life.Max;
		this.mana.Current = this.mana.Max;
	}

	public void Death()
	{
		this.life.Current = this.life.Max / 3;
		this.experience.Current = 0;
	}

	private void LostRessource()
	{
		LifeCurrent -= 1f;
		ManaCurrent -= 0.4f;
		ExperienceCurrent += 0.4f;
	}

	void Attack()
	{
		/*if (Input.GetMouseButton(0))
		{
			if (equipment.equipmentSlots[((int)e_equipmentEmplacement.Both_Hand)].equipped)
			{
				if (equipment.equipmentSlots[((int)e_equipmentEmplacement.Both_Hand)].item is CastHeal)
					skillMgr.PlayASkill("Simple Heal");
				else if (equipment.equipmentSlots[((int)e_equipmentEmplacement.Both_Hand)].item is CastDestruction)
					skillMgr.PlayASkill("Fireball");
				else if (equipment.equipmentSlots[((int)e_equipmentEmplacement.Both_Hand)].item is CastSkill)
					skillMgr.PlayASkill("War Scream");
			}
			//si arme as AWeapon
			Collider[] hitColliders = Physics.OverlapSphere(trans.position, 3, 1 << LayerMask.NameToLayer("Enemy"));
			for (short i =0; i < hitColliders.Length; i++)
			{
				Vector3 directionToTarget = transform.position - hitColliders[i].transform.position;
				float angle = Vector3.Angle(transform.forward, directionToTarget);
				if (Mathf.Abs(angle) > 90 && attackSpeed.CanDoMyAction())
				{
					int dmg = Random.Range(damage.min, damage.max + 1); 
					hitColliders[i].GetComponent<EnemyAttributes>().life.min -= dmg;
				}
			}
		}*/
	}

	#region Regeneration
	void Regeneration()
	{
		if (life.Current < life.Max)
			life.Current += Time.deltaTime * 0.01f * life.Max;
		if (life.Current > life.Max)
			life.Current = life.Max;

		if (mana.Current < mana.Max)
			mana.Current += Time.deltaTime * 0.01f * mana.Max;
		if (mana.Current > mana.Max)
			mana.Current = mana.Max;
	}
	#endregion
	void SetLife(float vita)
	{
		float hp = base.attributes[((int)e_entityAttribute.Life)];
		float hpPercent = base.attributes[((int)e_entityAttribute.Life_Percent)];

		this.life.Max = (int)((hp + 100 + vita * 15) * (1 + hpPercent / 100));
	}
	void SetDamage(float str)
	{
		float dmg = base.attributes[((int)e_entityAttribute.Damage)];
		float minDmg = base.attributes[((int)e_entityAttribute.Damage_Minimal)];
		float maxDmg = base.attributes[((int)e_entityAttribute.Damage_Maximal)];

		this.damage.Initialize((int)(dmg + (str * 2) + minDmg + 8), (int)(dmg + (str * 3) + maxDmg + 12));
	}
	#endregion
}
