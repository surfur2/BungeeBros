using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public static InputManager instance;

    public bool devMode;

	// Use this for initialization
	void Start () {
		if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
	}

    public float GetHorizontalForPlayer (int playerNumber)
    {
        if (devMode)
            playerNumber = 1;

        return Input.GetAxis("P" + playerNumber +"_Horizontal");
    }

    public bool GetButtonDownForPlayer (int playerNumber, string button)
    {
        if (devMode)
            playerNumber = 1;

        return Input.GetButtonDown("P" + playerNumber + "_" + button);
    }

    public bool GetButtonUpForPlayer(int playerNumber, string button)
    {
        if (devMode)
            playerNumber = 1;

        return Input.GetButtonUp("P" + playerNumber + "_" + button);
    }
}
