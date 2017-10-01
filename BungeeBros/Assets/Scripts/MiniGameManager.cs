using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameUtilities;

public class MiniGameManager : MonoBehaviour {

    public static MiniGameManager Instance { get { return _instance; } }
    public List<int> playerToTeam = new List<int>();
    public List<Player> Players { get { return players; } }

    private static MiniGameManager _instance = null;
    List<Player> players = new List<Player>();
    TeamManager teamMan;

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
        MakePlayers();
        teamMan.MakeTeams(players);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void MakePlayers()
    {
        int playerNumber = 1;
        foreach (int teamNumber in playerToTeam)
        {
            players.Add(new Player(playerNumber, teamNumber));
            playerNumber++;
        }
    }

    /// <summary>
    /// Adds "scoreValue" amount to the player<playerNumber>'s score
    /// </summary>
    /// <param name="playerNumber">The player Number</param>
    /// <param name="scoreValue">Score amount to add</param>
    public void AddToPlayerScore(int playerNumber, float scoreValue)
    {
        players[playerNumber - 1].score += scoreValue;
    }
}
