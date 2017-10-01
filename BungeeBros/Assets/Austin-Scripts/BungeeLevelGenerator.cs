using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameUtilities;

public class BungeeLevelGenerator : MonoBehaviour
{
    // Instance Variables
    public Sprite bridgeArt;
    public Sprite cliffArt;
    public Sprite waterArt;

    public int levelHeight;
    public float bridgeYLocation;

    public TeamManager teamMan;

    private GameObject bridgeObject;
    private GameObject waterObject;
    private Vector3[] playerStartLocations;


    public GameObject BridgeObject
    {
        get { return bridgeObject; }
    }
    public GameObject WaterObject
    {
        get { return waterObject; }
    }


    // Use this for initialization
    void Start()
    {
        GenerateNewLevel(levelHeight);
        playerStartLocations = new Vector3[teamMan.players.Count];
        SetupPlayerLanes();
    }

    // Update is called once per frame
    void Update()
    {
    }


    #region Methods
    /// <summary>
    /// Generates a new level
    /// </summary>
    /// <param name="height">The height of the level. Cannot be less than 1.</param>
    private void GenerateNewLevel(int height)
    {
        // Prevent less than one height
        height = height < 1 ? 1 : height;

        // Create container gameobject
        GameObject worldContainer = new GameObject("WorldContainer");

        // Create bridge
        bridgeObject = new GameObject("Bridge");
        bridgeObject.AddComponent<SpriteRenderer>().sprite = bridgeArt;
        bridgeObject.transform.parent = worldContainer.transform;

        float bridgeSpriteHeight = bridgeObject.GetComponent<SpriteRenderer>().bounds.extents.y * 2;

        // Create cliffs
        for (int i = 1; i < height + 1; i++)
        {
            GameObject cliff = new GameObject("Cliff" + i);
            cliff.AddComponent<SpriteRenderer>().sprite = cliffArt;

            Vector3 newCliffPos = cliff.transform.position;
            newCliffPos.y -= bridgeSpriteHeight * i;
            cliff.transform.position = newCliffPos;

            cliff.transform.parent = worldContainer.transform;
        }

        // Create Water
        waterObject = new GameObject("Water");
        waterObject.AddComponent<SpriteRenderer>().sprite = waterArt;

        Vector3 newWaterPos = waterObject.transform.position;
        newWaterPos.y -= bridgeSpriteHeight * (height + 1);
        waterObject.transform.position = newWaterPos;

        waterObject.transform.parent = worldContainer.transform;
    }

    /// <summary>
    /// Determines the locations of each player on the bridge. 
    /// </summary>
    private void SetupPlayerLanes()
    {
        // Calculate the furthest edges of the camera in world space.
        float horizExtent = Camera.main.orthographicSize * Screen.width / Screen.height;
        float farLeft = -horizExtent;

        // Calculate how far the players should be from each other (and how far the edge players should be from the edge of the screen).
        float separationDistance = (horizExtent * 2) / (playerStartLocations.Length + 1);

        // Loop through the empty array of length 
        for (int i = 0; i < playerStartLocations.Length; i++)
        {
            // Create player objects in the world.
            GameObject newPlayer = new GameObject("Player" + (i + 1));
            newPlayer.AddComponent<SpriteRenderer>().sprite = teamMan.players[i].playerCharacterSprite;
            newPlayer.transform.localScale = new Vector3(.2f, .2f);

            // Calculate and save their starting positions.
            playerStartLocations[i] = new Vector3(
                farLeft + (separationDistance * (i + 1)),
                bridgeYLocation + newPlayer.GetComponent<SpriteRenderer>().bounds.extents.y,
                -1);

            // Move them there.
            newPlayer.transform.position = playerStartLocations[i];
        }
    }
    #endregion
}
