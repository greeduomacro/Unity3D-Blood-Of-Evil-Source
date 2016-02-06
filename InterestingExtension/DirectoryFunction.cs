using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class DirectoryFunction 
{
	public static List<string> GetSubFiles(string repositoryPath)
	{
		List<string> folderList = new List<string>();
	
		foreach (string folderName in Directory.GetFiles(repositoryPath))
			folderList.Add(folderName);
		
		return folderList;
	}

	public static List<string> GetSubDirectories(string repositoryPath)
	{
		return new List<string>(Directory.GetDirectories(repositoryPath));
	}

	public static string GetMyDocumentsPath()
	{
		return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
	}

	public static string CombinePath(string pathA, string pathB)
	{
		return Path.Combine(pathA, pathB);
	}

	public static void CreateRepository(string repositoryPath)
	{
		DirectoryInfo directory = new DirectoryInfo(repositoryPath);

		try 
		{
			if (directory.Exists == false)
				directory.Create();
		}
		catch (IOException exception)
		{
			Debug.Log("You can't create a repository: " + exception);
		}
	}

	public static string GetLastWrittedFiles(List<string> folders)
	{
		string result = "";

		foreach (string folder in folders)
			if (File.GetLastWriteTime(folder) > File.GetLastWriteTime(result))
				result = folder;

		return result;
	}

	public static string GetMostRecentFolder(List<string> directories)
	{
		string result = "";

		foreach (string directory in directories)
			if (Directory.Exists(result) && Directory.GetLastWriteTime(directory) > Directory.GetLastWriteTime(result))
				result = directory;

		return result;
	}

	public static int GetTheNumberDirectory(string rootDirectory)
	{
		return Directory.GetDirectories(rootDirectory).Length;
	}
}
