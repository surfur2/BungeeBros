using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameUtilities;

public class MiniGameManager : MonoBehaviour {

    public GameObject PlayerPrefab;
    public List<int> playerToTeam = new List<int>();
    public List<Sprite> playerArt = new List<Sprite>();
    public float RoundTimer = 10;
    public float totalCordLength = 500;

    private static MiniGameManager _instance = null;
    private List<PlayerController> players = new List<PlayerController>();
    private TeamManager teamMan;
    private BungeeLevelGenerator levelGen;
    private float minCordLength = 20;
    private float maxCordLength = 100;
    private float startTime;
    private bool jumped = false;

    public static MiniGameManager Instance { get { return _instance; } }
    public List<PlayerController> Players { get { return players; } }
    public float MinCordLength {  get { return minCordLength; } }
    public float MaxCordLength {  get { return maxCordLength; } }

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
    void Start () {
        teamMan = GetComponent<TeamManager>();
        levelGen = GetComponent<BungeeLevelGenerator>();

        maxCordLength = Random.Range(100, totalCordLength);
        levelGen.InitLevel();

        MakePlayers(levelGen.PlayerStartLocations);
        teamMan.MakeTeams(players);

        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - startTime >= RoundTimer && !jumped)
        {
            foreach(PlayerController player in players)
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

            player.InitPlayer(playerNumber, playerToTeam[i]);
            SpriteRenderer playerSprite = player.GetComponent<SpriteRenderer>();
            playerSprite.sprite = playerArt[i];

            Vector3 spawn = spawnPoints[i];

            if (spawnHeight)
            {
                spawn.y = spawnHeight.transform.position.y;
            }
            else
            {
                spawn.y += playerSprite.bounds.extents.y * Mathf.PI;
            }
            
            playerContainer.transform.position = spawn;

            players.Add(player);
            playerNumber++;
        }
    }
}
