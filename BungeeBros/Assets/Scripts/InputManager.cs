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

        return Input.GetAxis("Horizontal_P" + playerNumber);
    }

    public bool GetButtonDownForPlayer (int playerNumber, string button)
    {
        if (devMode)
            playerNumber = 1;

        return Input.GetButtonDown(button + "_P" + playerNumber);
    }

    public bool GetButtonUpForPlayer(int playerNumber, string button)
    {
        if (devMode)
            playerNumber = 1;

        return Input.GetButtonUp(button + "_P" + playerNumber);
    }
}
