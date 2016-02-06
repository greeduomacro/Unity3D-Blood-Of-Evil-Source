using UnityEngine;
using System.Collections;

public sealed class LoadingData 
{
	private float playingTime;
	private string sceneName;
	private int level;
	private string repositoryPath;
	private string partyName;

	public float PlayingTime {	get { return playingTime; }
								private set { playingTime = value; } }
	public string SceneName {	get { return sceneName; } 
								private set { sceneName = value; } }
	public int Level {	get { return level; } 
						private set { level = value; } }
	public string RepositoryPath {	get { return repositoryPath; }
									private set { repositoryPath = value; } }
	public string PartyName	{	get { return partyName; }
								private set { partyName = value; } }

	public void Initialize(string repositoryPath, string partyName)
	{
		this.repositoryPath = repositoryPath;
		this.partyName = partyName;
	}

	public void Update(int level)
	{
		this.playingTime +=  Time.deltaTime;
		this.sceneName = Application.loadedLevelName;
		this.level = level;
	}
}

