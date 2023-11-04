using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] private AudioClip coinPickupSfx;
    [SerializeField] private int pointsForCoinPickup = 100;
    
    private GameSession gameSession;
    private bool wasCollected = false;
    
    private void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !wasCollected)
        {
            AudioSource.PlayClipAtPoint(coinPickupSfx, transform.position);
            gameSession.IncreaseScore(pointsForCoinPickup);
            wasCollected = true;
            Destroy(gameObject);
        }
    }
}
