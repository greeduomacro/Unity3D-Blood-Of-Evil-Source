using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public abstract class AStuff<TModuleType> : AItem<TModuleType> where TModuleType : APlayer
{
	public List<EquipmentAttribute>	blueAttributes;	//Random
	public List<EquipmentAttribute> whiteAttributes; //Defined
	public List<EquipmentPossibleAttribute> randomAttributes;
	public List<EquipmentPossibleAttribute> fixedAttributes;
	public e_equipmentCategory equipmentCategory;
	public e_equipmentEmplacement equipmentEmplacement;
	public e_equipmentQuality equipmentQuality;
	public e_equipmentEquipped equipped;
	public float importanceOAttributefValue;
	public int levelRequiered;
	//public bool equipped;

	public AStuff()
	{
		this.importanceOAttributefValue = 1.0f + (this.levelRequiered * 0.15f);
		this.fixedAttributes = new List<EquipmentPossibleAttribute>();
		this.randomAttributes = new List<EquipmentPossibleAttribute>();
		this.blueAttributes = new List<EquipmentAttribute>();
		this.whiteAttributes = new List<EquipmentAttribute>();
	}

	#region generation d'attribut
	public void AddRandomAttribute(e_entityAttribute attri, float proba, Vector2 val)	{
		this.randomAttributes.Add(new EquipmentPossibleAttribute(attri, proba, val));
	}

	public void AddFixedAttribute(e_entityAttribute attri, Vector2 val)	{
		this.fixedAttributes.Add(new EquipmentPossibleAttribute(attri, val));
	}
	#endregion

	public float GetAttributeValue(EquipmentAttribute attri)
	{
		return attri.Value;
	}

	public float GenerateAttributeValue(EquipmentPossibleAttribute attri)
	{
		return Random.Range(attri.values.x, attri.values.y) * this.importanceOAttributefValue;
	}

	public bool IsFloatEquipmentAttribute(e_entityAttribute attri)	{
		return attri.ToString().IndexOf("_Percent") != -1;
	}

	public string GetDisplayEquipmentAttribute(e_entityAttribute attri)
	{
		//supprimer que la premiere occurence de _Percent, pas la 2eme
		return attri.ToString().Replace("_Percent", "").Replace("_", " ");
	}

	public string GetFinalSignEquipmentAttribute(e_entityAttribute attri)
	{
		if (this.IsFloatEquipmentAttribute(attri))
			return "%";
		return "";
	}

	public string GetCorrectEquipmentAttributeValue(EquipmentAttribute equipmentttribute)
	{
		//if l'enum de possible attribute contient _Percent alors on F2 (afficche que 2 décimal) sa valeur sinon on la round
		if (this.IsFloatEquipmentAttribute(equipmentttribute.WhichAttribute))
			return equipmentttribute.Value.ToString("F2");
		return (equipmentttribute.Value).ToString();
	}

	public List<ObjectAttribute> GetStuffComparatorThatItemDontHave(AStuff<TModuleType> equippedStuff, bool itemEquipped)
	{
		//string result = "";
		List<ObjectAttribute> objectAttri = new List<ObjectAttribute>();

		for (int stuffIndex = 0; stuffIndex < this.blueAttributes.Count; stuffIndex++)
		{
			bool finded = false;
			int equippedStuffIndex = 0;

			for (; equippedStuffIndex < equippedStuff.blueAttributes.Count && !finded; equippedStuffIndex++)
				if (this.blueAttributes[stuffIndex].WhichAttribute == equippedStuff.blueAttributes[equippedStuffIndex].WhichAttribute)
					finded = true;

			if (!finded)
				objectAttri.Add(new ObjectAttribute(this.blueAttributes[stuffIndex].WhichAttribute.ToString(), ((itemEquipped) ? this.blueAttributes[stuffIndex].Value : -this.blueAttributes[stuffIndex].Value)));
		}
		return objectAttri;
		//return result;
	}

	public List<ObjectAttributeCompared> GetStuffComparatorThatItemHave(AStuff<TModuleType> equippedStuff)
	{
		List<ObjectAttributeCompared> objectAttri = new List<ObjectAttributeCompared>();

		for (int stuffIndex = 0; stuffIndex < this.blueAttributes.Count; stuffIndex++)
		{
			for (int equippedStuffIndex = 0; equippedStuffIndex < equippedStuff.blueAttributes.Count; equippedStuffIndex++)
			{
				if (this.blueAttributes[stuffIndex].WhichAttribute == equippedStuff.blueAttributes[equippedStuffIndex].WhichAttribute)
				{
					float difference = this.GetAttributeValue(this.blueAttributes[stuffIndex]) - equippedStuff.GetAttributeValue(equippedStuff.blueAttributes[equippedStuffIndex]);

					objectAttri.Add(new ObjectAttributeCompared(this.blueAttributes[stuffIndex].WhichAttribute.ToString(), difference, this.GetAttributeValue(this.blueAttributes[stuffIndex])));
				}
			}
		}
		return objectAttri;
	}

	public List<ObjectAttributeCompared> GetheBlueAttributes()	{
		List<ObjectAttributeCompared> objectAttri = new List<ObjectAttributeCompared>();

		for (int blueAttributeIndex = 0; blueAttributeIndex < this.blueAttributes.Count; blueAttributeIndex++)
			objectAttri.Add(new ObjectAttributeCompared(this.blueAttributes[blueAttributeIndex].WhichAttribute.ToString(), 0, this.blueAttributes[blueAttributeIndex].Value));

		return objectAttri;
	}

	public List<ObjectAttribute> GetTheWhiteAttributes()	{
		List<ObjectAttribute> objectAttri = new List<ObjectAttribute>();
		int whiteAttributeIndex = 0;

		var weapon = this as AWeapon<TModuleType>;
		if (weapon != null)
		{
			whiteAttributeIndex = 2;

			objectAttri.Add(new ObjectAttribute("Damage Minimal", this.whiteAttributes[0].Value));
			objectAttri.Add(new ObjectAttribute("Damage Maximal", this.whiteAttributes[1].Value));
		}
		else
		{
			var cast = this as ACast<TModuleType>;
			if (cast != null)
				objectAttri.Add(new ObjectAttribute("Mana Cost", cast.ManaCost));
		}
		for (; whiteAttributeIndex < this.whiteAttributes.Count; whiteAttributeIndex++)
				objectAttri.Add(new ObjectAttribute(this.whiteAttributes[whiteAttributeIndex].WhichAttribute.ToString(), this.whiteAttributes[whiteAttributeIndex].Value));

		objectAttri.Add(new ObjectAttribute("Level Requiered", this.levelRequiered));

		return objectAttri;
	}

	public List<ObjectAttributeCompared> GetTheWhiteAttributesCompared(AStuff<TModuleType> equippedStuff, AEntityAttribute<APlayer> player)
	{
		List<ObjectAttributeCompared> objectAttri = new List<ObjectAttributeCompared>();
		int whiteAttributeIndex = 0;

		if (this is AWeapon<TModuleType>)
		{
			float damageX = this.whiteAttributes[0].Value - equippedStuff.whiteAttributes[0].Value;
			float damageY = this.whiteAttributes[1].Value - equippedStuff.whiteAttributes[1].Value;

			objectAttri.Add(new ObjectAttributeCompared("Damage Minimal", damageX, this.whiteAttributes[0].Value));
			objectAttri.Add(new ObjectAttributeCompared("Damage Maximal", damageY, this.whiteAttributes[1].Value));
			whiteAttributeIndex = 2;
		}
		else
		{
			var cast = this as ACast<TModuleType>;
			if (cast != null)
			{
				var equippedCast = this as ACast<TModuleType>;
				float manaCost = cast.ManaCost - equippedCast.ManaCost;

				objectAttri.Add(new ObjectAttributeCompared("Mana Cost", manaCost, cast.ManaCost));
			}
		}

		for (; whiteAttributeIndex < this.whiteAttributes.Count; whiteAttributeIndex++)
		{
			float difference = this.GetAttributeValue(this.whiteAttributes[whiteAttributeIndex]) - equippedStuff.GetAttributeValue(equippedStuff.whiteAttributes[whiteAttributeIndex]);

			objectAttri.Add(new ObjectAttributeCompared(this.whiteAttributes[whiteAttributeIndex].WhichAttribute.ToString(), difference, this.GetAttributeValue(this.whiteAttributes[whiteAttributeIndex])));
		}

		objectAttri.Add(new ObjectAttributeCompared("Level Requiered", player.Level - this.levelRequiered, this.levelRequiered));

		return objectAttri;
	}

	public List<ObjectAttributeCompared> GetTheBlueAttributesCompared(List<ObjectAttributeCompared> equippedStuff)
	{
		List<ObjectAttributeCompared> objectAttri = new List<ObjectAttributeCompared>();

		for (int stuffIndex = 0; stuffIndex < this.blueAttributes.Count; stuffIndex++)
		{
			for (int equippedStuffIndex = 0; equippedStuffIndex < equippedStuff.Count; equippedStuffIndex++)
			{
				if (this.blueAttributes[stuffIndex].WhichAttribute.ToString() == equippedStuff[equippedStuffIndex].name)
				{
					float difference = this.GetAttributeValue(this.blueAttributes[stuffIndex]) - equippedStuff[equippedStuffIndex].value;

					objectAttri.Add(new ObjectAttributeCompared(this.blueAttributes[stuffIndex].WhichAttribute.ToString(), difference, this.GetAttributeValue(this.blueAttributes[stuffIndex])));
				}
			}
		}

		return objectAttri;
	}


}
