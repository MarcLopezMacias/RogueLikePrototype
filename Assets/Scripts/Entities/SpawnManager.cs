using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> sceneSpawners;

    [SerializeField]
    public GameObject[] spawnersToSpawn;

    [SerializeField]
    public int numberOfSpawnersToSpawn;
    public int spawnersSpawned;

    private Vector2 RangeX, RangeY;

    private int enemiesToSpawn, enemiesSpawned;
    private bool doneSpawningEnemies;

    private bool doneSpawningSpawners;

    void Start()
    {
        if (numberOfSpawnersToSpawn > 0 && spawnersSpawned < numberOfSpawnersToSpawn) Spawn();
    }

    void OnEnable()
    {
        Start();
    }

    void Update()
    {
        if (spawnersSpawned < numberOfSpawnersToSpawn)
        {
            Spawn();
        }
        enemiesSpawned = CalculateOt();
    }

    public void Remove(GameObject toRemove)
    {
        sceneSpawners.Remove(toRemove);
    }

    public void DisableAll()
    {
        foreach (GameObject spawner in sceneSpawners)
        {
            spawner.GetComponent<Spawner>().enabled = false;
        }
    }

    public void EnableAll()
    {
        foreach (GameObject spawner in sceneSpawners)
        {
            spawner.GetComponent<Spawner>().enabled = true;
        }
    }

    public void ResetSpawners()
    {
        foreach (GameObject spawner in sceneSpawners)
        {
            spawner.GetComponent<Spawner>().Reset();
        }
    }

    private int CalculateIt()
    {
        int number = 0;
        foreach (GameObject spawner in sceneSpawners)
        {
            number += spawner.GetComponent<Spawner>().NumberOfEnemiesToSpawn;
        }
        return number;
    }

    private int CalculateOt()
    {
        int number = 0;
        foreach (GameObject spawner in sceneSpawners)
        {
            number += spawner.GetComponent<Spawner>().enemiesSpawned;
        }
        return number;
    }

    private void Spawn()
    {
        float PosX = Random.Range(RangeX.x, RangeX.y);
        float PosY = Random.Range(RangeY.x, RangeY.y);
        Instantiate(GetRandomSpawner(), new Vector3(transform.position.x + PosX, transform.position.y + PosY, 0), Quaternion.identity);
        spawnersSpawned++;
    }

    private GameObject GetRandomSpawner()
    {
        return spawnersToSpawn[Random.Range(0, spawnersToSpawn.Length)];
    }

    public bool IsDoneSpawning()
    {
        return doneSpawningEnemies;
    }

    public int GetEnemiesSpawned()
    {
        return enemiesSpawned;
    }

    public int GetEnemiesToSpawn()
    {
        return enemiesToSpawn;
    }
}
