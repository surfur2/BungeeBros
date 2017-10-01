using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public static InputManager instance;

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
        return Input.GetButtonDown(button + "_" + "P" + playerNumber);
    }

    public bool GetButtonUpForPlayer(int playerNumber, string button)
    {
        return Input.GetButtonDown(button + "_" + "P" + playerNumber);
    }
}
