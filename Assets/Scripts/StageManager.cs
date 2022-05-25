using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private static GameObject MainCamera;

    private int roomsCompleted;

    public int difficulty;

    [SerializeField]
    public GameObject starterWeapon;

    [SerializeField]
    public Transform[] spawnPoints;

    void Start()
    {
        MainCamera = GameObject.FindWithTag("MainCamera");
    }

    void Update()
    {
        
    }

    public void LoadNewStage()
    {
        Transform spawnPoint = GetNewSpawnPoint();
        GameManager.Instance.GetComponent<ScoreManager>().ResetScore();

        SpawnSpawners(spawnPoint);
        EnableSpawners();

        SetCameraPosition(spawnPoint);
        SetPlayerLocation(spawnPoint);

        ResetPlayer();
        SpawnPlayerWeapon(spawnPoint);
    }

    private Transform GetNewSpawnPoint()
    {
        return spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
    }

    private void SpawnSpawners(Transform location)
    {
        SpawnManager manager = GameManager.Instance.GetComponent<SpawnManager>();
        manager.StartSpawning(location);
    }

    private void EnableSpawners()
    {
        SpawnManager manager = GameManager.Instance.GetComponent<SpawnManager>();
        manager.enabled = true;
        manager.EnableSpawners();
    }

    private void SetCameraPosition(Transform location)
    {
        MainCamera.GetComponent<FollowPlayer>().SetNewPosition(location);
    }

    private void SetPlayerLocation(Transform location)
    {
        GameManager.Instance.Player.GetComponent<Player>().SetLocation(location);
    }

    public int GetRoomsCompleted()
    {
        return roomsCompleted;
    }

    public void RoomCompleted()
    {
        GameManager.Instance.GetComponent<SpawnManager>().DestroySpawners();
        GameManager.Instance.GetComponent<EnemyManager>().enemiesSlain = 0;
        GameManager.Instance.GetComponent<ScoreManager>().IncreaseScore(5);
        roomsCompleted++;
    }

    private void SpawnEnemies(Transform location)
    {
        EnableEnemies();
        GameManager.Instance.GetComponent<EnemyManager>().ResetEnemies();
    }

    private void ResetPlayer()
    {
        GameManager.Instance.Player.GetComponent<Player>().Reset();
    }

    private void ResetPlayerPosition()
    {
        GameManager.Instance.Player.GetComponent<Player>().ResetPosition();
    }

    private void ResetCameraPosition()
    {
        MainCamera.GetComponent<FollowPlayer>().ResetPosition();
    }

    private void ResetEnemies()
    {
        EnableEnemies();
        GameManager.Instance.GetComponent<EnemyManager>().ResetEnemies();
    }

    private void EnableEnemies()
    {
        GameManager.Instance.GetComponent<EnemyManager>().enabled = true;
    }

    public void DisableSpawners()
    {
        GameManager.Instance.GetComponent<SpawnManager>().DisableAll();
        GameManager.Instance.GetComponent<SpawnManager>().enabled = false;
    }

    public void DisableEnemies()
    {
        GameManager.Instance.GetComponent<EnemyManager>().DisableAll();
    }

    private void SetDifficulty()
    {
        if (roomsCompleted == 0)
        {
            GameManager.Instance.GetComponent<SpawnManager>().SetSpawnersToSpawn(1);
        }
        else
        {
            GameManager.Instance.GetComponent<SpawnManager>().SetSpawnersToSpawn(roomsCompleted + 1);
        }
    }

    private void SpawnPlayerWeapon(Transform location)
    {
        Debug.Log("Spawning weapon");
        Instantiate(starterWeapon, location.position, Quaternion.identity);
    }
}
