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
    public List<Sprite> frontHarnessTints = new List<Sprite>();
    public List<Sprite> backHarnessTints = new List<Sprite>();
    public List<Material> ropeMaterials = new List<Material>();
    public float RoundTimer = 10;
    public float totalCordLength = 100f;

    private static MiniGameManager _instance = null;
    private List<PlayerController> players = new List<PlayerController>();
    private List<bool> playersReachedRestingLocation = new List<bool>();
    private TeamManager teamMan;
    private BungeeLevelGenerator levelGen;
    private BungeeUIManager uiManager;
    private BungeeCameraController camControl;
    private float minCordLength = 10f;
    private float maxCordLength;
    private float startTime;
    private bool jumped = false;
    private float countdownTimer;
    private int winner = -1;
    private int furthestIndex = -1;

    public static MiniGameManager Instance { get { return _instance; } }
    public List<PlayerController> Players { get { return players; } }
    public float MinCordLength { get { return minCordLength; } }
    public float MaxCordLength { get { return maxCordLength; } }
    public float CountdownTimer { get { return countdownTimer; } }
    public int Winner { get { return winner; } }
    public int FurthestIndex { get { return furthestIndex; } }

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
        camControl = Camera.main.gameObject.GetComponent<BungeeCameraController>();

        maxCordLength = Random.Range(minCordLength + 20.0f, totalCordLength * 0.8f);
        levelGen.InitLevel(maxCordLength);

        MakePlayers(levelGen.PlayerStartLocations);
        teamMan.MakeTeams(players);

        uiManager.Init(levelGen.PlayerStartLocations);

        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (camControl.currentCameraState == BungeeCameraStates.WaitAtTop)
        {
            countdownTimer += Time.deltaTime;
            if (countdownTimer >= RoundTimer && !jumped)
            {
                // winner = GetWinner_Balance1();
                winner = GetWinner_Balance2();
                furthestIndex = GetMaxCordPlayer();

                // Tell camera to begin chasing the furthest jumper.
                camControl.currentCameraState = BungeeCameraStates.ChaseFurthest;

                // Make players jump
                foreach (PlayerController player in players)
                {
                    player.JumpPlayer();
                }

                jumped = true;
            }
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

                    // Set the cord length for each player separately
                    player.SetCordLength(player.FillBarValue);
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
    public int GetMaxCordPlayer()
    {
        float currentMaxCord = 0;
        int currentMaxPlayer = 1;

        foreach(PlayerController player in players)
        {
            if (player.GetCordLength() >= currentMaxCord)
            {
                currentMaxPlayer = player.GetPlayerNumber();
                currentMaxCord = player.GetCordLength();
            }
        }

        return currentMaxPlayer;
    }

    // Get if the players have all finished bouncing and jumping
    public bool PlayersReachedFinalDestination()
    {
        bool haveAllReachedBottom = true;

        foreach (PlayerController player in players)
        {
            haveAllReachedBottom = player.HasPlayerReachedResting();

            if (!haveAllReachedBottom)
                break;
        }

        return haveAllReachedBottom;
    }
}
