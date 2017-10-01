using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {

    public float jumpspeed;

    private Rigidbody2D myRigidbody2D;
    private BoxCollider2D myBoxCollider2D;
    // Use this for initialization
    void Start () {

        myRigidbody2D = GetComponent<Rigidbody2D>();
        myBoxCollider2D = GetComponent<BoxCollider2D>();
	}

    public void PlayerJump()
    {
        Vector2 velocityUpdate = new Vector2(0.0f, 0.0f);

        velocityUpdate.y = jumpspeed;
        myRigidbody2D.velocity += velocityUpdate;
        myBoxCollider2D.isTrigger = true;
    }
}
