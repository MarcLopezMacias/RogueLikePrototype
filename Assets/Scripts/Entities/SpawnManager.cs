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

    public void DestroySpawners()
    {
        foreach (GameObject spawner in sceneSpawners)
        {
            spawner.GetComponent<Spawner>().Destroy();
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

    private void Spawn(Transform location)
    {
        GameObject spawner = GetRandomSpawner();
        float PosX = Random.Range(RangeX.x, RangeX.y);
        float PosY = Random.Range(RangeY.x, RangeY.y);
        Instantiate(spawner, new Vector3(location.position.x + PosX, location.position.y + PosY, 0), Quaternion.identity);
        spawnersSpawned++;
        if (spawnersSpawned < numberOfSpawnersToSpawn) Spawn(location);
    }

    private GameObject GetRandomSpawner()
    {
        return spawnersToSpawn[Random.Range(0, spawnersToSpawn.Length)];
    }

    public int GetEnemiesToSpawn()
    {
        return enemiesToSpawn;
    }

    public void StartSpawning(Transform location)
    {
        OnEnable();
        Spawn(location);
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

    void OnEnable()
    {
        sceneSpawners = new List<GameObject>();
        spawnersSpawned = 0;
        enemiesSpawned = 0;
        spawnedEverything = false;
    }
}
