using UnityEngine;
using System.Collections;

public static class SaveInformation
{
	public static string root = "RPG Saves";
	public static string image = "ScreenShot.png";
	public static string file = "Sauvegarde.xml";
	public static string loadingData = "Loading Data.xml";
	public static string level = "Niveau";
	public static string rootPath = DirectoryFunction.CombinePath(DirectoryFunction.GetMyDocumentsPath(), root);
}
