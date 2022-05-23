using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private static GameObject MainCamera;

    private int roomsCompleted;

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
    }

    private Transform GetNewSpawnPoint()
    {
        return spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
    }

    private void SpawnSpawners(Transform location)
    {
        SpawnManager manager = GameManager.Instance.GetComponent<SpawnManager>();
        manager.StartSpawning();
    }

    private void EnableSpawners()
    {
        SpawnManager manager = GameManager.Instance.GetComponent<SpawnManager>();
        manager.enabled = true;
        StartCoroutine(manager.EnableAll());
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
        roomsCompleted++;
        GameManager.Instance.GetComponent<ScoreManager>().IncreaseScore(5);
    }

    public void ResetSpawners()
    {
        EnableSpawners();
        GameManager.Instance.GetComponent<SpawnManager>().ResetSpawners();
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
}
