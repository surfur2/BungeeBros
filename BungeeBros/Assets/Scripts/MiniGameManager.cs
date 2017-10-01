using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameUtilities;

public class MiniGameManager : MonoBehaviour
{

    public GameObject PlayerPrefab;
    public List<int> playerToTeam = new List<int>();
    public List<Sprite> playerRestingArt = new List<Sprite>();
    public List<Sprite> playerDivingArt = new List<Sprite>();
    public List<Sprite> harnessTints = new List<Sprite>();
    public float RoundTimer = 10;
    public float totalCordLength = 150;

    private static MiniGameManager _instance = null;
    private List<PlayerController> players = new List<PlayerController>();
    private TeamManager teamMan;
    private BungeeLevelGenerator levelGen;
    private BungeeUIManager uiManager;
    private float minCordLength = 50;
    private float maxCordLength = 90;
    private float startTime;
    private bool jumped = false;
    private float countdownTimer;

    public static MiniGameManager Instance { get { return _instance; } }
    public List<PlayerController> Players { get { return players; } }
    public float MinCordLength { get { return minCordLength; } }
    public float MaxCordLength { get { return maxCordLength; } }
    public float CountdownTimer { get { return countdownTimer; } }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        teamMan = GetComponent<TeamManager>();
        levelGen = GetComponent<BungeeLevelGenerator>();
        uiManager = GetComponent<BungeeUIManager>();

        maxCordLength = Random.Range(minCordLength * 2, totalCordLength);
        levelGen.InitLevel(maxCordLength);

        MakePlayers(levelGen.PlayerStartLocations);
        teamMan.MakeTeams(players);

        uiManager.Init(levelGen.PlayerStartLocations);

        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        countdownTimer = Time.time - startTime;
        if (countdownTimer >= RoundTimer && !jumped)
        {
            // Calculate winner
            int winner = GetWinner_Balance1();
            int maxCordPlayer = GetMaxCordPlayer();


            // Make players jump
            foreach (PlayerController player in players)
            {
                player.JumpPlayer();
            }

            jumped = true;
        }
    }

    void MakePlayers(Vector3[] spawnPoints)
    {
        int playerNumber = 1;
        GameObject spawnHeight = GameObject.Find("SpawnHeight");

        for (int i = 0; i < playerToTeam.Count; i++)
        {
            GameObject playerContainer = Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity);
            PlayerController player = playerContainer.GetComponentInChildren<PlayerController>(true);

            player.InitPlayer(playerNumber, playerToTeam[i], playerRestingArt[i], playerDivingArt[i]);

            Vector3 spawn = spawnPoints[i];

            if (spawnHeight)
            {
                spawn.y = spawnHeight.transform.position.y;
            }
            else
            {
                spawn.y += playerRestingArt[i].bounds.extents.y * Mathf.PI;
            }

            playerContainer.transform.position = spawn;

            players.Add(player);
            playerNumber++;
        }
    }

    /// <summary>
    /// Calculates winner considering the worst score from each team for Balance option 1
    /// </summary>
    /// <returns>The teamNumber that won</returns>
    public int GetWinner_Balance1()
    {
        float best = 0;
        int winner = -1;

        // Check the best score out of all teams
        foreach (Team team in teamMan.Teams)
        {
            float currentWorst = maxCordLength;
            int currentWorstPlayer = 0;

            // Get the worst score from all players in the team
            foreach (PlayerController player in team.players)
            {
                if (player.FillBarValue >= maxCordLength)
                {
                    currentWorst = -1;
                    currentWorstPlayer = player.GetPlayerNumber();
                    break;
                }

                if (player.FillBarValue < currentWorst)
                {
                    currentWorst = player.FillBarValue;
                    currentWorstPlayer = player.GetPlayerNumber();
                }

                // Set the cord length for each player separately
                player.SetCordLength(player.FillBarValue);
            }

            if (currentWorst > best)
            {
                best = currentWorst;
                winner = currentWorstPlayer;
            }
        }

        return winner;
    }

    /// <summary>
    /// Calculates winner considering the addition of all cord lengths from each team for Balance option 2
    /// </summary>
    /// <returns>One player at random from the winning team</returns>
    public int GetWinner_Balance2()
    {
        float best = 0;
        int winner = -1;

        // Check the best score out of all teams
        foreach (Team team in teamMan.Teams)
        {
            float currentSum = 0;

            // Get the sum of scores of all players in the team
            foreach (PlayerController player in team.players)
            {
                currentSum += player.FillBarValue;
                if (currentSum >= maxCordLength)
                {
                    currentSum = -1;
                    break;
                }
            }

            if (currentSum > best)
            {
                best = currentSum;
                winner = team.players[Random.Range(0, team.players.Count - 1)].GetPlayerNumber();
            }

            // Set the cord length for all players in the same team
            float len = 0;
            if (currentSum == -1)
                len = maxCordLength;
            else
                len = currentSum;

            foreach (PlayerController player in team.players)
            {
                player.SetCordLength(len);
            }
        }

        return winner;
    }

    /// <summary>
    /// Calculates winner considering the average cord length from each team for Balance option 2
    /// </summary>
    /// <returns>One player at random form the winning team</returns>
    public int GetWinner_Balance3()
    {
        float best = 0;
        int winner = -1;

        // Check the best score out of all teams
        foreach (Team team in teamMan.Teams)
        {
            float currentSum = 0;

            // Get the worst score from all players in the team
            foreach (PlayerController player in team.players)
            {
                currentSum += player.FillBarValue;
                if (currentSum >= maxCordLength)
                {
                    currentSum = -1;
                    break;
                }
            }

            currentSum /= team.players.Count;

            if (currentSum > best)
            {
                best = currentSum;
                winner = team.players[Random.Range(0, team.players.Count - 1)].GetPlayerNumber();
            }

            // Set the cord length for all players in the same team
            float len = 0;
            if (currentSum == -1)
                len = maxCordLength;
            else
                len = currentSum;

            foreach (PlayerController player in team.players)
            {
                player.SetCordLength(len);
            }
        }

        return winner;
    }

    // Get player with the maximum cord length
    int GetMaxCordPlayer()
    {
        float currentMaxCord = 0;
        int currentMaxPlayer = 1;

        int playerIterator = 1;
        foreach(PlayerController player in players)
        {
            if (player.GetCordLength() >= maxCordLength)
                return currentMaxPlayer;

            if (player.GetCordLength() > currentMaxCord)
            {
                currentMaxCord = player.GetCordLength();
                currentMaxPlayer = playerIterator;
            }

            playerIterator++;
        }

        return currentMaxPlayer;
    }
}
