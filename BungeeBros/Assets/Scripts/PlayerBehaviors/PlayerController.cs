using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float score;

    private int playerNumber;
    private int playerTeam;

    // My subobjects
    private PlayerBungeeControl myBungeeController;
    private Jump myJumpController;

    private void Start()
    {
        myBungeeController = GetComponent<PlayerBungeeControl>();
        myJumpController = GetComponent<Jump>();
    }

    private void Update()
    {   
        if(Input.GetButtonDown("space"))
        {
            SetPlayerCordLength(score);
            JumpPlayer();
        }
    }

    public void InitPlayer(int _playerNumber, int _playerTeam)
    {
        playerNumber = _playerNumber;
        playerTeam = _playerTeam;
    }

    public int GetPlayerNumber()
    {
        return playerNumber;
    }

    public int GetPlayerTeam()
    {
        return playerTeam;
    }

    public float GetPlayerSocre()
    {
        return score;
    }

    public void SetPlayerCordLength (float lengthOfCord)
    {
        myBungeeController.SetPlayerGuess(lengthOfCord);
    }

    public void JumpPlayer ()
    {
        myJumpController.PlayerJump();
    }
}
