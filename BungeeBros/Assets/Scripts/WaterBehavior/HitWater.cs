using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitWater : MonoBehaviour {
    
    public int numberOfParticles;

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player != null)
        {
            ParticleSystem myPatricleSystem = player.GetComponentInChildren<ParticleSystem>();
            myPatricleSystem.Emit(numberOfParticles);
            player.PlayerHitWater(gameObject.transform.position);
        }
    }
}
