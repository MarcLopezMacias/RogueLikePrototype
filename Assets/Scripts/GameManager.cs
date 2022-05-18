﻿using System;
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

    public Inventory PlayerInventory;
    public Weapon PlayerWeaponComponent;


    private static GameObject MainCamera;

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

        MainCamera = GameObject.FindWithTag("MainCamera");

        PlayerInventory = GameObject.Find("Inventory").GetComponent<Inventory>();

    }

    // Update is called once per frame
    void Update()
    {
        if(_player == null)
        {
            _player = GameObject.FindWithTag("Player");
        }
        if (PlayerInventory == null)
        {
            PlayerInventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        }
        if (_player != null && PlayerWeaponComponent == null)
        {
            PlayerWeaponComponent = GameManager.Instance.Player.GetComponentInChildren<Weapon>();
        }
    }

    public void ResetStage()
    {
        ResetCameraPosition();
        ResetPlayerPosition();
        ResetSpawners();
    }

    public void ResetPlayerPosition()
    {
        Player.GetComponent<Player>().ResetPosition();
    }

    public void ResetCameraPosition()
    {
        MainCamera.GetComponent<FollowPlayer>().ResetPosition();
    }

    public void ResetSpawners()
    {

    }

    public void GameOver()
    {
        UIController.GameOver();
        StartCoroutine(WaitForGameOverScreen());
    }

    private void ResetGame()
    {
        GoToStartMenu();
    }

    private IEnumerator WaitForGameOverScreen()
    {
        yield return new WaitForSeconds(UIController.GameOverScreenTime);
        ResetGame();
    }

    private void GoToStartMenu()
    {
        SceneManager.LoadScene("RogueLikeStartMenu");
    }

    public void GoToInGameScene()
    {
        SceneManager.LoadScene("RogueLikeInGame");
    }

    public bool SuccessfulRol(float DropChance)
    {
        if (UnityEngine.Random.Range(0, 100) <= DropChance)
        {
            return true;
        }
        else return false;
    }
}
