using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shooter : MonoBehaviour
{
    [Header("Projectile")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float projectileLifetime = 5f;
    [SerializeField] private float firingRate = 0.2f;

    [Header("AI")]
    [SerializeField] private bool useAI;
    [SerializeField] private float firingRateVariance = 0f;
    [SerializeField] private float minimumFiringRate = 0.1f;

    private Coroutine firingCoroutine;
    private AudioPlayer audioPlayer;
    
    [HideInInspector] public bool isFiring = false;

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    private void Start()
    {
        isFiring = useAI;
    }

    private void Update()
    {
        Fire();
    }

    private void Fire()
    {
        if (firingCoroutine == null && isFiring)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if(firingCoroutine != null && !isFiring)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    private IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject projectileInstance = Instantiate(
                projectilePrefab, 
                transform.position, 
                Quaternion.identity
            );

            Rigidbody2D projectileRigidbody = projectileInstance.GetComponent<Rigidbody2D>();

            if (projectileRigidbody)
            {
                projectileRigidbody.velocity = transform.up * projectileSpeed;
            }
            
            Destroy(projectileInstance, projectileLifetime);
            
            audioPlayer.PlayShootingClip();
            yield return new WaitForSeconds(GetRandomFiringRate());
        }
    }

    private float GetRandomFiringRate()
    {
        float projectileFiringRate = Random.Range(
            firingRate - firingRateVariance,
            firingRate + firingRateVariance
        );

        return Mathf.Clamp(projectileFiringRate, minimumFiringRate, float.MaxValue);
    }
}
