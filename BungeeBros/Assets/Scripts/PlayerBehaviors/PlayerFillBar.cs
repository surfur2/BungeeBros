using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFillBar : MonoBehaviour {

    public float fillSpeedPercentHold;
    public float reductionSpeedPercentHold;
    public float fillSpeedPercentSpam;
    public float reductionSpeedPercentSpam;

    private PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update () {
        if (!playerController.HasPlayerJumped())
        {
            // Register when the button is held to increase amount of cord
            /*if (InputManager.instance.GetButtonForPlayer(playerController.GetPlayerNumber(), "A"))
            {
                playerController.AddValueToScore(Time.deltaTime * fillSpeedPercentHold * MiniGameManager.Instance.totalCordLength);
            }
            else
            {
                playerController.AddValueToScore(-(Time.deltaTime * reductionSpeedPercentHold * MiniGameManager.Instance.totalCordLength));
            }*/

            // Register when the button is tapped to increase the amount of cord.
            if (InputManager.instance.GetButtonDownForPlayer(playerController.GetPlayerNumber(), "A"))
            {
                playerController.AddValueToScore(fillSpeedPercentSpam);
            }
            else
            {
                playerController.AddValueToScore(-(Time.deltaTime * reductionSpeedPercentSpam * MiniGameManager.Instance.totalCordLength));
            }
        }
    }
}
