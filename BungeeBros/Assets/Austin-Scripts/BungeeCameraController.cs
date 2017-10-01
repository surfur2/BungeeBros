using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BungeeCameraController : MonoBehaviour
{
    // Instance Variables
    // public Camera mainCamera;

    public float upTileSpeed;
    public float downTileSpeed;

    public BungeeCameraStates currentCameraState;

    public BungeeLevelGenerator levelGenerator;

    private float timer;
    private int winnerIndex;
    private int furthestIndex;


    // Use this for initialization
    void Start()
    {
        upTileSpeed = upTileSpeed <= 0 ? 1 : upTileSpeed;
        downTileSpeed = downTileSpeed <= 0 ? 1 : downTileSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentCameraState)
        {
            case BungeeCameraStates.PanToTop:
                PanToTop(upTileSpeed);
                break;

            case BungeeCameraStates.PanToBottom:
                PanToBottom(downTileSpeed);
                break;

            case BungeeCameraStates.WaitAtBottom:
                WaitAtBottom();
                break;

            case BungeeCameraStates.WaitAtTop:
                WaitAtTop();
                break;

            case BungeeCameraStates.ChaseFurthest:
                ChaseFurthest();
                break;

            case BungeeCameraStates.ChaseWinner:
                ChaseWinner();
                break;

            default:
                break;
        }
    }


    #region Methods
    /// <summary>
    /// Pans the camera to the top of the screen (the bridge).
    /// </summary>
    /// <param name="cliffTime">The time it takes for the camera to cross one Cliff tile.</param>
    private void PanToTop(float cliffTime)
    {
        timer += Time.deltaTime;
        float panTime = cliffTime * levelGenerator.NumCliffs;

        Vector3 newCamPos = Vec3SmoothLerp(levelGenerator.WaterObject.transform.position, levelGenerator.BridgeObject.transform.position, timer, panTime);
        newCamPos.z = Camera.main.gameObject.transform.position.z;
        
        if(newCamPos.y >= levelGenerator.BridgeObject.transform.position.y)
        {
            newCamPos.y = levelGenerator.BridgeObject.transform.position.y;
            currentCameraState = BungeeCameraStates.WaitAtTop;
            timer = 0;
        }

        Camera.main.gameObject.transform.position = newCamPos;
    }

    /// <summary>
    /// Pans the camera to the bottom of the screen (the water).
    /// </summary>
    /// <param name="cliffTime">The time it takes for the camera to cross one Cliff tile.</param>
    private void PanToBottom(float cliffTime)
    {
        timer += Time.deltaTime;
        float panTime = cliffTime * levelGenerator.NumCliffs;

        Vector3 newCamPos = Vec3SmoothLerp(levelGenerator.BridgeObject.transform.position, levelGenerator.WaterObject.transform.position, timer, panTime);
        newCamPos.z = Camera.main.gameObject.transform.position.z;

        if (newCamPos.y <= levelGenerator.WaterObject.transform.position.y)
        {
            newCamPos.y = levelGenerator.WaterObject.transform.position.y;
            currentCameraState = BungeeCameraStates.WaitAtBottom;
            timer = 0;
        }

        Camera.main.gameObject.transform.position = newCamPos;
    }

    /// <summary>
    /// Makes the camera move to the bottom of the level and sit there.
    /// </summary>
    private void WaitAtBottom()
    {
        if (levelGenerator.WaterObject != null)
        {
            Vector3 newCamPos = levelGenerator.WaterObject.transform.position;
            newCamPos.z = Camera.main.transform.position.z;
            Camera.main.transform.position = newCamPos;
        }
    }

    /// <summary>
    /// Makes the camera move to the top of the level and sit there.
    /// </summary>
    private void WaitAtTop()
    {
        if (levelGenerator.BridgeObject != null)
        {
            Vector3 newCamPos = levelGenerator.BridgeObject.transform.position;
            newCamPos.z = Camera.main.transform.position.z;
            Camera.main.transform.position = newCamPos;
        }
    }

    /// <summary>
    /// Makes the camera chase after the jumper who goes the furthest
    /// </summary>
    private void ChaseFurthest()
    {
        winnerIndex = MiniGameManager.Instance.GetWinner_Balance1();
        furthestIndex = MiniGameManager.Instance.GetMaxCordPlayer();

        Vector3 camPos = transform.position;
        camPos.y = MiniGameManager.Instance.Players[furthestIndex].gameObject.transform.position.y;
        transform.position = camPos;

        if(camPos.y <= levelGenerator.WaterObject.transform.position.y)
        {
            currentCameraState = BungeeCameraStates.ChaseWinner;
            timer = 0;
        }
    }

    private void ChaseWinner()
    {
        timer += Time.deltaTime;

        Vector3 newCamPos = Vec3SmoothLerp(
            levelGenerator.WaterObject.transform.position, 
            MiniGameManager.Instance.Players[winnerIndex].transform.position, 
            timer, 
            5);

        newCamPos.z = Camera.main.gameObject.transform.position.z;
        Camera.main.gameObject.transform.position = newCamPos;
    }

    //Lerp from a point to another point (with SmootherStep)
    private Vector3 Vec3SmoothLerp(Vector3 start, Vector3 end, float time, float totalTime)
    {
        Vector3 lerped = new Vector3(
            Mathf.Lerp(start.x, end.x, Smootherstep(time / totalTime)),
            Mathf.Lerp(start.y, end.y, Smootherstep(time / totalTime)),
            Mathf.Lerp(start.z, end.z, Smootherstep(time / totalTime))
            );
        return lerped;
    }


    //From: https://chicounity3d.wordpress.com/2014/05/23/how-to-lerp-like-a-pro/
    //Smoothly lerp between two points with ease in and ease out
    private float Smootherstep(float t)
    {
        return t * t * t * (t * (6f * t - 15f) + 10f);
    }

    #endregion
}

public enum BungeeCameraStates
{
    WaitAtTop,
    WaitAtBottom,
    PanToTop,
    PanToBottom,
    ChaseFurthest,
    ChaseWinner
};
