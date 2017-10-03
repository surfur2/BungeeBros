using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public static InputManager instance = null;

    private bool isDevMode = true;

	// Use this for initialization
	void Awake () {
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
            return DevModeButtonDown(playerNumber);

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
            return DevModeButtonState(playerNumber);

        return Input.GetButton(button + "_P" + playerNumber);
    }


    private bool DevModeButtonDown(int playerNumber)
    {
        switch (playerNumber)
        {
            case 1:
                return Input.GetKeyDown("a");
            case 2:
                return Input.GetKeyDown("s");
            case 3:
                return Input.GetKeyDown("k");
            case 4:
                return Input.GetKeyDown("l");
            default:
                return Input.GetKeyDown("a");
        }
    }

    private bool DevModeButtonState (int playerNumber)
    {
        switch (playerNumber)
        {
            case 1:
                return Input.GetKey("a");
            case 2:
                return Input.GetKey("s");
            case 3:
                return Input.GetKey("k");
            case 4:
                return Input.GetKey("l");
            default:
                return Input.GetKey("a");
        }
    }

}
