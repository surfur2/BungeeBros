using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWaves : MonoBehaviour {

    public List<Rigidbody2D> waves = new List<Rigidbody2D>();
    List<float> seedValue = new List<float>();

	// Use this for initialization
	void Start () {
		for (int i = 0; i < waves.Count; i++)
        {
            seedValue.Add(Random.Range(0.0f, 1.0f));
        }
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < waves.Count; i++)
        {
            waves[i].velocity = new Vector2(Mathf.Cos(Time.time + seedValue[i]), 0.0f);
        }
	}
}
