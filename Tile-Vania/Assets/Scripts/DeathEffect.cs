using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffect : MonoBehaviour
{
    private ParticleSystem deathEffect;
    void Start()
    {
        deathEffect = FindObjectOfType<ParticleSystem>();
        StartCoroutine(PlayDeathEffect());
    }

    private IEnumerator PlayDeathEffect()
    {
        deathEffect.Play();
        yield return new WaitForSecondsRealtime(0.2f);
        deathEffect.Stop();
    }
}
