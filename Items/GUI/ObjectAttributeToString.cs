using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public sealed class ObjectAttributeToString<TModuleType> where TModuleType : APlayer
{
	public string GetDisplayEquipmentAttribute(string attri)
	{
		//supprimer que la premiere occurence de _Percent, pas la 2eme
		return attri.Replace("_Percent", "").Replace("_", " ");
	}

	public string GetFinalSignEquipmentAttribute(string attri)
	{
		if (this.IsFloatEquipmentAttribute(attri))
			return "%";
		return "";
	}


	public bool IsFloatEquipmentAttribute(string attri)
	{
		return attri.IndexOf("_Percent") != -1;
	}

	//public string ComparedRandomToString(AStuff stuff, List<ObjectAttributeCompared> objectAttri)
	//{
	//	return
	//}

	public string ComparedDefinedToString(AStuff<TModuleType> stuff, List<ObjectAttributeCompared> objectAttri)
	{
		string result = "";
		int i = 0;

		if (stuff is AWeapon<TModuleType>)
		{
			result = "<b>Damage</b>: " + objectAttri[0].value + "-" + objectAttri[1].value +
				"(" + GUIExtension.IfNegativeReturnRedIfPositiveReturnGreen(objectAttri[0].difference) + "-" +
				GUIExtension.IfNegativeReturnRedIfPositiveReturnGreen(objectAttri[1].difference) + ")\n";

			i = 2;
		}

		return result + ComparedToString(objectAttri, i);
	}

	public string SimpleDefinedToString(AStuff<TModuleType> stuff, List<ObjectAttribute> objectAttri)
	{
		string result = "";
		int i = 0;

		if (stuff is AWeapon<TModuleType>)
		{
			result = "<b>Damage</b>: " + objectAttri[0].value + "-" + objectAttri[1].value + "\n";

			i = 2;
		}

		return result += DefinedToString(stuff, objectAttri, i);
	}

	public string DefinedToString(AStuff<TModuleType> stuff, List<ObjectAttribute> objectAttri, int i = 0, bool useColor = false)
	{
		string result = "";
		for (; i < objectAttri.Count; i++)
		{
			bool isFloatAttribute = IsFloatEquipmentAttribute(objectAttri[i].name);

			string color = "<color=white>";

			if (useColor)
			{
				if (objectAttri[i].value > 0)
					color = "<color=#00FF00FF>+";
				else if (objectAttri[i].value < 0)
					color = "<color=red>";
			}

			result += "<b>" + GetDisplayEquipmentAttribute(objectAttri[i].name) + "</b>: " + color + 
			((isFloatAttribute) ? objectAttri[i].value.ToString("F2") : objectAttri[i].value.ToString()) +
			GetFinalSignEquipmentAttribute(objectAttri[i].name) +  "</color>\n";
		}

		return result;
	}

	public string ComparedToString(List<ObjectAttributeCompared> objectAttri, int i = 0, bool whiteAttribute = true)
	{
		string result = "";

		for (; i < objectAttri.Count; i++)
		{
			string color = "";
			bool isFloatAttribute = IsFloatEquipmentAttribute(objectAttri[i].name);

			if (objectAttri[i].difference < 0)
				color = "<color=red>";
			else if (objectAttri[i].difference > 0)
				color = "<color=#00FF00FF>";

			result += 
					((whiteAttribute) ? "<color=white>" : "<color=#6666FFFF>") + "" +
					"<b>" + GetDisplayEquipmentAttribute(objectAttri[i].name) + "</b>:</color> " +
					((isFloatAttribute) ?
					objectAttri[i].value.ToString("F2") : objectAttri[i].value.ToString()) +
					((objectAttri[i].difference != 0) ?
					"(" + color +
					((isFloatAttribute) ?
					objectAttri[i].difference.ToString("F2") : (objectAttri[i].difference).ToString()) +
					GetFinalSignEquipmentAttribute(objectAttri[i].name) +
					"</color>)" : "") +
					"\n";
		}

		return result;
	}

	public string GetTheWhiteAttributesCompared<TModuleType>(AStuff<TModuleType> stuff, AStuff<TModuleType> equippedStuff, AEntityAttribute<TModuleType> player) where TModuleType : APlayer
	{
		string result = MultiResolutions.Font(12);

		return result; //A CORRIGER+ ComparedDefinedToString(stuff, stuff.GetTheWhiteAttributesCompared(equippedStuff, player)) + "</size></size>";
	}

	public List<ObjectAttributeCompared> GetTheBlueAttributesCompared(List<ObjectAttributeCompared> a, List<ObjectAttributeCompared> b)
	{
		List<ObjectAttributeCompared> result = new List<ObjectAttributeCompared>(a.Count);

		return result;
	}

	public string GetTheBlueAttributesCompared(AStuff<TModuleType> stuff, AStuff<TModuleType> equippedStuff)
	{
		string result = MultiResolutions.Font(12);

		result += ComparedToString(stuff.GetStuffComparatorThatItemHave(equippedStuff), 0, false);
		result += DefinedToString(stuff, stuff.GetStuffComparatorThatItemDontHave(equippedStuff, true), 0, true);
		result += DefinedToString(equippedStuff, equippedStuff.GetStuffComparatorThatItemDontHave(stuff, false), 0, true);

		return result + "</size>";
	}


	public List<ObjectAttributeCompared> AdditionListOfObjectAttributeCompared(List<ObjectAttributeCompared> a, List<ObjectAttributeCompared> b)
	{
		List<ObjectAttributeCompared> result = new List<ObjectAttributeCompared>(a.Count);
		
		a.ForEach((item) =>
		{
			result.Add(new ObjectAttributeCompared(item));
		});

		//faire ca pour les attributs bleu puis blanc
		for (byte ib = 0; ib < b.Count; ib++)
		{
			bool remplaced = false;

			for (byte ir = 0; ir < a.Count; ir++)
			{
				if (result[ir].name == b[ib].name)
				{
					remplaced = true;
					result[ir].value += b[ib].value;
				}
			}

			if (!remplaced)
				result.Add(new ObjectAttributeCompared(b[ib]));
		}

		return result;
	}

	public List<ObjectAttributeCompared> OperationListOfObjectAttribute(List<ObjectAttribute> a, List<ObjectAttribute> b, string myOperator = "+")
	{
		List<ObjectAttributeCompared> result = new List<ObjectAttributeCompared>(a.Count);

		a.ForEach((item) =>
		{
			result.Add(new ObjectAttributeCompared(item));
		});

		for (byte ib = 0; ib < b.Count; ib++)
		{
			bool remplaced = false;

			for (byte ir = 0; ir < a.Count; ir++)
			{
				if (result[ir].name == b[ib].name)
				{
					remplaced = true;

					if (myOperator == "+")
						result[ir].value += b[ib].value;
					else if (myOperator == "-")
						result[ir].value -= b[ib].value;
				}
			}

			if (!remplaced)
				result.Add(new ObjectAttributeCompared(b[ib]));
		}

		return result;
	}
}
