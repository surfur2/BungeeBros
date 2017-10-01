using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public static InputManager instance;

    private bool isDevMode = true;

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

    public bool GetButtonDownForPlayer (int playerNumber, string button)
    {
        if (isDevMode)
            return Input.GetKey("a");

        return Input.GetButtonDown(button + "_P"+ playerNumber);
    }

    public bool GetButtonUpForPlayer(int playerNumber, string button)
    {
        return Input.GetButtonUp(button + "_P" + playerNumber);
    }
}
