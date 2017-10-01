using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BungeeBarUI : MonoBehaviour
{
    public float emptyBarBungeeDistance;
    public float fullBarBungeeDistance;

    private GameObject barScaler;
    private GameObject pointer;
    private SpriteRenderer fillBarRenderer;

    private const float FULL_BAR_SCALE_FACTOR = 2.857f;

    // Use this for initialization
    void Start()
    {
        barScaler = transform.Find("BarScaler").gameObject;
        pointer = transform.Find("Pointer").gameObject;
        fillBarRenderer = barScaler.transform.Find("FillBar").gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        SetBarRepresentation(Time.time);
    }

    public void SetBarRepresentation(float playerSelection)
    {
        playerSelection = Mathf.Clamp(playerSelection, emptyBarBungeeDistance, fullBarBungeeDistance);

        float scaleFactor = (playerSelection / fullBarBungeeDistance) * FULL_BAR_SCALE_FACTOR;

        barScaler.transform.localScale = new Vector3(scaleFactor, barScaler.transform.localScale.y, 1);

        Vector3 pointerPos = barScaler.transform.position;
        pointerPos.x += fillBarRenderer.bounds.extents.x * 2;
        //pointerPos.y += 
        pointer.transform.position = pointerPos;
    }
}
