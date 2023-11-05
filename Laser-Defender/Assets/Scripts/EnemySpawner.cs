using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<WaveConfigSo> waveConfigs;
    [SerializeField] private float timeBetweenWaves = 0f;
    
    private WaveConfigSo currentWave;
    private bool isLooping = true;
    
    private void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }

    public WaveConfigSo GetCurrentWave()
    {
        return currentWave;
    }

    private IEnumerator SpawnEnemyWaves()
    {
        do
        {
            foreach (WaveConfigSo wave in waveConfigs)
            {
                currentWave = wave;
                for (int i = 0; i < currentWave.GetEnemyCount(); i++)
                {
                    Instantiate(
                        currentWave.GetEnemyPrefab(i),
                        currentWave.GetStartingWaypoint().position,
                        Quaternion.Euler(0, 0, 180),
                        transform
                    );
                    yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
                }

                yield return new WaitForSeconds(timeBetweenWaves);
            }
        } while (isLooping);
    }
}
