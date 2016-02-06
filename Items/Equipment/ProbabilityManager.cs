using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public delegate void DelegateInstantiateWithIndex(int index);

public class ProbabilityManager : AServiceInitializer
{
	#region Functions
	public void		DisplayProbabilities(float[] probabilities)
	{
		string	stringPercent = "";

		for (uint i = 0; i < probabilities.Length; i++)
			stringPercent += probabilities[i] + "%\t";

		Debug.Log(stringPercent);
	}

	public void		ResetProbabilities(ref float[] probabilities)
	{
		for (short i =0; i < probabilities.Length; i++)
			probabilities[i] = 0;
	}

	public float[]		ProbabilityConvertorPartToPercent(float[] probabilities)
	{
		float[]		percentProbabilities = new float[probabilities.Length];
		float		total		= 0.0f;	

		for (uint x = 0; x < probabilities.Length; total += probabilities[x], x++);

		float		ratio = 1 / total * 100;
	
		for (uint i = 0; i < probabilities.Length; i++)
			percentProbabilities[i] = probabilities[i] * ratio;

		DisplayProbabilities(percentProbabilities);

		return percentProbabilities;
	}

	void	Update()
	{
		//MakeFallMyItemWithFunction( 
		//ProbabilityConvertorPartToPercent(new float[12] { 12.00f,  0.00f, 0.00f, 0.00f, 12.00f, 12.0f, 10.00f, 5.00f, 8.00f, 8.00f, 7.00f, 7.00f }));//,
	//	new Vector2(1.18f, 8.65f));
	}
	//int		GetProbabilityIndex(float[] probabilities)	{ return GetProbabilityIndex(probabilities, false); }
	public e_entityAttribute GetProbabilityIndex(List<EquipmentPossibleAttribute> possibleAttribute, bool displayPercent = false)	{
		int index = 0;
		float total = 0.0f;

		for (int x = 0; x < possibleAttribute.Count; x++)
			total += possibleAttribute[x].probability;

		float random = Random.Range(0.0f, total);

		for (float product = possibleAttribute[0].probability; random > product; ++index, product += possibleAttribute[index].probability) ;//!(random >= productTmp && random <= product)) //index++;

		if (displayPercent)
		{
			string percents = "";

			for (short i =0; i < possibleAttribute.Count; i++)
				percents += (possibleAttribute[i].probability / total * 100.00f) + "%\t";
			Debug.Log(percents);
		}

		return possibleAttribute[index].attribute;
	//	public e_entityAttribute attribute;
	//public float probability;
	}
	public int		GetProbabilityIndex(float[] probabilities, bool displayPercent = false) //delegate en default si il y en a on lappel
	{
		int			index		= 0;
		float		total		= 0.0f;	

		for (uint x = 0; x < probabilities.Length; total += probabilities[x], x++);

		float random = Random.Range(0.0f, total);

		for (float product = probabilities[0]; random > product; ++index, product += probabilities[index]);//!(random >= productTmp && random <= product)) //index++;

		if (displayPercent)
		{
			string		percents = "";

			for (uint i = 0; i < probabilities.Length; i++)
				percents += (probabilities[i] / total * 100.00f) + "%\t";
			Debug.Log(percents);
		}

		return index;
	}
	#endregion
	#region exemple d'utilisation
	public List<int>	MakeFallMyItemWithFunction(float[] probabilities) 									{ return MakeFallMyItemWithFunction(probabilities, new Vector2(1.0f, 1.0f)); }
	public List<int>	MakeFallMyItemWithFunction(float[] probabilities, DelegateInstantiateWithIndex del)	{ return MakeFallMyItemWithFunction(probabilities, new Vector2(1.0f, 1.0f),	del); }
	public List<int>	MakeFallMyItemWithFunction(float[] probabilities, Vector2 itemFallChance)
	{
		List<int> indexArray = new List<int>();

		float itemFall = Random.Range(itemFallChance.x, itemFallChance.y);

		for (; itemFall >= 1; itemFall--)
			indexArray.Add(GetProbabilityIndex(probabilities));

		if (itemFall < 1 && itemFall != 0)
			if (Random.Range(0.0f, 1.0f) <= itemFall)
				indexArray.Add(GetProbabilityIndex(probabilities));

		return indexArray;
	}
	public List<int> MakeFallMyItemWithFunction(float[] probabilities, Vector2 itemFallChance, DelegateInstantiateWithIndex del)
	{
		List<int>		indexArray =  MakeFallMyItemWithFunction(probabilities, itemFallChance);

		foreach (int val in indexArray)
			del(val);

		return indexArray;
	}

	//void		InstantiateItemWithIndex(int index)
	//{
	//	e_bonusTexture	bonus = (e_bonusTexture)index;
	//	Debug.Log(bonus);
	//}
	#endregion
}
