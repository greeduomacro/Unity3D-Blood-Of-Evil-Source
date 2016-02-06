using UnityEngine;
using System.Collections;

[System.Serializable]
public abstract class AConsommable<TModuleType> : AItem<TModuleType> where TModuleType : APlayer
{
	protected e_consommableCategory consommableCategory;
	protected e_consommableType consommableType;
	protected float timeRequieredToUseIt;
	protected float percentOfCare;

	#region Properties
	public e_consommableCategory ConsommableCategory {	get { return consommableCategory; }
														private set { consommableCategory = value; } }
	public e_consommableType ConsommableType		{	get { return consommableType; }
														private set { consommableType = value; } }
	public float TimeRequieredToUseIt				{	get { return timeRequieredToUseIt; }
														private set { if (value >= 0) timeRequieredToUseIt = value; } }
	public float PercentOfCare						{	get { return percentOfCare;  }
														private set { if (value >= 0) percentOfCare = value; } } 
	#endregion

	public AConsommable()
	{
		this.filtre = ItemExtension.FiltreConsommable;
	}

	public bool CanUse(PlayerAttribute<TModuleType> user)
	{
		bool canUseConso = true;//user.TimerForPotion >= timeRequieredToUseIt;

		//if (canUseConso)
		//	user.TimerForPotion = 0.0f;

		return canUseConso;
	}

	public abstract void Use(PlayerAttribute<TModuleType> user);
}










