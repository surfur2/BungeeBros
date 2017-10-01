using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BungeeUIManager : MonoBehaviour
{
    public GameObject bungeeBarPrefab;
    public float barHeightAbovePlayers;
    private GameObject UIContainer;

    private BungeeBarUI[] bungeeBars;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (bungeeBars != null && bungeeBars.Length > 0)
        {
            UpdateBungeeDisplays();
        }
    }

    public void Init(Vector3[] playerStartPositions)
    {
        UIContainer = new GameObject("UI Container");

        for(int i = 0; i < playerStartPositions.Length; i++)
        {
            GameObject uiBar = Instantiate(bungeeBarPrefab);

            Vector3 transformPosition = uiBar.transform.position;
            transformPosition.y += barHeightAbovePlayers;
            transformPosition.z = Camera.main.transform.position.z + 1;
            uiBar.transform.position = transformPosition;

            uiBar.transform.parent = UIContainer.transform;
            uiBar.name = "BungeeMeter" + (i + 1);

            bungeeBars[i] = uiBar.GetComponent<BungeeBarUI>();
        }
    }


    public void UpdateBungeeDisplays()
    {
        for (int i = 0; i < bungeeBars.Length; i++)
        {
            float playerSelection = MiniGameManager.Instance.Players[i].GetPlayerScore();
            bungeeBars[i].SetBarRepresentation(playerSelection, MiniGameManager.Instance.MinCordLength, MiniGameManager.Instance.totalCordLength);
        }
    }
}
