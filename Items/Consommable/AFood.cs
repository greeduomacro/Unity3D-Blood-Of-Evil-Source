using UnityEngine;
using System.Collections;

public abstract class AFood<TModuleType> : AConsommable<TModuleType> where TModuleType : APlayer
{
	protected int occurence;
	protected float timeBetweenOccurence;

	#region Properties
	public int Occurence {	get { return occurence; }
							private set { if (0 < value) occurence = value; } }
	public float TimeBetweenOccurence {	get { return timeBetweenOccurence; }
										private set { if (0 > value) timeBetweenOccurence = value; } }
	public void OccurenceDecrementation() { --occurence; }
	#endregion

	public AFood()
	{
		this.consommableCategory = e_consommableCategory.Food;
		this.timeRequieredToUseIt = 7f;
		this.percentOfCare = 0.1f;
		this.occurence = 4;
		this.timeBetweenOccurence = 0.4f;

		this.weight = 3;
		this.name = "Nourriture de ";
		this.description = "Vous soigne à 4 reprises différentes";
		this.mesh = "Consommable";
	}

	public override void Use(PlayerAttribute<TModuleType> user)
	{
		GameObject activeFoodObject = ServiceLocator.Instance.ObjectManager.Instantiate("ActiveFood");

		//A CORRIGER activeFoodObject.AddComponent<ActiveFood<TModuleType>>().Initialize(user, this);
	}
}
