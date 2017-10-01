using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float score;

    private int playerNumber;
    private int playerTeam;

    private bool playerJumped;

    // My subobjects
    private PlayerBungeeControl myBungeeController;
    private Jump myJumpController;

    private void Start()
    {
        playerJumped = false;

        myBungeeController = GetComponent<PlayerBungeeControl>();
        myJumpController = GetComponent<Jump>();
    }

    private void Update()
    {   
        if(Input.GetKeyDown("space"))
        {
            myBungeeController.SetPlayerGuess(score);
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

    public float GetPlayerScore()
    {
        return score;
    }

    public bool HasPlayerJumped()
    {
        return playerJumped;
    }

    public void AddValueToScore (float value)
    {
        score += value;

        if (score < MiniGameManager.Instance.MinCordLength)
        {
            score = MiniGameManager.Instance.MinCordLength;
        }

        if (score > MiniGameManager.Instance.MaxCordLength)
        {
            score = MiniGameManager.Instance.MaxCordLength;
        }

        Debug.Log("My current score: "+ score);
    }

    public void JumpPlayer ()
    {
        myJumpController.PlayerJump();
        playerJumped = true;
    }
}
