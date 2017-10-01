using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameUtilities;

public class MiniGameManager : MonoBehaviour {

    public static MiniGameManager Instance { get { return _instance; } }
    public GameObject PlayerPrefab;
    public List<int> playerToTeam = new List<int>();
    public List<Sprite> playerArt = new List<Sprite>();
    public List<PlayerController> Players { get { return players; } }

    private static MiniGameManager _instance = null;
    List<PlayerController> players = new List<PlayerController>();
    TeamManager teamMan;
    BungeeLevelGenerator levelGen;

    private void Awake()
    {
        // Singleton initialization
        if (_instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    // Use this for initialization
    void Start () {
        teamMan = GetComponent<TeamManager>();
        levelGen = GetComponent<BungeeLevelGenerator>();

        levelGen.InitLevel();

        MakePlayers(levelGen.PlayerStartLocations);
        teamMan.MakeTeams(players);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void MakePlayers(Vector3[] spawnPoints)
    {
        int playerNumber = 1;
        for (int i = 0; i < playerToTeam.Count; i++)
        {
            GameObject playerGO = Instantiate(PlayerPrefab, spawnPoints[i], Quaternion.identity);
            PlayerController player = playerGO.GetComponent<PlayerController>();

            player.InitPlayer(playerNumber, playerToTeam[i], 0);
            playerGO.GetComponent<SpriteRenderer>().sprite = playerArt[i];

            players.Add(player);
            playerNumber++;
        }
    }
}
