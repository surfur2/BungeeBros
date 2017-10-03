using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameUtilities;

public class BungeeBarUI : MonoBehaviour
{
    // Constants
    private const float FULL_BAR_SCALE_FACTOR = 2.857f;

    // Instance Variables
    private GameObject barScaler;
    private GameObject pointer;
    private SpriteRenderer fillBarRenderer;
    private TextMesh lowText;
    private TextMesh highText;

    private float pointerHalfHeight;


    // Use this for initialization
    void Start()
    {
        barScaler = transform.Find("BarScaler").gameObject;
        pointer = transform.Find("Pointer").gameObject;
        fillBarRenderer = barScaler.transform.Find("FillBar").gameObject.GetComponent<SpriteRenderer>();
        pointerHalfHeight = pointer.GetComponent<SpriteRenderer>().bounds.extents.y;

        lowText = transform.Find("LowText").gameObject.GetComponent<TextMesh>();
        highText = transform.Find("HighText").gameObject.GetComponent<TextMesh>();

        lowText.gameObject.GetComponent<MeshRenderer>().sortingLayerName = "UI Layer";
        highText.gameObject.GetComponent<MeshRenderer>().sortingLayerName = "UI Layer";
    }

    // Update is called once per frame
    void Update()
    {

    }


    #region Methods
    /// <summary>
    /// Sets the current bar representation amount. ie. How much rope out of the total is the player using.
    /// </summary>
    /// <param name="playerSelection">The length of rope (in meters) the player is currently selecting. Will be clamped between the empty and full bungee distance values.</param>
    /// <param name="min">The minimum value of the player selection.</param>
    /// <param name="max">The maximum value of the player selection.</param>
    public void SetBarRepresentation(float playerSelection, float min, float max)
    {
        //Set displayed min and max
        float scaledMin = Mathf.Floor((min * Globals.UNITY_UNIT_TO_METERS)/10) * 10;
        float scaledMax = Mathf.Ceil((max * Globals.UNITY_UNIT_TO_METERS) /100) * 100;

        lowText.text = scaledMin.ToString();
        highText.text = scaledMax.ToString();

        // Clamp the selection between the min and max
        playerSelection = Mathf.Clamp(playerSelection, min, max);

        // Calculate the percentage the bar will be filled, and multiply that by the full scale value to get the proper representation
        float scaleFactor = ((playerSelection - min) / (max - min)) * FULL_BAR_SCALE_FACTOR;

        // Represent the selection on the bar
        barScaler.transform.localScale = new Vector3(scaleFactor, barScaler.transform.localScale.y, 1);

        // Calculate the location of the pointer at the end of the bar and move it
        Vector3 pointerPos = barScaler.transform.position;
        pointerPos.x += fillBarRenderer.bounds.extents.x * 2;
        pointerPos.y += pointerHalfHeight;
        pointer.transform.position = pointerPos;
    }
    #endregion
}
