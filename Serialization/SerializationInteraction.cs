using UnityEngine;
using System.Collections;

public sealed class SerializationInteraction : MonoBehaviour
{
    private bool isNewParty = false;

	public void InitializePlayerAndGameManagerBeforeLoadLevel(bool isANewParty, GameObject player, GameObject gameManager, SerializationInformation info)
	{
		player = GameObject.Instantiate(player) as GameObject;

		//GameObject.FindObjectOfType<PlayerAttribute>().LoadingData.Initialize(info.RepositoryPath, info.PartyName);
		//A CORRIGERSerializationManager.Instance.Initialize(info.RepositoryPath, info.PartyName, isANewParty);

		player.name = "Player";
		gameManager.name = "Game Manager";
		DontDestroyOnLoad(player);
		DontDestroyOnLoad(gameManager);
	}

	public void NewParty(GameObject player, GameObject gameManager, SerializationInformation info)
	{
        isNewParty = true;
		this.InitializePlayerAndGameManagerBeforeLoadLevel(true, player, gameManager, info);

		Application.LoadLevel("Level 1");
	}

	public void LoadParty(GameObject player, GameObject gameManager, SerializationInformation info, bool isInMainMenu)
	{
		if (isInMainMenu)
			this.InitializePlayerAndGameManagerBeforeLoadLevel(false, player, gameManager, info);
		else
		{
			DontDestroyOnLoad(player);
			DontDestroyOnLoad(gameManager);
		}
		Application.LoadLevel(info.LoadingDataToDisplay.SceneName);
			
	}

	public void ContinueParty(GameObject player, GameObject gameManager, SerializationInformation info)
	{
		string lastGame = DirectoryFunction.GetMostRecentFolder(info.Directories);

		if (lastGame == "")
			lastGame = info.Directories[0];

		for (short i =0; i < info.Directories.Count; i++)
		{
			if (lastGame == info.Directories[i])
			{
				info.LoadingDataToDisplay = info.LoadingDatas[i];
				info.PartyName = info.LoadingDataToDisplay.PartyName;
			}
		}
		this.InitializePlayerAndGameManagerBeforeLoadLevel(false, player, gameManager, info);

		Application.LoadLevel(info.LoadingDataToDisplay.SceneName);
	}

    public void OnLevelWasLoaded()
    {
        if (isNewParty)
        {
            GameObject player = GameObject.FindGameObjectWithTag("PlayerBody");
            GameObject placeHolder = GameObject.FindGameObjectWithTag("BeginPlaceHolder");

            player.transform.position = placeHolder.transform.position;
            player.transform.rotation = placeHolder.transform.rotation;
        }
    }
}