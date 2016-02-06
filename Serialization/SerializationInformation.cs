using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class SerializationInformation
{
	#region Attributs
	private string lastSave;
	private List<string> directories;
	private string repositoryPath;
	private string partyName;
	private List<LoadingData> loadingDatas;
	private LoadingData loadingDataToDisplay;

	private FileStream fs;
	private TimeSpan ts;
	#endregion

	#region Propriété
	public List<LoadingData> LoadingDatas
	{
		get { return loadingDatas; }
		private set { loadingDatas = value; }
	}
	public LoadingData LoadingDataToDisplay
	{
		get { return loadingDataToDisplay; }
		set { loadingDataToDisplay = value; }
	}
	public List<string> Directories
	{
		get { return directories; }
		private set { directories = value; }
	}
	public TimeSpan Ts
	{
		get { return ts; }
		set { ts = value; }
	}
	public FileStream Fs
	{
		get { return fs; }
		set { fs = value; }
	}
	public string LastSave
	{
		get { return lastSave; }
		private set { lastSave = value; }
	}
	public string PartyName
	{
		get { return partyName; }
		set { partyName = value; }
	}
	public string RepositoryPath
	{
		get { return repositoryPath; }
		private set { repositoryPath = value; }
	}
	#endregion

	public void Initialize()
	{
		this.loadingDataToDisplay = null;
		this.loadingDatas = new List<LoadingData>();
		this.repositoryPath = DirectoryFunction.CombinePath(DirectoryFunction.GetMyDocumentsPath(), SaveInformation.root);
		DirectoryFunction.CreateRepository(this.repositoryPath);

		this.InitializeLoadingDatas();
		this.GetMostRecentSave();
		this.partyName = "";
	}

	public void GetMostRecentSave()
	{
		this.lastSave = DirectoryFunction.GetMostRecentFolder(this.directories);

		if (this.lastSave == "" && this.directories.Count > 0)
			this.lastSave = this.directories[0];
	}

	public void InitializeLoadingDatas()
	{
		this.directories = DirectoryFunction.GetSubDirectories(this.repositoryPath);
		this.directories.Sort((a, b) => { return ((Directory.GetLastWriteTime(a) > Directory.GetLastWriteTime(b)) ? 1 : -1); });
		this.loadingDatas.Clear();

		foreach (string directory in directories)
		{
			LoadingData loadingData = new LoadingData();
						
			loadingData = SerializationTemplate.Load<LoadingData>(DirectoryFunction.CombinePath(
				DirectoryFunction.CombinePath(this.repositoryPath, directory), SaveInformation.loadingData));

			this.loadingDatas.Add(loadingData);
		}
	}
}