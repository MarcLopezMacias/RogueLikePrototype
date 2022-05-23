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
    private int spawnersSpawned;

    private Vector2 RangeX, RangeY;

    private int enemiesToSpawn, enemiesSpawned;

    public bool spawnedEverything;

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

    public IEnumerator EnableAll()
    {
        yield return new WaitForSeconds(0.1f);
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

    private int GetNumberOfEnemiesToSpawn()
    {
        int number = 0;
        foreach (GameObject spawner in sceneSpawners)
        {
            number += spawner.GetComponent<Spawner>().NumberOfEnemiesToSpawn;
        }
        return number;
    }

    public int GetNumberOfEnemiesSpawned()
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
        GameObject spawner = GetRandomSpawner();
        float PosX = Random.Range(RangeX.x, RangeX.y);
        float PosY = Random.Range(RangeY.x, RangeY.y);
        Instantiate(spawner, new Vector3(transform.position.x + PosX, transform.position.y + PosY, 0), Quaternion.identity);
        spawnersSpawned++;
    }

    private GameObject GetRandomSpawner()
    {
        return spawnersToSpawn[Random.Range(0, spawnersToSpawn.Length)];
    }

    public int GetEnemiesToSpawn()
    {
        return enemiesToSpawn;
    }

    public void StartSpawning()
    {
        Start();
    }

    public bool IsDoneSpawning()
    {
        int toSpawn = GetNumberOfEnemiesToSpawn(), spawned = GetNumberOfEnemiesSpawned();

        if (toSpawn == 0 || spawned == 0)
            return false;
        if (toSpawn == spawned)
            return true;
        else
            return false;
    }
}
