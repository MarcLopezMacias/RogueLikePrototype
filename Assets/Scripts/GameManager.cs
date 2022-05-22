using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static  GameManager Instance { get { return _instance; } }
    private static GameManager _instance;

    public GameObject Player { get { return _player; } }
    private GameObject _player;

    public static UIController UIController;
    public static SoundController SoundController;

    public static ScoreManager ScoreManager;
    public static EnemyManager EnemyManager;
    public static SpawnManager SpawnManager;

    public Inventory PlayerInventory;
    public Weapon PlayerWeaponComponent;

    private static GameObject MainCamera;

    public bool GameLoop = false, gameOver = false, MenuLoop = true, ShopLoop = false;

    [SerializeField]
    public Transform[] spawnPoints;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            Debug.Log("GameManager DESTROYED");
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        PlayerInventory = _player.GetComponent<Inventory>();

        UIController = gameObject.GetComponent<UIController>();
        SoundController = gameObject.GetComponent < SoundController>();

        ScoreManager = gameObject.GetComponent<ScoreManager>();

        EnemyManager = gameObject.GetComponent<EnemyManager>();
        SpawnManager = gameObject.GetComponent<SpawnManager>();

        MainCamera = GameObject.FindWithTag("MainCamera");

        PlayerInventory = GameObject.Find("Inventory").GetComponent<Inventory>();

        DisableSpawners();
        DisableEnemies();

        DataSaver.Load();

    }

    void Update()
    {
        if (GameLoop)
        {
            Time.timeScale = 1f;

            // REFRESH NULL COMPONENTS
            if (_player == null) _player = GameObject.FindWithTag("Player");
            if (PlayerInventory == null) PlayerInventory = GameObject.Find("Inventory").GetComponent<Inventory>();
            if (_player != null && PlayerWeaponComponent == null) PlayerWeaponComponent = GameManager.Instance.Player.GetComponentInChildren<Weapon>();

            if (LegitGameOver())
            {
                Debug.Log("Player finished round");
                ScoreManager.IncreaseScore(5);
                GameOver();
            }
        } 
        else if (gameOver)
        {
            DisableSpawners();
        }
        else
        {
            Time.timeScale = 0f;
            DisableSpawners();
        }
        
        if (MenuLoop && !UIController.inMenu) UIController.ShowMenu();

        if (ShopLoop && !UIController.inShop) UIController.ShowShop();
        
    }

    public void StartGame()
    {
        LoadNewStage();
    }

    public void LoadNewStage()
    {
        Transform spawnPoint = GetNewSpawnPoint();
        ScoreManager.ResetScore();
        SpawnSpawners(spawnPoint);
        SetCameraPosition(spawnPoint);
        SetPlayerLocation(spawnPoint);

        MenuLoop = false;
        GameLoop = true;

        UIController.ShowGame();
    }

    private void SpawnSpawners(Transform location)
    {

    }

    private void SpawnEnemies(Transform location)
    {
        EnableEnemies();
        EnemyManager.ResetEnemies();
    }

    public void ResetStage()
    {
        ScoreManager.ResetScore();
        ResetEnemies();
        ResetSpawners();
        ResetCameraPosition();
        ResetPlayer();

        MenuLoop = false;
        GameLoop = true;

        UIController.ShowGame();
    }

    public void SetPlayerLocation(Transform location)
    {
        Player.GetComponent<Player>().SetLocation(location);
        
    }

    public void ResetPlayer()
    {
        Player.GetComponent<Player>().Reset();
    }

    public void SetCameraPosition(Transform location)
    {
        MainCamera.GetComponent<FollowPlayer>().SetNewPosition(location);
    }

    public void ResetCameraPosition()
    {
        MainCamera.GetComponent<FollowPlayer>().ResetPosition();
    }

    public void ResetSpawners()
    {
        EnableSpawners();
        SpawnManager.ResetSpawners();
    }

    private void ResetEnemies()
    {
        EnableEnemies();
        EnemyManager.ResetEnemies();
    }

    private void EnableSpawners()
    {
        SpawnManager.enabled = true;
    }

    private void EnableEnemies()
    {
        EnemyManager.enabled = true;
    }

    private void DisableSpawners()
    {
        SpawnManager.DisableAll();
        SpawnManager.enabled = false;
    }

    private void DisableEnemies()
    {
        EnemyManager.DisableAll();
    }

    public bool LegitGameOver()
    {
        return (EnemyManager.EnemiesInGame.Count == 0 && (EnemyManager.GetEnemiesSlain() == SpawnManager.GetEnemiesSpawned()) && SpawnManager.IsDoneSpawning());
    }

    public void GameOver()
    {
        if(GameLoop)
        {
            GameLoop = false;
            gameOver = true;
            ScoreManager.RecordScore();
            if (EnemyManager.GetNumberOfEnemiesAlive() > 0) EnemyManager.DisableAll();
            UIController.ShowGameOver();
            Debug.Log("waitin");
            StartCoroutine(WaitForGameOverScreen());
        } 
    }

    private IEnumerator WaitForGameOverScreen()
    {
        yield return new WaitForSeconds(UIController.GameOverScreenTime);
        Debug.Log("Reactivating");
        gameOver = false;
        MenuLoop = true;
        ResetGame();
    }

    private void ResetGame()
    {
        UIController.ShowMenu();
        
        GameLoop = false;
        gameOver = false;
        MenuLoop = true;
        ShopLoop = false;

        LoadNewStage();
    }

    public bool SuccessfulRol(float DropChance)
    {
        if (UnityEngine.Random.Range(0, 100) <= DropChance)
        {
            return true;
        }
        else return false;
    }

    public void Quit()
    {
        DataSaver.Save();
        // SAVE AND STUFF ?
    }

    private Transform GetNewSpawnPoint()
    {
        return spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
    }
}
