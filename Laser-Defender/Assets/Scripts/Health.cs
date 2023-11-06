using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private bool isPlayer;
    [SerializeField] private int health = 50;
    [SerializeField] private int score = 50;
    [SerializeField] private ParticleSystem hitEffect;
    [SerializeField] private CameraShake cameraShake;
    [SerializeField] private bool applyCameraShake;

    private AudioPlayer audioPlayer;
    private ScoreKeeper scoreKeeper;
    
    private void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        if (!damageDealer)
        {
            return;
        }
        
        TakeDamage(damageDealer.GetDamage());
        PlayHitEffect();
        audioPlayer.PlayDamageClip();
        ShakeCamera();
        damageDealer.Hit();
    }
    
    public int GetHealth()
    {
        return health;
    }

    private void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (!isPlayer)
        {
            scoreKeeper.AddScore(score);
        }
        
        Destroy(gameObject);
    }

    private void PlayHitEffect()
    {
        if (hitEffect == null)
        {
            return;
        }
        
        ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
        ParticleSystem.MainModule mainModule = instance.main;
        Destroy(instance.gameObject, mainModule.duration + mainModule.startLifetime.constantMax);
    }

    private void ShakeCamera()
    {
        if (applyCameraShake && cameraShake != null)
        {
            cameraShake.Play();
        }
    }
}
