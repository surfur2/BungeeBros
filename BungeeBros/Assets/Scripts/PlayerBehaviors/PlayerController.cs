using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float score;

    private int playerNumber;
    private int playerTeam;
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
            myBungeeController.SetPlayerGuess(score);
            JumpPlayer();
        }
    }

    public void InitPlayer(int _playerNumber, int _playerTeam, Sprite _restingSprite, Sprite _divingSprite)
    {
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

    public float GetPlayerScore()
    {
        return score;
    }

    public bool HasPlayerJumped()
    {
        return playerJumped;
    }

    public void AddValueToScore (float value)
    {
        score += value;

        if (score < MiniGameManager.Instance.MinCordLength)
        {
            score = MiniGameManager.Instance.MinCordLength;
        }

        if (score > MiniGameManager.Instance.MaxCordLength)
        {
            score = MiniGameManager.Instance.MaxCordLength;
        }

        Debug.Log("My current score: "+ score);
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
}
