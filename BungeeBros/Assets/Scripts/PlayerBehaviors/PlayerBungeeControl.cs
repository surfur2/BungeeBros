using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameUtilities;

public class PlayerBungeeControl : MonoBehaviour {

    private float lengthOfCord;
    private Vector3 playerStartingPosition;
    private bool reachedRestingLocation;
    private bool readyToJump;
    private const float bungeeThreshhold = 0.2f;
    private const float errorForFall = 0.2f;
    private const float velocityError = 0.87f;
    private const float counterGravityForce = 30.0f;

    private Rigidbody2D myRigidBody;

    public float LengthOfCord { get { return lengthOfCord; } }

	// Use this for initialization
	void Start () {
        myRigidBody = gameObject.GetComponent<Rigidbody2D>();
        reachedRestingLocation = false;
        readyToJump = false;
    }
	
	// Update is called once per frame
	void Update () {
		if (readyToJump && !reachedRestingLocation)
        {
            float currentFallDistance = (gameObject.transform.position - playerStartingPosition).magnitude / Globals.UNITY_UNIT_TO_METERS;

            // Stop bouncing and jittering if you are within a certain distance of final position
            if (Mathf.Abs(currentFallDistance - lengthOfCord) < errorForFall && myRigidBody.velocity.magnitude < velocityError)
            {
                myRigidBody.velocity = new Vector2(0.0f, 0.0f);
                myRigidBody.gravityScale = 0.0f;
                reachedRestingLocation = true;
            }
            // Apply a bungee force to spring bakc up.
            else if (currentFallDistance >= lengthOfCord)
            {
                Vector2 newVelocity = new Vector2(myRigidBody.velocity.x, myRigidBody.velocity.y + (counterGravityForce * myRigidBody.gravityScale * Time.deltaTime));
                myRigidBody.velocity = newVelocity;
            }
        }
	}

    public void SetPlayerGuess(float _lengthOfCord)
    {
        lengthOfCord = _lengthOfCord;
        readyToJump = true;
        playerStartingPosition = gameObject.transform.position;
    }

    public bool HasPlayerReachedRestingLocation()
    {
        return reachedRestingLocation;
    }

    public void SetPlayerHasReachedResting()
    {
        reachedRestingLocation = true;
    }
}
