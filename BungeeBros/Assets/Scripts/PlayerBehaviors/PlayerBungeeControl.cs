using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBungeeControl : MonoBehaviour {

    public float unityUnitToMeter = 20.0f;
    public float lengthOfCord;

    private Vector3 playerStartingPosition;
    private bool reachedRestingLocation = false;
    private const float bungeeThreshhold = 0.2f;
    private const float errorForFall = 0.2f;
    private const float velocityError = 0.87f;
    private const float counterGravityForce = 25.0f;

    private Rigidbody2D myRigidBody;

	// Use this for initialization
	void Start () {
        SetPlayerGuess(lengthOfCord);
        playerStartingPosition = gameObject.transform.position;
        myRigidBody = gameObject.GetComponent<Rigidbody2D>();
        reachedRestingLocation = false;
    }
	
	// Update is called once per frame
	void Update () {
		if (lengthOfCord > 0.0f && !reachedRestingLocation)
        {
            float currentFallDistance = (gameObject.transform.position - playerStartingPosition).magnitude * unityUnitToMeter;

            Debug.Log("Current bungee length: " + myRigidBody.velocity.magnitude);

            if (Mathf.Abs(currentFallDistance - lengthOfCord) < errorForFall && myRigidBody.velocity.magnitude < velocityError)
            {
                myRigidBody.velocity = new Vector2(0.0f, 0.0f);
                myRigidBody.gravityScale = 0.0f;
                reachedRestingLocation = true;
            }
            else if (currentFallDistance >= lengthOfCord)
            {
                Vector2 newVelocity = new Vector2(myRigidBody.velocity.x, myRigidBody.velocity.y + (counterGravityForce * Time.deltaTime));
                myRigidBody.velocity = newVelocity;
            }
        }
	}

    public void SetPlayerGuess (float _lengthOfCord)
    {
        lengthOfCord = _lengthOfCord;
    }
}
