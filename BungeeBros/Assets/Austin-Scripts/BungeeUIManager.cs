using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BungeeUIManager : MonoBehaviour
{
    public GameObject bungeeBarPrefab;
    public GameObject timerPrefab;
    public float barHeightAbovePlayers;

    private GameObject UIContainer;

    private BungeeBarUI[] bungeeBars;
    private TextMesh timer;

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
        if (timer != null)
        {
            UpdateTimerDisplay();
        }
    }

    /// <summary>
    /// Initializes the base state of the UI Manager for the Bungee minigame.
    /// </summary>
    /// <param name="playerStartPositions">The precalculated starting positions of each player.</param>
    public void Init(Vector3[] playerStartPositions)
    {
        // Create base objects
        bungeeBars = new BungeeBarUI[playerStartPositions.Length];
        UIContainer = new GameObject("UI Container");

        // Iterate and create player meters
        for (int i = 0; i < playerStartPositions.Length; i++)
        {
            GameObject uiBar = Instantiate(bungeeBarPrefab);

            Vector3 transformPosition = playerStartPositions[i];
            transformPosition.y += barHeightAbovePlayers;
            uiBar.transform.position = transformPosition;

            uiBar.transform.parent = UIContainer.transform;
            uiBar.name = "BungeeMeter" + (i + 1);

            bungeeBars[i] = uiBar.GetComponent<BungeeBarUI>();
        }

        // Initialize the timer
        timer = Instantiate(timerPrefab).GetComponent<TextMesh>();
        Vector3 timerPos = new Vector3(0, 4.5f);
        timer.gameObject.transform.position = timerPos;
        timer.gameObject.transform.parent = UIContainer.transform;
        timer.gameObject.GetComponent<MeshRenderer>().sortingLayerName = "UI Layer";
    }

    /// <summary>
    /// Updates the current display of the bungie bars based on player input
    /// </summary>
    public void UpdateBungeeDisplays()
    {
        for (int i = 0; i < bungeeBars.Length; i++)
        {
            float playerSelection = MiniGameManager.Instance.Players[i].FillBarValue;
            bungeeBars[i].SetBarRepresentation(playerSelection, MiniGameManager.Instance.MinCordLength, MiniGameManager.Instance.totalCordLength);
        }
    }

    /// <summary>
    /// Updates the current display of the timer based on the MiniGameManagers internal timer
    /// </summary>
    public void UpdateTimerDisplay()
    {
        float timerValue = Mathf.Ceil(MiniGameManager.Instance.RoundTimer - MiniGameManager.Instance.CountdownTimer);
        timer.text = timerValue <= 0 ? "JUMP!" : timerValue.ToString();
    }
}
