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
    public static ScoreManager ScoreManager;
    public static EnemyManager EnemyManager;
    public static SpawnManager SpawnManager;

    public Inventory PlayerInventory;
    public Weapon PlayerWeaponComponent;

    private static GameObject MainCamera;

    public bool GameLoop = false, gameOver = false, MenuLoop = true, ShopLoop = false;

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

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        PlayerInventory = _player.GetComponent<Inventory>();

        UIController = gameObject.GetComponent<UIController>();
        ScoreManager = gameObject.GetComponent<ScoreManager>();

        EnemyManager = gameObject.GetComponent<EnemyManager>();
        SpawnManager = gameObject.GetComponent<SpawnManager>();

        MainCamera = GameObject.FindWithTag("MainCamera");

        PlayerInventory = GameObject.Find("Inventory").GetComponent<Inventory>();

        DisableEverything();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameLoop)
        {
            Time.timeScale = 1f;

            // REFRESH NULL COMPONENTS
            if (_player == null) _player = GameObject.FindWithTag("Player");
            if (PlayerInventory == null) PlayerInventory = GameObject.Find("Inventory").GetComponent<Inventory>();
            if (_player != null && PlayerWeaponComponent == null) PlayerWeaponComponent = GameManager.Instance.Player.GetComponentInChildren<Weapon>();
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

        if (LegitGameOver())
        {
            ScoreManager.IncreaseScore(5);
            GameOver();
        }
    }

    public void StartGame()
    {
        MenuLoop = false;
        ResetStage();
    }

    public void ResetStage()
    {
        MenuLoop = false;
        GameLoop = true;
        EnableSpawners();
        ResetCameraPosition();
        ResetPlayer();
        ResetSpawners();
        UIController.ShowGame();
    }

    public void ResetPlayer()
    {
        Player.GetComponent<Player>().Reset();
    }

    public void ResetCameraPosition()
    {
        MainCamera.GetComponent<FollowPlayer>().ResetPosition();
    }

    public void ResetSpawners()
    {
        SpawnManager.ResetSpawners();
    }

    private void EnableSpawners()
    {
        SpawnManager.enabled = true;
    }

    private void DisableSpawners()
    {
        SpawnManager.DisableAll();
        SpawnManager.enabled = false;
    }

    private void DisableEverything()
    {
        DisableSpawners();
        DisableEnemies();
    }

    private void DisableEnemies()
    {
        EnemyManager.DisableAll();
    }

    public bool LegitGameOver()
    {
        return (EnemyManager.EnemiesInGame.Count == 0 && (EnemyManager.GetEnemiesSlain() > 0) && SpawnManager.IsDoneSpawning());
    }

    public void GameOver()
    {
        if(GameLoop)
        {
            GameLoop = false;
            gameOver = true;
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

        if (_player != null) _player.GetComponent<Player>().Reset(); UIController.ShowMenu();
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
        // SAVE AND STUFF ?
    }
}
