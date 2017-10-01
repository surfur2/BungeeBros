using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    private int playerNumber;
    private int playerTeam;
    private float fillBarValue;

    private bool playerJumped;

    // My subobjects
    private PlayerBungeeControl myBungeeController;
    private Jump myJumpController;


    public float FillBarValue { get { return fillBarValue; } }


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
            myBungeeController.SetPlayerGuess(fillBarValue);
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

    public bool HasPlayerJumped()
    {
        return playerJumped;
    }

    public void AddValueToScore (float value)
    {
        fillBarValue += value;

        if (fillBarValue < MiniGameManager.Instance.MinCordLength)
        {
            fillBarValue = MiniGameManager.Instance.MinCordLength;
        }

        if (fillBarValue > MiniGameManager.Instance.MaxCordLength)
        {
            fillBarValue = MiniGameManager.Instance.MaxCordLength;
        }
    }

    public void JumpPlayer ()
    {
        myJumpController.PlayerJump();
        playerJumped = true;
    }

    public void SetCordLength(float cordLen)
    {
        myBungeeController.SetPlayerGuess(cordLen);
    }

    public float GetCordLength()
    {
        return myBungeeController.LengthOfCord;
    }
}
