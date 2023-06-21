using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapCounter : MonoBehaviour
{
    public int hitsNeeded = 1;   // Number of hits needed to trigger lap over
    private int hitCount = 0;    // Counter for number of hits
    private bool lapOver = false;   // Flag to track if lap over has been triggered

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cube"))   // Replace "BoxCollider" with the tag of your collider
        {
            hitCount++;   // Increment hit counter
            if (hitCount >= hitsNeeded && !lapOver)   // Check if hit count has reached required amount and lap over hasn't been triggered yet
            {
                Time.timeScale = 0f;   // Pause the scene
                Debug.Log("Lap Over");   // Display message
                lapOver = true;   // Set lap over flag to true to prevent triggering again
            }
        }
    }
}
