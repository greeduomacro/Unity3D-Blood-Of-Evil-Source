//public enum e_whichItemCanItake
//{
//    Equipment,
//    Consommable,
//    Quest,
//}

//namespace e_itemFiltre
//{
	
//}

//public enum e_itemType
//{
//	Stuff,
//	//Conssommable,
//	//Quest,
//	//Raw
//	SIZE,
//}

public enum e_itemContainer : byte
{
	Inventory,
	Equipment,
}

public enum e_itemEmplacement : byte
{
	Equipment,
	Inventory,
	Ground
}

public enum e_itemCategory : byte
{
	Stuff,
    Key,
	Consommable,
	SIZE
}

public enum e_itemMesh : byte {
NONE,
	One_Handed_Sword,
	Two_Handed_Sword,
	One_Handed_Axe,
	Two_Handed_Axe,
    Key,
    Cast,
}

public enum e_equipmentCategory : byte
{
	Weapon,
	Clothe,
	Cast
}

public enum e_equipmentQuality : byte
{
	Normal,
	Commun,
	Magic,
	Rare,
	Epic,
	Legendary,
	God
}

public enum e_equipmentEquipped : byte
{
	NOT_EQUIPPED,
	LEFT_HAND,
	RIGHT_HAND,
	BOTH_HAND,
	EQUIPPED,
}

public enum e_equipmentEmplacement : byte
{
	Left_Hand,
	Right_Hand,
	Both_Hand,
	Armor,
	Shoes,
	Glove,
	Belt,
	Helmet,
	Trousers,
	SIZE
}

public enum e_entityAttribute : byte
{
	Life,
	Life_Percent,
	Mana,
	Mana_Percent,
	Damage,
	Damage_Minimal,
	Damage_Maximal,
	Attack_Speed_Percent,
	Attack_Speed_PercentPercent,
	Fast_Cast_Percent,
	Fast_Cast_Damage_Percent,
	Range,
	Move_Speed,
	Move_Speed_Percent,
	Move_Speed_Factor,
	Vitality,
	Endurance,
	Strength,
	Dexterity,
	Block_Chance,
	Resistance,
	Energy,
	Defence,
	Defence_Percent,
	Gold_Quantity_Percent,
	Gold_Amount_Percent,
	Gold_Rarity_Percent,
	Item_Rarity_Percent,
	Item_Quantity_Percent,
	Experience_Percent,
	SIZE
}

public enum e_weaponCategory
{
	Melee,
	Range,
	SIZE,
//	Magic,
}

public enum e_weaponType {
	Axe,
	Sword,
	Lance,
	Spear,
	Wand,
	Staff,
	Mass,
	Scepter,
	Bow,
	Crossbow,
}

public enum e_clotheCategory {
	Armor,
	Helmet,
	Belt,
	Trousers,
	Glove,
	Shoes,
	Shield,
	SIZE,
}

public enum e_castCategory {
	Destruction,
	Heal,
	Skill,
	SIZE
}

public enum e_itemInstantiateType {
	OnGround,
	OnInterface,
}


public enum e_stuffInstantiate {
	WEAPON,
	ARMOR,

	Axe_One_Handed,
	Sword_One_Handed,
	Mass_One_Handed,
	Scepter_One_Handed,
	Axe_Both_Handed,
	Sword_Both_Handed,
	Mass_Both_Handed,
	Scepter_Both_Handed,
	Lance,
	Spear,
	Wand,
	Staff,
	Bow,
	Crossbow,
	Armor,
	Helmet,
	Belt,
	Trousers,
	Glove,
	Shoes,
	Shield,

	SIZE
}

public enum e_goldQuality
{
	Coin,
	Purse,
	Chest,
	
	SIZE,	
}

public enum e_consommableType
{
	Life,
	Mana,
	Life_And_Mana,
}

public enum e_consommableCategory
{
	Potion,
	Food,
}

public enum e_consommableProbability
{
	Life_Potion,
	Mana_Potion,
	Life_And_Mana_Potion,
	Life_Food,
	Mana_Food,
	Life_And_Mana_Food,

	SIZE,
}