﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameUtilities;

public class BungeeLevelGenerator : MonoBehaviour
{
    // Instance Variables
    public GameObject bridgePrefab;
    public GameObject cliffPrefab;
    public GameObject waterPrefab;

    public float bridgeYLocation;
    public float waterYLocation;

    public TeamManager teamMan;

    private GameObject bridgeObject;
    private GameObject waterObject;
    private Vector3[] playerStartLocations;
    private int numCliffs;

    public int NumCliffs
    {
        get { return numCliffs; }
    }


    public GameObject BridgeObject
    {
        get { return bridgeObject; }
    }
    public GameObject WaterObject
    {
        get { return waterObject; }
    }
    public Vector3[] PlayerStartLocations
    {
        get { return playerStartLocations; }
    }


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void InitLevel(float lengthToWater)
    {
        GenerateNewLevel(lengthToWater);
        playerStartLocations = new Vector3[MiniGameManager.Instance.playerToTeam.Count];
        SetupPlayerLanes();
    }


    #region Methods
    /// <summary>
    /// Generates a new level
    /// </summary>
    /// <param name="height">The height of the level. Cannot be less than 1.</param>
    private void GenerateNewLevel(float height)
    {
        // Prevent less than one height
        height = height < 1 ? 1 : height;

        //How many cliff tiles
        numCliffs = (int)(height) + 1;

        // Create container gameobject
        GameObject worldContainer = new GameObject("WorldContainer");

        // Create bridge
        bridgeObject = Instantiate(bridgePrefab);
        bridgeObject.transform.parent = worldContainer.transform;
        bridgeObject.GetComponentInChildren<TextMesh>().text = "Distance:\n" + (int)(height * Globals.UNITY_UNIT_TO_METERS) + " m";

        float bridgeSpriteHeight = bridgeObject.GetComponent<SpriteRenderer>().bounds.extents.y * 2;

        // Create cliffs
        for (int i = 1; i < numCliffs + 1; i++)
        {
           
            GameObject cliff = Instantiate(cliffPrefab);

            // Adjust the position of the tile
            Vector3 newCliffPos = cliff.transform.position;
            newCliffPos.y -= bridgeSpriteHeight * i;
            cliff.transform.position = newCliffPos;

            cliff.transform.parent = worldContainer.transform;
        }

        // Create Water
        waterObject = Instantiate(waterPrefab);

        Vector3 newWaterPos = waterObject.transform.position;
        newWaterPos.y = bridgeYLocation - (height * Globals.UNITY_UNIT_TO_METERS) + waterYLocation;
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
            // Calculate and save their starting positions.
            playerStartLocations[i] = new Vector3(
                farLeft + (separationDistance * (i + 1)),
                bridgeYLocation,
                -1);
        }
    }
    #endregion
}
