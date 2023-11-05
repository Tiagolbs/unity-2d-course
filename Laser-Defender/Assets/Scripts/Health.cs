using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    [SerializeField] private int health = 50;
    [SerializeField] private ParticleSystem hitEffect;
    [SerializeField] private CameraShake cameraShake;
    [SerializeField] private bool applyCameraShake;

    private void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
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
        ShakeCamera();
        damageDealer.Hit();
    }

    private void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
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
