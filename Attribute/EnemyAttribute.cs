using UnityEngine;
using System.Collections;

public class EnemyAttribute<TModuleType> : AEntityAttribute<TModuleType> where TModuleType : APlayer
{
	#region Attributs
	[HideInInspector]
	private AEntityAttribute<TModuleType> targetAttribute;
	private Vector2 offset;
	//private GameObject player;
	//private EnemyIA ai;
	[SerializeField]
	private EnemyLoot<TModuleType> enemyLoot;
	[SerializeField]
	//private Animator animator;

	private bool isLive = true;
	private float timerToBackToLife;
	#endregion
	#region Properties
	public AEntityAttribute<TModuleType> TargetAttribute
	{
		get { return targetAttribute; }
		set { if (value != null) targetAttribute = value; }
	}
	//public EnemyIA AI						{	get { return ai; } 
	//                                            private set { if (value != null) ai = value; } }
	public bool IsLive
	{
		get { return isLive; }
		private set { isLive = value; }
	}
	public float TimerToBackToLife
	{
		get { return timerToBackToLife; }
		private set { timerToBackToLife = value; }
	}
	#endregion

	public EnemyAttribute()
	{
		this.attributes[((int)e_entityAttribute.Attack_Speed_Percent)] = 1f;
		this.attributes[((int)e_entityAttribute.Range)] = 4f;
		this.EnemyDifficultyWithLevel();
		this.life.Current = this.life.Max;
	}

	void EnemyDifficultyWithLevel()
	{
		//this.level = GameObject.FindObjectOfType<PlayerAttribute>().Level;
		this.life.Max = (this.level * 10 * this.level) + 30;

		this.damage.Initialize((int)(this.level * this.level * 2f) + 15, (int)(this.level * this.level * 3f) + 18);
		this.moveSpeed += this.level * 2;
		this.attributes[((int)e_entityAttribute.Attack_Speed_Percent)] = 1f - this.level * 0.005f;
		this.attributes[(int)(e_entityAttribute.Range)] = 2f;

		this.enemyLoot = new EnemyLoot<TModuleType>();
		//this.enemyLoot.Initialize(this, this.trans);
	}

	public override void Update()
	{
		this.EnemyDifficultyWithLevel();

		base.Update();

		this.LiveFunction();
	}

	void LiveFunction()
	{
		if (this.life.Current > this.life.Max)
			this.life.Current = this.life.Max;
		if (this.life.Current <= 0)
		{
			if (isLive != false)
			{
				PlayerAttribute<TModuleType> playerAttribute = targetAttribute as PlayerAttribute<TModuleType>;
				this.enemyLoot.entityAttribute = this.targetAttribute;
				this.enemyLoot.InstantiateItems();

				if (null != playerAttribute)
					playerAttribute.ExperienceCurrent += 25 * level * attributes[(int)e_entityAttribute.Experience_Percent] * playerAttribute.attributes[(int)e_entityAttribute.Experience_Percent] * 0.0001f;

				//this.animator.SetBool("Die", true);
			}

			this.isLive = false;
			//MeshRenderer[] ChildrenRenderer = gameObject.GetComponentsInChildren<MeshRenderer>();
			//for (short i =0; i < ChildrenRenderer.Length; i++)
			//    ChildrenRenderer[i].enabled = false;


		}
		if (!isLive)
		{
			this.timerToBackToLife += Time.deltaTime;

			if (this.timerToBackToLife > 5)
			{
				this.isLive = true;
				this.timerToBackToLife = 0;

				//this.level = GameObject.FindObjectOfType<PlayerAttribute>().Level;
				this.EnemyDifficultyWithLevel();
				this.life.Current = this.life.Max;
				//this.trans.position = this.spawnPoint;

				//MeshRenderer[] ChildrenRenderer = gameObject.GetComponentsInChildren<MeshRenderer>();
				//for (short i =0; i < ChildrenRenderer.Length; i++)
				//    ChildrenRenderer[i].enabled = true;

				////this.animator.SetBool("Die", false);
				//this.rigidbody.velocity = Vector3.zero;
			}
		}
	}

	void OnGUI()
	{
		var oldMat = GUI.matrix;
		GUI.matrix = MultiResolutions.GetGUIMatrix();

		//if (this.isLive && this.player != null && Vector3.Angle(base.ModuleManager.transform.forward, base.ModuleManager.transform.position - this.trans.position) > 90)
		//{
		//    string text = life.min + " / " + life.max;

		//    offset = new Vector2(text.Length * -0.0025f, -0.05f);
		//    //Vector3 position = Camera.main.GetComponent<Camera>().WorldToViewportPoint(trans.position) + Vector3.up;
		//    //Rect rect = MultiResolutions.Rectangle(position.x + offset.x, 1 - position.y + offset.y, text.Length * 0.005f, 0.03f);

		//    //GUI.Box(rect, MultiResolutions.Font(12) + "<color=red>" + text + "</color></size>");
		//    //GUI.Box(rect, MultiResolutions.Font(12) + "<color=red>" + text + "</color></size>");
		//    //GUI.Box(rect, MultiResolutions.Font(12) + "<color=red>" + text + "</color></size>");
		//}

		GUI.matrix = oldMat;
	}

	public override void GetDamaged(float damage)
	{
		if (damage <= 0)
			return;

		//this.animator.SetBool("IsFiring", false);

		//this.life.min -= damage;
		//if (this.life.min > 0)
		//this.animator.SetTrigger("IsHit");
		//else
		//{
		//    //this.animator.SetBool("IsWalking", false);
		//    //this.animator.SetBool("IsRunning", false);
		//    //this.animator.SetBool("PrepareFire", false);
		//    //this.animator.SetBool("Die", true);
		//}
	}
}
