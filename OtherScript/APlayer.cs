using UnityEngine;
using System.Collections;

public enum e_playerClass
{
	Barbarian,
	Tanker,
	Necromancer,
	Druid,
	Mage,
	Hunter,
}

public abstract class APlayer : AEntity
{
	#region Attributes
	private Transform trans;
	private NavMeshAgent navMeshAgent;
	private PlayerGameObjects<APlayer> objects;			//module
	private PlayerGUIWindowManager windowManager;
	private AModule<APlayer>[] modules;
	#endregion
	#region Data Attributes
	private ASkillManager<APlayer> skills;					//module
	private PlayerAttribute<APlayer> attributes;				//module
	private ItemManager<APlayer> items;						//module
	private PlayerMovement<APlayer> movement;				//module
	private PlayerAnimation<APlayer> animations;				//module
	private PlayerInteractions<APlayer> interactions;		//module
	private PlayerUserInterfaceInformations<APlayer> userInterfaceInformations; // modules
	private PlayerCollisions<APlayer> collisions;			//module
	private PlayerJobManager<APlayer> jobs;					//module
	private e_playerClass category;
	private string username;
	#endregion
	#region Properties
	public Transform Trans { get { return trans; } private set { trans = value; } }
	public NavMeshAgent NavMeshAgent { get { return navMeshAgent; } private set { navMeshAgent = value; } }
	public PlayerGameObjects<APlayer> Objects { get { return objects; } private set { objects = value; } }
	public PlayerGUIWindowManager WindowManager { get { return windowManager; } private set { windowManager = value; } }
	public AModule<APlayer>[] Modules { get { return modules; } private set { modules = value; } }
	#endregion
	#region Data Properties
	public ASkillManager<APlayer> Skills { get { return skills; } private set { skills = value; } }
	public PlayerAttribute<APlayer> Attributes { get { return attributes; } private set { attributes = value; } }
	public ItemManager<APlayer> Items { get { return items; } private set { items = value; } }
	public PlayerMovement<APlayer> Movement { get { return movement; } private set { movement = value; } }
	public PlayerAnimation<APlayer> Animations { get { return animations; } private set { animations = value; } }
	public PlayerInteractions<APlayer> Interactions { get { return interactions; } private set { interactions = value; } }
	public PlayerUserInterfaceInformations<APlayer> UserInterfaceInformations { get { return userInterfaceInformations; } private set { userInterfaceInformations = value; } }
	public PlayerCollisions<APlayer> Collisions { get { return collisions; } private set { collisions = value; } }
	public PlayerJobManager<APlayer> Jobs { get { return jobs; } private set { jobs = value; } }
	public e_playerClass Category { get { return category; } private set { category = value; } }
	public string UserName { get { return username; } private set { username = value; } }
	#endregion
	#region Builder
	private void InitializeServiceLocator()
	{
		base.ServiceLocator = GameObject.FindObjectOfType<ServiceLocator>();
		base.ServiceLocator.Initialize();
		base.ServiceLocator = ServiceLocator.Instance;
		base.LanguageManager = new LanguageManager();
	}
	protected void InitializeSpecificFeaturesOfClass(e_playerClass category, ASkillManager<APlayer> skills, PlayerAttribute<APlayer> attribute)
	{
		this.username = "BLooDBuRNiNG";
		this.InitializeServiceLocator();

		this.category = category;

		this.skills = skills;
		this.attributes = attribute;
		this.items = new ItemManager<APlayer>();
		this.items.Initialize(this); //logiquement l'item manager aura une référence sur notre classe pour savoir si il peut équiper des objest spécifique a la classe
	
		this.InitializePlayer();
	}
	private void InitializePlayer()
	{
		this.trans = transform;
		this.navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
		this.windowManager = gameObject.GetComponent<PlayerGUIWindowManager>();

		this.modules = new AModule<APlayer>[]
		{
			(this.objects = new PlayerGameObjects<APlayer>()),
			this.attributes,
			this.items,
			(this.interactions = new PlayerInteractions<APlayer>()),
			(this.animations = new PlayerAnimation<APlayer>()),
			(this.movement = new PlayerMovement<APlayer>()),
			this.skills,
			(this.userInterfaceInformations = new PlayerUserInterfaceInformations<APlayer>()),
			(this.collisions = new PlayerCollisions<APlayer>()),
			(this.jobs = new PlayerJobManager<APlayer>()),
		};

		for (short i =0; i < this.modules.Length; i++)
			this.modules[i].Initialize(this);

		this.windowManager.InitializeByPlayer();
	}
	#endregion
	#region Functions
	protected void MyUpdate()
	{
		for (short i =0; i < this.modules.Length; i++)
			this.modules[i].Update();
	}

	public void LateUpdate()
	{
		this.movement.LateUpdate();
	}
	#endregion
}
