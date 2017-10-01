﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeControl : MonoBehaviour {

    public Transform anchorPosition;
    public Transform playerPoisiton;

    private LineRenderer myLineRenderer;
    private Vector3[] lineRendererPoints = new Vector3[2];

    private void Start()
    {
        myLineRenderer = GetComponent<LineRenderer>();

        lineRendererPoints.SetValue(anchorPosition.position, 0);
    }

    private void Update()
    {
        lineRendererPoints.SetValue(playerPoisiton.position, 1);
        myLineRenderer.SetPositions(lineRendererPoints);
    }
}