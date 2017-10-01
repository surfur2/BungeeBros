using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private int playerNumber;
    private int playerTeam;
    private float score;

    public void InitPlayer(int _playerNumber, int _playerTeam, float _score)
    {
        playerNumber = _playerNumber;
        playerTeam = _playerTeam;
        score = _score;
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
}
