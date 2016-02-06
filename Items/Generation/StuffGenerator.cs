using UnityEngine;
using System.Collections;

public class StuffGenerator<TModuleType> : AItemGenerator<TModuleType> where TModuleType : APlayer
{
	#region Attributes
	private e_equipmentQuality minQuality;
	private e_equipmentQuality maxQuality;
	private float itemRarity;
	#endregion
	#region Properties
	public e_equipmentQuality MinQuality {	get { return minQuality; }
											private set { minQuality = value; } }
	public e_equipmentQuality MaxQuality {	get { return maxQuality; }
											private set { maxQuality = value; }	}
	public float ItemRarity {	get { return itemRarity; }
								set { if (0 < value) itemRarity = value; } }
	#endregion

	public StuffGenerator()
	{
		this.probabilities = new float[((int)e_stuffInstantiate.SIZE)];
		this.probabilities[((int)e_stuffInstantiate.ARMOR)] = 1;
		this.probabilities[((int)e_stuffInstantiate.WEAPON)] = 1;
	}

	public void InitializeMinMaxQuality(e_equipmentQuality min, e_equipmentQuality max)
	{
		this.minQuality = min;
		this.maxQuality = max;
	}

	public void GenerateAttributes(AInventory<TModuleType> aInventory, AttributeInitializer<TModuleType> attributeInitializer)
	{
		foreach (AWeapon<TModuleType> weapon in aInventory.Weapons)
		{
			if (null == weapon.fixedAttributes || null == attributeInitializer)
				Debug.Log("it shouldn't work");
			else
				attributeInitializer.GenerateStuffAttribute(weapon);
		}

		foreach (AClothe<TModuleType> clothe in aInventory.Clothes)
		{
			if (null == clothe.fixedAttributes || null == attributeInitializer)
				Debug.Log("it shouldn't work");
			else
				attributeInitializer.GenerateStuffAttribute(clothe);
		}
	}

	public void GenerateRandomItems(AItemContainer<TModuleType> itemContainer)
	{
		int number = MathExtension.GenerateRandomIntBetweenFloat(NumberOfItemGenerated);

		for (byte i = 0; i < number; i++)
		{
			if (this.probabilities.Length > 0)
			{
				e_stuffInstantiate whichPartOfStuff = (e_stuffInstantiate)ServiceLocator.Instance.ProbabilityManager.GetProbabilityIndex(this.probabilities);
				AStuff<TModuleType> itemGenerated = GenerateStuff(whichPartOfStuff);

				if (null != itemGenerated)
				{
					itemGenerated.levelRequiered = this.levelRequired.RandomBetweenValues();
					itemGenerated.equipmentQuality = this.GenerateStuffQuality();

					this.GenerateStuffName(itemGenerated);
					itemContainer.AddItem(itemGenerated);
				}
			}
		}
	}

	private void GenerateStuffName(AStuff<TModuleType> stuff)
	{
		if (stuff.equipmentEmplacement == e_equipmentEmplacement.Both_Hand)
			stuff.Name = "Épée à 2 mains";
		else
			stuff.Name = "Épée à 1 main";

		if (stuff is AClothe<TModuleType>)
			stuff.Name = stuff.equipmentEmplacement.ToString().Replace("_", " ");

		if (MathExtension.IsBetween(stuff.levelRequiered, 0, 9))
			stuff.Name += " d'aventurier";
		else if (MathExtension.IsBetween(stuff.levelRequiered, 10, 19))
			stuff.Name += " de carton";
		else if (MathExtension.IsBetween(stuff.levelRequiered, 20, 29))
			stuff.Name += " de papier maché";
		else if (MathExtension.IsBetween(stuff.levelRequiered, 30, 39))
			stuff.Name += " de bronze";
		else
			stuff.Name += " de caca";
	}

	public e_equipmentQuality GenerateStuffQuality()
	{
		int min = (int)minQuality;
		int max = (int)maxQuality;
		byte power = 1;


		while (this.itemRarity > Random.Range(0f, ((int)Mathf.Pow(2, power))))
		{
			++power;
			++min;
			this.itemRarity *= 0.7f;
		}

		power = 1;;

		while (min < max)
		{
			if (Random.Range(1, ((int)Mathf.Pow(2, power)) + 1) == 1)
				++min;
			else
				break;

			++power;
		}

		return (((e_equipmentQuality)min <= e_equipmentQuality.God) ? ((e_equipmentQuality)min) : e_equipmentQuality.God);
	}

