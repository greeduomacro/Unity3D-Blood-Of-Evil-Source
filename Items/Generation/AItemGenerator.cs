using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AItemGenerator<TModuleType> where TModuleType : APlayer
{
	#region Attributes
	protected float[] probabilities;
	protected MinMaxi levelRequired;
	protected MinMaxi level;
	protected e_itemInstantiateType instantiateType;
	public MinMaxf NumberOfItemGenerated;
#endregion
	#region Properties
	public float[] Probabilities {	get { return probabilities; }
									private set { probabilities = value; } }
	public MinMaxi LevelRequired {	get { return levelRequired; }
									private set { levelRequired = value; } }
	public MinMaxi Level {	get { return level; }
							private set { level = value; } }
	public e_itemInstantiateType InstantiateType {	get { return instantiateType; }
													set { instantiateType = value; }}
	//public MinMaxf NumberOfItemGenerated {	get { return numberOfItemGenerated; }
	//										private set { numberOfItemGenerated = value; } }
	#endregion

	public AItemGenerator()
	{
		this.levelRequired = new MinMaxi();
		this.level = new MinMaxi();
		this.instantiateType = e_itemInstantiateType.OnGround;
		this.NumberOfItemGenerated = new MinMaxf();
	}

	public void ResetProbabilities()
	{
		ServiceLocator.Instance.ProbabilityManager.ResetProbabilities(ref this.probabilities);
	}
	public void ResetProbabilities(float[] probabilities)
	{
		this.probabilities = probabilities;
	}

	public void AddFixedItems(AItemContainer<TModuleType> itemContainer, List<AItem<TModuleType>> items)
	{
		if (items != null)
			foreach (AItem<TModuleType> item in items)
				itemContainer.AddItem(item);
	}
}