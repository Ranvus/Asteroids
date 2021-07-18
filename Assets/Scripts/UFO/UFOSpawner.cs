using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOSpawner : MonoBehaviour
{
    [SerializeField] private UFO ufoPrefab;

    [SerializeField] private float spawnRate = 3f;
    [SerializeField] private float spawnTimer;
    public float spawnX;
    private float spawnY;

    private GameObject[] ufo;

    public static UFOSpawner Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        spawnTimer = spawnRate;
    }

    private void Update()
    {
        ufo = GameObject.FindGameObjectsWithTag("UFO");

        if (ufo.Length == 0)
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer < 0)
            {
                UFOSpawn();
                spawnTimer = spawnRate;
            }
        }
    }

    private void UFOSpawn()
    {
        int ran = Random.Range(1, 3);

        if (ran == 1)
        {
            spawnX = 8.3f;
        }
        else
        {
            spawnX = -8.3f;
        }

        spawnY = Random.Range(-3.5f, 3.5f);

        Vector3 spawnPos = new Vector3(spawnX, spawnY, 0);

        Instantiate(ufoPrefab, spawnPos, transform.rotation);
    }
}
