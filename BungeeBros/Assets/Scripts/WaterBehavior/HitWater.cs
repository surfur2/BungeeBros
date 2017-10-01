using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitWater : MonoBehaviour {

    public ParticleSystem myPatricleSystem;
    public int numberOfParticles;

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player != null)
        {
            myPatricleSystem.transform.position = player.transform.position;
            myPatricleSystem.Emit(numberOfParticles);
            player.PlayerHitWater(gameObject.transform.position);
        }
    }
}
