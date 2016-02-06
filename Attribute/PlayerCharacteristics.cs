using UnityEngine;
using System.Collections;


[System.Serializable]
public class PlayerCharacteristics<TModuleType> : AModule<TModuleType> where TModuleType : APlayer
{
	#region Attributes
	private PlayerCharacteristic[] characteristics;
	#endregion
	#region Data Attributes
	private int characteristicRemain;
	#endregion
	#region Data Properties
	public int CharacteristicRemain
	{
		get { return characteristicRemain; }
		set { if (value >= 0) characteristicRemain = value; }
	}
	public void CharacteristicRemainIncrement() { characteristicRemain += 1; }
	public void CharacteristicRemainDecrement() { characteristicRemain -= 1; } 
	#endregion
	#region Properties
	public PlayerCharacteristic[] Characteristics { get { return characteristics; }
													private set { characteristics = value; } }
	#endregion
	#region Builder
	public PlayerCharacteristics() {} 

	public override void Initialize(TModuleType player)
	{
		base.SetModule(player, "Scripts/Behaviour/Attributes/Characteristics");

		this.characteristics = new PlayerCharacteristic[(int)(e_playerCharacteristic.SIZE)];

		for (short i =0; i < (int)(e_playerCharacteristic.SIZE); i++)
		{
			this.characteristics[i] = new PlayerCharacteristic();
			this.characteristics[i].Characteristic = (e_playerCharacteristic)i;
		}

		this.characteristics[0].Selected = true;

		this.characteristicRemain = 10;
	}
	#endregion
	#region GUI Functions
	//refaire en objet
	public string GetCharacteristicText(e_playerCharacteristic charac)
	{
		if (charac == e_playerCharacteristic.Strength) return charac.ToString() + " add damage and the possibility to wear more weight";
		if (charac == e_playerCharacteristic.Resistance) return charac.ToString() + " add better defence capacities";
		if (charac == e_playerCharacteristic.Vitality) return charac.ToString() + " add life and endurance";
		if (charac == e_playerCharacteristic.Energy) return charac.ToString() + " add mana and better skill effect";

		return "";
	}
	public void AddCharacteristic(int i)
	{
		--this.characteristics[i].PointLevel;
		this.CharacteristicRemainIncrement();
		this.Select(i);
	}


	public void Select(int i)
	{
		foreach (var playerCharac in characteristics)
			playerCharac.Selected = false;

		this.characteristics[i].Selected = true;
	}

	public void SubstractCharacteristic(int i)
	{
		++this.characteristics[i].PointLevel;
		this.CharacteristicRemainDecrement();
		this.Select(i);
	}

	public void ApplyCharacteristic()
	{
		for (short i =0; i < (int)(e_playerCharacteristic.SIZE); i++)
		{
			this.characteristics[i].TotalPoint += this.characteristics[i].PointLevel;
			this.characteristics[i].PointLevel = 0;
		}
	}

	public void CancelCharacteristic()
	{
		for (short i =0; i < (int)(e_playerCharacteristic.SIZE); i++)
		{
			this.CharacteristicRemain += this.characteristics[i].PointLevel;
			this.characteristics[i].PointLevel = 0;
		}
	}
	#endregion
	#region Override Functions
	public override void Update()
	{
		this.StrengthAttribute();
		this.ResistanceAttribute();
		this.EnduranceAttribute();
		this.EnergyAttribute();
	}
	#endregion
	#region Functions
	void EnergyAttribute()
	{
	//    float mp = this.attributes.attributes[((int)e_entityAttribute.Mana)];
	//    float mpPercent = this.attributes.attributes[((int)e_entityAttribute.Mana_Percent)];
	//    float energy = this.attributes.attributes[((int)e_entityAttribute.Energy)] + this.characteristics[(int)(e_playerCharacteristic.Energy)].TotalPoint;

	//    this.attributes.Mana.Max = (int)((mp + 100 + energy * 3) * (1 + mpPercent / 100));
	//    this.skillEffectPercent = 1 + this.attributes[((int)e_entityAttribute.Fast_Cast_Damage_Percent)] * 0.01f + energy * 0.05f;
	}
	void EnduranceAttribute()
	{
		//float vita = this.attributes.attributes[((int)e_entityAttribute.Vitality)] + this.characteristics[(int)(e_playerCharacteristic.Vitality)].TotalPoint;

		//this.SetLife(vita);
		//this.SetEndurance(vita);
	}
	void ResistanceAttribute()
	{
		//float res = this.attributes.attributes[((int)e_entityAttribute.Resistance)] + this.characteristics[(int)(e_playerCharacteristic.Resistance)].TotalPoint;
		//float def = this.attributes.attributes[((int)e_entityAttribute.Defence)];
		//float defPercent = this.attributes.attributes[((int)e_entityAttribute.Defence_Percent)];

		//this.defence = (def + (res * 15f)) * defPercent * 0.01f;
	}
	void StrengthAttribute()
	{
		//float str = this.attributes[((int)e_entityAttribute.Strength)] + this.playerCharacteristics.Characteristics[(int)(e_playerCharacteristic.Strength)].TotalPoint;

		//this.SetDamage(str);
		//this.SetMaximalWeight(str);
	}
	#endregion
}