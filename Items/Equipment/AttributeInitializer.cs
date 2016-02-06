using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public sealed class AttributeInitializer<TModuleType> where TModuleType : APlayer
{
	private AEntityAttribute<APlayer> playerAttributes;

	public AEntityAttribute<APlayer> PlayerAttributes {	get { return playerAttributes; }
								private set { if (null != value) playerAttributes = value; } } 

	public void InitializeByPlayerAttribute(AEntityAttribute<APlayer> attri)	{
		this.playerAttributes = attri;
	}

	public void ModifyPlayerAttribute(float value, e_entityAttribute attri)		{
		this.playerAttributes.attributes[((int)attri)] += value;
	}

	public void GenerateDefinedAttributes(List<EquipmentPossibleAttribute> definedAttribute, AStuff<TModuleType> stuff)
	{
		for (int attribute = 0; attribute < definedAttribute.Count; attribute++)
		{
			e_entityAttribute whichAttribute = definedAttribute[attribute].attribute;
			
			definedAttribute[((int)attribute)].value =
			stuff.GenerateAttributeValue(definedAttribute[((int)attribute)]);
			stuff.whiteAttributes.Add(new EquipmentAttribute(definedAttribute[((int)attribute)].value, whichAttribute));
			if (!(stuff.IsFloatEquipmentAttribute(whichAttribute)))
				stuff.whiteAttributes[stuff.whiteAttributes.Count - 1].Value = Mathf.Round(stuff.whiteAttributes[stuff.whiteAttributes.Count - 1].Value);
		}
	}

	public void GenerateRandomAttributes(List<EquipmentPossibleAttribute> possibleAttribute, AStuff<TModuleType> stuff)
	{
		int qualityAttribbute = ((int)stuff.equipmentQuality);

		if (possibleAttribute.Count < qualityAttribbute)
		{
			Debug.Log("It is not possible to generate more attributes for : " + stuff.Name + " becaue you dont have enought random attributes initialized");
			qualityAttribbute = possibleAttribute.Count;
		}

		int attribute = 0;

		for (int x = 0; x < qualityAttribbute; x++)
		{
			bool goToMyGoTo;
			e_entityAttribute whichAttribute = ServiceLocator.Instance.ProbabilityManager.GetProbabilityIndex(possibleAttribute);
			do
			{
				whichAttribute = ServiceLocator.Instance.ProbabilityManager.GetProbabilityIndex(possibleAttribute);

				attribute = 0;

				for (short i =0; i < possibleAttribute.Count; i++)
					if (possibleAttribute[i].attribute == whichAttribute)
						attribute = i;

				goToMyGoTo = false;
				for (short i =0; i < stuff.blueAttributes.Count; i++)
					if (whichAttribute == stuff.blueAttributes[i].WhichAttribute)
						goToMyGoTo = true;
			} while (goToMyGoTo);

			possibleAttribute[((int)attribute)].value =
			stuff.GenerateAttributeValue(possibleAttribute[((int)attribute)]);
			stuff.blueAttributes.Add(new EquipmentAttribute(possibleAttribute[((int)attribute)].value, whichAttribute));
			if (!(stuff.IsFloatEquipmentAttribute(whichAttribute)))
				stuff.blueAttributes[stuff.blueAttributes.Count - 1].Value = Mathf.Round(stuff.blueAttributes[stuff.blueAttributes.Count - 1].Value);
		}
	}

	public AStuff<TModuleType> GenerateStuffAttribute(e_equipmentQuality quality, int levelRequiered, AStuff<TModuleType> stuff)
	{
		stuff.equipmentQuality = quality;
		stuff.levelRequiered = levelRequiered;
		stuff.importanceOAttributefValue *= 1.0f + (stuff.levelRequiered * 0.15f);
		//stuff.name = stuff.equipmentEmplacement.ToString();

		stuff.Price *= (int)(1 + (stuff.levelRequiered * 0.25f)) * (((int)stuff.equipmentQuality) + 1);

		this.GenerateDefinedAttributes(stuff.fixedAttributes, stuff);
		this.GenerateRandomAttributes(stuff.randomAttributes, stuff);

		stuff.blueAttributes.Sort((EquipmentAttribute a, EquipmentAttribute b) => { return a.WhichAttribute.ToString().CompareTo(b.WhichAttribute.ToString()); }); 

		stuff.fixedAttributes = null;
		stuff.randomAttributes = null;

		return stuff;
	}

	public AStuff<TModuleType> GenerateStuffAttribute(AStuff<TModuleType> stuff)
	{
		return GenerateStuffAttribute(stuff.equipmentQuality, stuff.levelRequiered, stuff);
	}

	public void ModifyPlayerAttributesWithEquipment(AStuff<TModuleType> stuff, int negativeOrPositiveOne)
	{
		for (short i =0; i < stuff.blueAttributes.Count; i++)
			ModifyPlayerAttribute(stuff.GetAttributeValue(stuff.blueAttributes[i]) * negativeOrPositiveOne, stuff.blueAttributes[i].WhichAttribute);

		for (short i =0; i < stuff.whiteAttributes.Count; i++)
			ModifyPlayerAttribute(stuff.GetAttributeValue(stuff.whiteAttributes[i]) * negativeOrPositiveOne, stuff.whiteAttributes[i].WhichAttribute);
	}

	public static void AddFixedAttribute(e_entityAttribute attri, Vector2 val, AStuff<TModuleType> stuff)
	{
		stuff.fixedAttributes.Add(new EquipmentPossibleAttribute(attri, val));
	}
}
