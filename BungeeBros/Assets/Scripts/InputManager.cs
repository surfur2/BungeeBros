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

    // This will return true the single frame that the button is initiallt pressed.
    public bool GetButtonDownForPlayer (int playerNumber, string button)
    {
        if (isDevMode)
            return Input.GetKey("a");

        return Input.GetButtonDown(button + "_P"+ playerNumber);
    }

    // this will return true for the single frame the button was released
    public bool GetButtonUpForPlayer(int playerNumber, string button)
    {
        return Input.GetButtonUp(button + "_P" + playerNumber);
    }

    // This will return true for all frames the buttonis held.
    public bool GetButtonForPlayer(int playerNumber, string button)
    {
        if (isDevMode)
            return Input.GetKey("a");

        return Input.GetButton(button + "_P" + playerNumber);
    }

    
}
