using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();//sets the variable to the player health if it has on otherwise returns null
        if(playerHealth != null)
        {
            playerHealth.Crash();
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
