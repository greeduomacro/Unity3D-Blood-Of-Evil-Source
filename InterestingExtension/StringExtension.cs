using UnityEngine;
using System.Collections;
using System.Text;

public static class StringExtension
{
	public static string ToRomanNumeral(this int value)
	{
		//if (value < 0)
		//	throw new ArgumentOutOfRangeException("Please use a positive integer greater than zero.");

		StringBuilder sb = new StringBuilder();
		int remain = value;
		while (remain > 0)
		{
			if (remain >= 1000) { sb.Append("M"); remain -= 1000; }
			else if (remain >= 900) { sb.Append("CM"); remain -= 900; }
			else if (remain >= 500) { sb.Append("D"); remain -= 500; }
			else if (remain >= 400) { sb.Append("CD"); remain -= 400; }
			else if (remain >= 100) { sb.Append("C"); remain -= 100; }
			else if (remain >= 90) { sb.Append("XC"); remain -= 90; }
			else if (remain >= 50) { sb.Append("L"); remain -= 50; }
			else if (remain >= 40) { sb.Append("XL"); remain -= 40; }
			else if (remain >= 10) { sb.Append("X"); remain -= 10; }
			else if (remain >= 9) { sb.Append("IX"); remain -= 9; }
			else if (remain >= 5) { sb.Append("V"); remain -= 5; }
			else if (remain >= 4) { sb.Append("IV"); remain -= 4; }
			else if (remain >= 1) { sb.Append("I"); remain -= 1; }
			//else throw new Exception("Unexpected error."); // <<-- shouldn't be possble to get here, but it ensures that we will never have an infinite loop (in case the computer is on crack that day).
		}

		return sb.ToString();
	}

	public static bool IsLower(char c)	{
		return c >= 'a' && c <= 'z';
	}

	public static bool IsUpper(char c)	{
		return c >= 'A' && c <= 'Z';
	}

	public static char ToLower(char c)	{
		if (IsUpper(c))
			return ((char)(c + 32));
		return c;
	}

	public static char ToUpper(char c)	{
		if (IsLower(c))
			return ((char)(c - 32));
		return c;
	}

	public static string FirstLetterMaj(string name)
	{
		for (short i =0; i < name.Length; i++)
			if (i == 0 || name[i - 1] == ' ' && StringExtension.IsLower(name[i]))
				name = StringExtension.ReplaceAtIndex(i, StringExtension.ToUpper(name[i]), name);
		return name;
	}

	//remplacer un seul character
	public static string ReplaceAtIndex(int i, char newChar, string str)	{
		char[] letters = str.ToCharArray();
		letters[i] = newChar;
		string newString= "";

		for (int x = 0; x < str.Length; x++)
			newString += letters[x];
		return newString;
	}

	//x/y/z >> x	xyz >> null
	public static string FindStringBeforeSeparatorIfSeparatorExist(string whereToSearch, string seperator)
	{
		int haveBeenFound = whereToSearch.IndexOf(seperator);

		return haveBeenFound != -1 ? whereToSearch.Substring(0, haveBeenFound) : "";
	}

	//x/y/z >> x	xyz >> xyz
	public static string FindStringBeforeSeparator(string whereToSearch, string seperator)
	{
		int haveBeenFound = whereToSearch.IndexOf(seperator);

		return haveBeenFound != -1 ? whereToSearch.Substring(0, haveBeenFound) : whereToSearch;
	}

	//x/y/z >> y/z 
	public static string FindStringAfterSeparator(string whereToSearch, string seperator)
	{
		int haveBeenFound = whereToSearch.IndexOf(seperator);

		return haveBeenFound != -1 ? whereToSearch.Substring(haveBeenFound + 1, whereToSearch.Length - haveBeenFound - 1) : "";
	}

	//x/y/z >> z
	public static string FindStringAfterLastSeparator(string whereToSearch, string seperator)
	{
		int haveBeenFound = whereToSearch.LastIndexOf(seperator);

		return haveBeenFound != -1 ? whereToSearch.Substring(haveBeenFound + 1, whereToSearch.Length - haveBeenFound - 1) : "";
	}

	// x/y/z >>> z/y/x
	public static string InverseOrderOfStringInFunctionOfSeparator(string format, string separator)
	{
		string stringReverseInFunctionOfSeparator = "";
		string[] splits = format.Split(separator.ToCharArray());

		for (int i =splits.Length - 1; i > -1; i--)
		{
			stringReverseInFunctionOfSeparator += splits[i];

			if (i - 1 > -1)
				stringReverseInFunctionOfSeparator += separator[0];
		}
		return stringReverseInFunctionOfSeparator;
	}
}

