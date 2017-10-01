using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFillBar : MonoBehaviour {

    public float fillSpeed;
    public float reductionSpeed;

    private PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update () {
        if (!playerController.HasPlayerJumped())
        {
            if (InputManager.instance.GetButtonDownForPlayer(playerController.GetPlayerNumber(), "A"))
            {
                playerController.AddValueToScore(Time.deltaTime * fillSpeed);
            }
            else
            {
                playerController.AddValueToScore(-(Time.deltaTime * reductionSpeed));
            }
        }
    }
}
