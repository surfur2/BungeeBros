using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BungeeLevelGenerator : MonoBehaviour
{
    // Instance Variables
    public Sprite bridgeArt;
    public Sprite cliffArt;
    public Sprite waterArt;

    public int levelHeight;

    private GameObject bridgeObject;
    private GameObject waterObject;


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
    #endregion
}
