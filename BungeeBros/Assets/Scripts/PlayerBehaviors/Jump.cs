using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {

    public float jumpspeed;

    private bool playerCanJump;
    private Rigidbody2D myRigidbody2D;
    private BoxCollider2D myBoxCollider2D;
    // Use this for initialization
    void Start () {
        playerCanJump = true;

        myRigidbody2D = GetComponent<Rigidbody2D>();
        myBoxCollider2D = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (playerCanJump)
        {
            Vector2 velocityUpdate = new Vector2(0.0f, 0.0f);

            if (Input.GetKeyDown("space"))
            {
                velocityUpdate.y = jumpspeed;
                playerCanJump = false;
                myRigidbody2D.velocity += velocityUpdate;
                myBoxCollider2D.isTrigger = true;
            }
        }
    }
}
