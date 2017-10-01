using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    private int playerNumber;
    private int playerTeam;
    private float fillBarValue;
    [SerializeField]
    private Sprite restingSprite;
    [SerializeField]
    private Sprite divingSprite;

    public Sprite forwardHarness;
    public Sprite backHarness;
    public SpriteRenderer harnessSpriteRenderer;

    private bool playerJumped;

    // My subobjects
    private PlayerBungeeControl myBungeeController;
    private Jump myJumpController;
    private RopeControl myRopeController;

    public float FillBarValue { get { return fillBarValue; } }
    // My components
    private SpriteRenderer playerSpriteRenderer;
    private Rigidbody2D myRigidBody;

    private void Awake()
    {
        playerJumped = false;

        myBungeeController = GetComponent<PlayerBungeeControl>();
        myJumpController = GetComponent<Jump>();
        myRopeController = GetComponentInChildren<RopeControl>();

        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {   
        if(Input.GetKeyDown("space") && !playerJumped)
        {
            myBungeeController.SetPlayerGuess(fillBarValue);
            JumpPlayer();
        }
    }

    public void InitPlayer(int _playerNumber, int _playerTeam, Sprite _restingSprite, Sprite _divingSprite)
    {
        playerSpriteRenderer = GetComponent<SpriteRenderer>();

        playerNumber = _playerNumber;
        playerTeam = _playerTeam;

        restingSprite = _restingSprite;
        divingSprite = _divingSprite;

        ChangeToForwardHarness();
    }

    public int GetPlayerNumber()
    {
        return playerNumber;
    }

    public int GetPlayerTeam()
    {
        return playerTeam;
    }

    public bool HasPlayerJumped()
    {
        return playerJumped;
    }

    public void AddValueToScore (float value)
    {
        fillBarValue += value;

        if (fillBarValue < MiniGameManager.Instance.MinCordLength)
        {
            fillBarValue = MiniGameManager.Instance.MinCordLength;
        }

        if (fillBarValue > MiniGameManager.Instance.MaxCordLength)
        {
            fillBarValue = MiniGameManager.Instance.MaxCordLength;
        }

        //Debug.Log("My current score: "+ fillBarValue);
    }

    public void JumpPlayer ()
    {
        myJumpController.PlayerJump();
        playerJumped = true;

        // Change the sprites for the player when they dive
        ChangeToBackwardHarness();     
    }

    public void SetCordLength(float cordLen)
    {
        myBungeeController.SetPlayerGuess(cordLen);
    }

    public float GetCordLength()
    {
        return myBungeeController.LengthOfCord;
    }

    public void PlayerHitWater(Vector3 wavePosition)
    {
        ChangeToForwardHarness();
        myRopeController.TurnOffRope();
        myBungeeController.SetPlayerGuess(MiniGameManager.Instance.MaxCordLength);
        myRigidBody.simulated = false;
    }

    private void ChangeToForwardHarness()
    {
        playerSpriteRenderer.sprite = restingSprite;
        harnessSpriteRenderer.sprite = forwardHarness;

        //Offset to make the harness line up with the dive sprite
        // TODO: this is shit and needs to not be a magic number
        harnessSpriteRenderer.transform.localPosition = new Vector3(0.0f, 0.0f, harnessSpriteRenderer.transform.position.z);
    }

    private void ChangeToBackwardHarness()
    {
        playerSpriteRenderer.sprite = divingSprite;
        harnessSpriteRenderer.sprite = backHarness;

        //Offset to make the harness line up with the dive sprite
        // TODO: this is shit and needs to not be a magic number
        harnessSpriteRenderer.transform.localPosition = new Vector3(0.22f, -1.57f, harnessSpriteRenderer.transform.position.z);
    }
}
