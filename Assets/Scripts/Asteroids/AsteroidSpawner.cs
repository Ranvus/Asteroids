using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] Asteroid asteroidPrefab;

    [SerializeField] float trajectoryVariance = 15f;
    [SerializeField] float spawnRate = 2f;
    [SerializeField] float spawnAmount = 1f;
    [SerializeField] float spawnDist = 15f;

    private GameObject[] asteroid;

    void Start()
    {
        AsteroidSpawn();
    }

    private void Update()
    {
        asteroid = GameObject.FindGameObjectsWithTag("Asteroid");

        if (asteroid.Length == 0)
        {
            spawnRate -= Time.deltaTime;
            print(spawnRate);
            if (spawnRate < 0)
            {
                spawnAmount++;
                AsteroidSpawn();
                spawnRate = 2f;
            }
        }
    }

    private void AsteroidSpawn()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Vector3 spawnDir = Random.insideUnitCircle.normalized * spawnDist;
            Vector3 spawnPoint = transform.position + spawnDir;

            float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            ObjectPooler.Instance.SpawnFromPool("Asteroid", spawnPoint, rotation);
        }
    }
}
