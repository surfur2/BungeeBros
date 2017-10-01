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


    public float FillBarValue { get { return fillBarValue; } }
    // My components
    private SpriteRenderer playerSpriteRenderer;

    private void Awake()
    {
        playerJumped = false;

        myBungeeController = GetComponent<PlayerBungeeControl>();
        myJumpController = GetComponent<Jump>();

        playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {   
        if(Input.GetKeyDown("space"))
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

        playerSpriteRenderer.sprite = restingSprite;
        harnessSpriteRenderer.sprite = forwardHarness;
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

        Debug.Log("My current score: "+ fillBarValue);
    }

    public void JumpPlayer ()
    {
        myJumpController.PlayerJump();
        playerJumped = true;
        
        // Change the sprites for the player when they dive
        playerSpriteRenderer.sprite = divingSprite;
        harnessSpriteRenderer.sprite = backHarness;

        //Offset to make the harness line up with the dive sprite
        // TODO: this is shit and needs to not be a magic number
        harnessSpriteRenderer.transform.localPosition = new Vector3( 0.22f, -1.57f, harnessSpriteRenderer.transform.position.z);
    }

    public void SetCordLength(float cordLen)
    {
        myBungeeController.SetPlayerGuess(cordLen);
    }

    public float GetCordLength()
    {
        return myBungeeController.LengthOfCord;
    }
}
