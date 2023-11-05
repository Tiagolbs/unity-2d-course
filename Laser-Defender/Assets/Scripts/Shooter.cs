using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float projectileLifetime = 5f;
    [SerializeField] private float projectileFiringRate = 0.2f;

    private Coroutine firingCoroutine;
    
    public bool isFiring = false;
    
    private void Start()
    {
        
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
                projectileRigidbody.velocity = new Vector2(0, projectileSpeed);
            }
            
            Destroy(projectileInstance, projectileLifetime);
            
            yield return new WaitForSeconds(projectileFiringRate);
        }
    }
}
