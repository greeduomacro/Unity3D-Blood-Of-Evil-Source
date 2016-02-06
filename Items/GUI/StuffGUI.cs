using UnityEngine;
using System.Collections;

public static class StuffGUI<TModuleType> where TModuleType : APlayer
{
	public static string GetEquippedString(AStuff<TModuleType> stuff)
	{
		string equippedString = "";

		switch (stuff.equipped)
		{
			case e_equipmentEquipped.EQUIPPED: equippedString = "< "; break;
			case e_equipmentEquipped.LEFT_HAND: equippedString = "(L) "; break;
			case e_equipmentEquipped.RIGHT_HAND: equippedString = "(R) "; break;
			case e_equipmentEquipped.BOTH_HAND: equippedString = "(B) "; break;
			default: break;
		}

		return equippedString;
	}

	public static string GetItemColor(AStuff<TModuleType> stuff)
	{
		string color = "<color=white>";

		switch (stuff.equipmentQuality)
		{
			case e_equipmentQuality.Commun: color = "<color=#00FF00FF>"; break;
			case e_equipmentQuality.Magic: color = "<color=#1100FFFF>"; break;
			case e_equipmentQuality.Rare: color = "<color=yellow>"; break;
			case e_equipmentQuality.Epic: color = "<color=#AA33FFFF>"; break;
			case e_equipmentQuality.Legendary: color = "<color=#EE7300FF>"; break;
			case e_equipmentQuality.God: color = "<color=red>"; break;
			default: break;
		}

		return color;
	}

	public static string GetTheBlueAttributes(AStuff<TModuleType> stuff)
	{
		string result = "";

		for (int x = 0; x < stuff.blueAttributes.Count; x++)
			result += GetBlueAttribute(x, stuff) + "\n";

		return result;
	}

	public static string GetBlueAttribute(int x, AStuff<TModuleType> stuff)
	{
		return GetAttributeString(x, "<color=#6666FFFF>", "<color=#6666FFFF>", stuff);
	}

	public static string GetAttributeString(int x, string color, string colorOfPlus, AStuff<TModuleType> stuff)
	{
		return color + MultiResolutions.Font(12) + "<b>" +
			stuff.GetDisplayEquipmentAttribute(stuff.blueAttributes[x].WhichAttribute) + "</b>: " +
			(!(stuff.IsFloatEquipmentAttribute(stuff.blueAttributes[x].WhichAttribute)) ? (colorOfPlus + "+</color>") : "") +
			stuff.GetCorrectEquipmentAttributeValue(stuff.blueAttributes[x]) +
			stuff.GetFinalSignEquipmentAttribute(stuff.blueAttributes[x].WhichAttribute) + "</size></color>";
	}

	public static string GetStuffName(AStuff<TModuleType> stuff)
	{
		return MultiResolutions.Font(16) + GetItemColor(stuff) + stuff.Name + "</color></size>";
	}

	public static string GetEquippedStuffName(AStuff<TModuleType> stuff)
	{
		return MultiResolutions.Font(16) + GetItemColor(stuff) + GetEquippedString(stuff) + stuff.Name + "</color></size>";
	}

	public static string GetKeyName(Keys<TModuleType> key)
	{
		return MultiResolutions.Font(16) + "<color=yellow>" + key.Name + "</color></size>";
	}

	public static Rect GetRectOfMyGUIContent(string text, float posX, float posY)
	{
		float height = (text.Split('\n').Length - 1) * 0.02f + 0.01f;

		return new Rect(0.41f, 0.85f - height, 0.15f, height);
	}

	public static void DisplayItemContent(Rect rect, string text)
	{
		for (byte i = 0; i < 2; i++)
			GUI.Box(rect, "");
		GUI.Box(rect, text);
	}

	public static void DisplayItemInteractionBorder(float posX, float posY, AStuff<TModuleType> stuff)
	{
		for (byte i = 0; i < 3; i++)
			GUI.Box(MultiResolutions.Rectangle(posX, posY, 0.06f, 0.025f *
				(((stuff.Filtre == ItemExtension.FiltreClothe ||
				stuff.Filtre == ItemExtension.FiltreWeapon)
				 ? 1 : 0) + ((stuff.equipped == e_equipmentEquipped.NOT_EQUIPPED) ? 3 : 1) +
				 (((stuff.equipped == e_equipmentEquipped.NOT_EQUIPPED) &&
				 (stuff.equipmentEmplacement == e_equipmentEmplacement.Left_Hand ||
				 stuff.equipmentEmplacement == e_equipmentEmplacement.Right_Hand)) ? 1 : 0))), "");
	}

	public static string GetWeaponCategory(AWeapon<TModuleType> weapon)
	{
		return weapon.equipmentQuality.ToString() + " " + weapon.WeaponType.ToString().Replace("_", " ");
	}

	public static string GetClotheCategory(AClothe<TModuleType> clothe)
	{
		return clothe.equipmentQuality.ToString() + " " + clothe.ClotheCategory.ToString().Replace("_", " ");
	}


	public static string GetStuffTitle(AStuff<TModuleType> stuff, Inventory<TModuleType> inventory, byte itemSelectedIndex)
	{
		string result = GetItemColor(stuff) + "<b>" + MultiResolutions.Font(13);

		if (stuff.Filtre == ItemExtension.FiltreWeapon)
			result += StringExtension.FirstLetterMaj(inventory.Weapons[itemSelectedIndex].Name);
		else if (stuff.Filtre == ItemExtension.FiltreClothe)
			result += StringExtension.FirstLetterMaj(inventory.Clothes[itemSelectedIndex].Name);

		result += "\n</size>" + MultiResolutions.Font(12);

		if (stuff.Filtre == ItemExtension.FiltreWeapon)
			result += GetWeaponCategory(inventory.Weapons[itemSelectedIndex]);
		else if (stuff.Filtre == ItemExtension.FiltreClothe)
			result += GetClotheCategory(inventory.Clothes[itemSelectedIndex]);

		result += "</size></b></color>\n" + MultiResolutions.Font(12);

		return result;
	}
}