	private AStuff<TModuleType> GenerateStuff(e_stuffInstantiate whichPartOfStuff)
	{
		AStuff<TModuleType> itemGenerated = null;

		switch (whichPartOfStuff)
		{
			case e_stuffInstantiate.ARMOR: itemGenerated = this.GenerateClothe(); break;
			case e_stuffInstantiate.WEAPON: itemGenerated = this.GenerateWeapon(); break;
			case e_stuffInstantiate.Axe_One_Handed: itemGenerated = new WeaponOneHandedAxe<TModuleType>(); break;
			case e_stuffInstantiate.Sword_One_Handed: itemGenerated = new WeaponOneHandedSword<TModuleType>(); break;
			case e_stuffInstantiate.Mass_One_Handed: itemGenerated = new WeaponOneHandedMass<TModuleType>(); break;
			case e_stuffInstantiate.Scepter_One_Handed: itemGenerated = new WeaponOneHandedScepter<TModuleType>(); break;
			case e_stuffInstantiate.Axe_Both_Handed: itemGenerated = new WeaponTwoHandedAxe<TModuleType>(); break;
			case e_stuffInstantiate.Sword_Both_Handed: itemGenerated = new WeaponTwoHandedSword<TModuleType>(); break;
			case e_stuffInstantiate.Mass_Both_Handed: itemGenerated = new WeaponTwoHandedMass<TModuleType>(); break;
			case e_stuffInstantiate.Scepter_Both_Handed: itemGenerated = new WeaponTwoHandedScepter<TModuleType>(); break;
			case e_stuffInstantiate.Lance: itemGenerated = new WeaponTwoHandedLance<TModuleType>(); break;
			case e_stuffInstantiate.Spear: itemGenerated = new WeaponTwoHandedSpear<TModuleType>(); break;
			case e_stuffInstantiate.Staff: itemGenerated = new WeaponTwoHandedStaff<TModuleType>(); break;
			case e_stuffInstantiate.Wand: itemGenerated = new WeaponOneHandedWand<TModuleType>(); break;
			case e_stuffInstantiate.Bow: itemGenerated = new WeaponTwoHandedBow<TModuleType>(); break;
			case e_stuffInstantiate.Crossbow: itemGenerated = new WeaponTwoHandedCrossbow<TModuleType>(); break;
			case e_stuffInstantiate.Armor: itemGenerated = new ClotheArmor<TModuleType>(); break;
			case e_stuffInstantiate.Helmet: itemGenerated = new ClotheHelmet<TModuleType>(); break;
			case e_stuffInstantiate.Belt: itemGenerated = new ClotheBelt<TModuleType>(); break;
			case e_stuffInstantiate.Trousers: itemGenerated = new ClotheTrousers<TModuleType>(); break;
			case e_stuffInstantiate.Glove: itemGenerated = new ClotheGlove<TModuleType>(); break;
			case e_stuffInstantiate.Shoes: itemGenerated = new ClotheShoes<TModuleType>(); break;
			case e_stuffInstantiate.Shield: itemGenerated = new ClotheShield<TModuleType>(); break;

			default: break;
		}

		return itemGenerated;
	}

	private AStuff<TModuleType> GenerateClothe()
	{
		e_clotheCategory clotheCategory = (e_clotheCategory)Random.Range(0, ((int)e_clotheCategory.SIZE));

		if (clotheCategory == e_clotheCategory.Armor) return new ClotheArmor<TModuleType>();
		if (clotheCategory == e_clotheCategory.Helmet) return new ClotheHelmet<TModuleType>();
		if (clotheCategory == e_clotheCategory.Belt) return new ClotheBelt<TModuleType>();
		if (clotheCategory == e_clotheCategory.Trousers) return new ClotheTrousers<TModuleType>();
		if (clotheCategory == e_clotheCategory.Glove) return new ClotheGlove<TModuleType>();
		if (clotheCategory == e_clotheCategory.Shield) return new ClotheShield<TModuleType>();
		if (clotheCategory == e_clotheCategory.Shoes) return new ClotheShoes<TModuleType>();

		return null;
	}

	private AStuff<TModuleType> GenerateWeapon()
	{
		//e_weaponCategory weaponCategory = (e_weaponCategory)Random.Range(0, ((int)e_weaponCategory.SIZE));
		e_weaponCategory weaponCategory = e_weaponCategory.Melee;

		if (weaponCategory == e_weaponCategory.Melee)
		{
			//e_weaponType weaponType =  (e_weaponType)Random.Range(0, ((int)e_weaponCategory.SIZE));
			int bothHand = Random.Range(0, 1 + 1);
			int weaponType = Random.Range(1, 1 + 1);

			if (bothHand == 1)
			{
				if (weaponType == 1)
					return new WeaponTwoHandedSword<TModuleType>();
			}
			else
			{
				if (weaponType == 1)
					return new WeaponOneHandedSword<TModuleType>();
			}
		}

		//else if (weaponCategory == e_weaponCategory.Range)	

		return null;
	}
}
