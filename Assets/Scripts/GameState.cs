using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameState : NetworkBehaviour
{
    public enum State
    {
        onLobby,
        onGame,
        onMeeting
    };

    public List<GameObject> playersList = new List<GameObject>();
    public List<GameObject> impostorsList = new List<GameObject>();
    GameSettings gameSettings;
    TasksState tasksState;
    DoorsManager doorsManager;
    public GameObject namesUI;
    public GameObject impostorsUI;
    public GameObject crewUI;
    public GameObject[] hideOnStart;

    [SyncVar(hook = nameof(OnGameState))]
    public State gameState = State.onLobby;

    private void Start()
    {
        gameSettings = this.GetComponent<GameSettings>();
        tasksState = this.GetComponent<TasksState>();
        doorsManager = this.GetComponent<DoorsManager>();
    }

    public void AddPlayersList(GameObject player)
	{
        playersList.Add(player);
	}
    public void RemovePlayersList(GameObject player)
    {
        if (isServer)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<BetweenUsNetworkManager>().skins.Add(player.GetComponent<SkinRenderer>().skin);
        }
        playersList.Remove(player);
    }

    public void OnGameState(State oldState, State newState)
	{
        if (oldState == State.onLobby && newState == State.onGame)
		{
			foreach (GameObject item in hideOnStart)
			{
                item.SetActive(false);
			}
        }
    }
    public void OpenRoleUI(GameObject player)
	{
        // On Client Side
        if (player.GetComponent<ClientController>().playerRole == ClientController.Role.impostor)
        {
            impostorsUI.SetActive(true);
        }
        else
        {
            crewUI.SetActive(true);
            crewUI.GetComponent<CrewUI>().AssignTasks();
        }
        this.GetComponent<TasksState>().taskbarPanel.SetActive(true);
    }
    public void GameStart()
	{
        // On Server
        // Change seed
        var randomizer = new System.Random();
        int seed = randomizer.Next(int.MinValue, int.MaxValue);
        Random.InitState(seed);

        int numPlayers = playersList.Count;
        
		while (gameSettings.impostorsCount >= numPlayers - gameSettings.impostorsCount)
		{
            gameSettings.impostorsCount -= 1;
		}

        List<GameObject> playersCopy = new List<GameObject>(playersList);

        // Assign Impostors
        for (int i = 0; i < gameSettings.impostorsCount; i++)
		{
            int n = Random.Range(0, playersCopy.Count);
            GameObject newImpostor = playersCopy[n];
            playersCopy.RemoveAt(n);

            newImpostor.GetComponent<ClientController>().playerRole = ClientController.Role.impostor;
        }

		// Give Crew to the rest
		foreach (GameObject crew in playersCopy)
		{
            crew.GetComponent<ClientController>().playerRole = ClientController.Role.crew;
		}

        // Calculate Total Tasks
        tasksState.totalTasks = playersCopy.Count * gameSettings.tasksCount;

        gameState = State.onGame;
        doorsManager.OpenCafeteria(true);
    }
}