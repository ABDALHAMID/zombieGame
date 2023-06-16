using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject[] zombiePrefabs;
    public float minimumDistanceToPlayer = 10f;

    private Transform playerTransform;
    private int waveNumber = 1;
    private int zombiesPerWave = 5;
    private float radius = 5f;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
        StartCoroutine(SpawnZombiesRoutine());
    }

    private IEnumerator SpawnZombiesRoutine()
    {
        while (true)
        {
            for (int i = 0; i < zombiesPerWave; i++)
            {
                GameObject zombiePrefab = GetRandomZombiePrefab();
                Vector3 spawnPoint = GetRandomSpawnPointOnNavMesh();

                if (Vector3.Distance(spawnPoint, playerTransform.position) > minimumDistanceToPlayer)
                {
                    SpawnZombie(zombiePrefab, spawnPoint);
                }
                else
                {
                    i--;
                }

                yield return null;
            }

            zombiesPerWave += 2;
            waveNumber++;

            yield return new WaitForSeconds(120f);
        }
    }

    private GameObject GetRandomZombiePrefab()
    {
        return zombiePrefabs[Random.Range(0, zombiePrefabs.Length)];
    }

    private Vector3 GetRandomSpawnPointOnNavMesh()
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += playerTransform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, radius, NavMesh.AllAreas);
        return navHit.position;
    }

    private void SpawnZombie(GameObject zombiePrefab, Vector3 position)
    {
        Instantiate(zombiePrefab, position, Quaternion.Euler(0f, Random.Range(0f, 360f), 0f));
    }
}
