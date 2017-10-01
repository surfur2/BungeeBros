using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBungeeControl : MonoBehaviour {

    public float unityUnitToMeter = 20.0f;
    public float lengthOfCord;

    private float halfCordLength;

    private Vector3 playerStartingPosition;
    private const float bungeeThreshhold = 0.2f;
    private const float errorForFall = 0.2f;

    private Rigidbody2D myRigidBody;

	// Use this for initialization
	void Start () {
        SetPlayerGuess(lengthOfCord);
        playerStartingPosition = gameObject.transform.position;
        myRigidBody = gameObject.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
		if (lengthOfCord > 0.0f)
        {
            float currentFallDistance = (gameObject.transform.position - playerStartingPosition).magnitude * unityUnitToMeter;

            /*if (Mathf.Abs(currentFallDistance - halfCordLength) < errorForFall)
            {
                myRigidBody.velocity = new Vector2(0.0f, 0.0f);
            }*/
            if (currentFallDistance >= halfCordLength)
            {
                 Vector2 newVelocity = new Vector2(myRigidBody.velocity.x, myRigidBody.velocity.y + (20.8f * Time.deltaTime));
                 myRigidBody.velocity = newVelocity;
            }
        }
	}

    public void SetPlayerGuess (float _lengthOfCord)
    {
        lengthOfCord = _lengthOfCord;
        halfCordLength = lengthOfCord / 2.0f;
    }
}
