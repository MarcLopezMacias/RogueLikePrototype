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
    public static StageManager StageManager;

    public Inventory PlayerInventory;
    public Weapon PlayerWeaponComponent;

    public bool GameLoop = false, gameOver = false, finishedRoom = false, MenuLoop = true, ShopLoop = false, OptionsLoop = false;

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

        StageManager = gameObject.GetComponent<StageManager>();

        PlayerInventory = gameObject.GetComponent<Inventory>();

        StageManager.DisableSpawners();
        StageManager.DisableEnemies();

        DataSaver.Load();

    }

    void Update()
    {
        if (GameLoop)
        {
            Time.timeScale = 1f;

            // REFRESH NULL COMPONENTS
            if (_player == null) _player = GameObject.FindWithTag("Player");
            if (PlayerInventory == null) PlayerInventory = gameObject.GetComponent<Inventory>();
            if (_player != null && PlayerWeaponComponent == null) PlayerWeaponComponent = GameManager.Instance.Player.GetComponentInChildren<Weapon>();

            finishedRoom = (EnemyManager.GetNumberOfEnemiesAlive() == 0 && (EnemyManager.GetEnemiesSlain() == SpawnManager.GetNumberOfEnemiesSpawned()) && SpawnManager.IsDoneSpawning());

            if (finishedRoom)
            {
                FinishRound();
                finishedRoom = false;
            }
        } 
        else if (gameOver)
        {
            // SMTH ?
        }
        else
        {
            Time.timeScale = 0f;
        }

        if (MenuLoop && !UIController.inMenu) UIController.ShowMenu();
        else if (OptionsLoop && !UIController.inOptions) UIController.ShowPanel();
        else if (ShopLoop && !UIController.inShop) UIController.OpenShop();
    }

    public void StartGame()
    {
        GameLoop = true;
        MenuLoop = false;

        UIController.HideMenu();
        UIController.ShowGame();
        
        StageManager.LoadNewStage();

        UIController.ShowGame();
    }

    public void FinishRound()
    {
        if (GameLoop)
        {
            GameLoop = false;
            gameOver = true;
            StageManager.RoomCompleted();
            ScoreManager.RecordScore();
            UIController.ShowGameOver();
            Debug.Log("waitin");
            StartCoroutine(WaitForGameOverScreen());
        }
    }

    public void GameOver()
    {
        if(GameLoop)
        {
            GameLoop = false;
            gameOver = true;
            EnemyManager.DestroyEnemies();
            UIController.ShowGameOver();
            PlayerInventory.ResetInventory();
            ScoreManager.ResetCoins();
            // Reset Inventory ?
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


}
