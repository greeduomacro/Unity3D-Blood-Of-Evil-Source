using UnityEngine;
using System.Collections;

public sealed class ActiveFood<TModuleType> : MonoBehaviour where TModuleType : APlayer
{
	private AFood<TModuleType> food;
	private float timerForOccurence;
	private PlayerAttribute<APlayer> player;

	#region Properties
	public AFood<TModuleType> Food
	{
		get { return food; }
						private set { if (null != value) food = value; } }
	public float TimeForOccurence {	get { return timerForOccurence; }
									private set { if (0 < value) timerForOccurence = value; } } 
	public PlayerAttribute<APlayer> Player {	get { return player; }
									private set { if (null != value) player = value; } }
	#endregion

	public void Initialize(PlayerAttribute<APlayer> player, AFood<TModuleType> food)
	{
		this.food = food;
		this.player = player;
	}

	void Update()
	{
		this.timerForOccurence += Time.deltaTime;

		if (this.timerForOccurence > this.food.TimeBetweenOccurence)
		{
			this.timerForOccurence = 0f;
			this.food.OccurenceDecrementation();

			this.Care();

			if (this.food.Occurence == 0)
				Destroy(gameObject);
		}
	}

	void Care()
	{
		e_consommableType type = food.ConsommableType;

		if (type == e_consommableType.Life || type == e_consommableType.Life_And_Mana)
			player.LifeCurrent += player.Life.Max * this.food.PercentOfCare;
		if (type == e_consommableType.Mana || type == e_consommableType.Life_And_Mana)
			player.ManaCurrent += player.Mana.Max * this.food.PercentOfCare;
	}
}
