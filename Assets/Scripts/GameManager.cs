using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    /*
     * GameManager: que gestiona l'estat del joc, el reinici de partida 
     * i serveix per comunicar els elements de partida.
     */

    public static  GameManager Instance { get { return _instance; } }
    private static GameManager _instance;

    public GameObject Player { get { return _player; } }
    private GameObject _player;

    public static UIController UIController;

    public List<GameObject> EnemiesInGame;

    private static GameObject MainCamera;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            Debug.Log("GameManager DESTROYED");
        }
        _instance = this;
        DontDestroyOnLoad(_instance);
    }

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player");

        UIController = GameObject.Find("Canvas").GetComponent<UIController>();

        MainCamera = GameObject.FindWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int GetNumberOfEnemiesAlive()
    {
        return EnemiesInGame.Count;
    }


    /* PER FER EL CANVI ENTRE SCENES ESCALABLE, ES PODRIA CREAR
     * UN "SCENE_CHANGER" O ALGO AIXI
     */

    public void ResetStage()
    {
        ResetCameraPosition();
        ResetPlayerPosition();
    }

    public void ResetPlayerPosition()
    {
        Player.GetComponent<Player>().ResetPosition();
    }

    public void ResetCameraPosition()
    {
        MainCamera.GetComponent<FollowPlayer>().ResetPosition();
    }

    public void GameOver()
    {
        UIController.GameOver();
        StartCoroutine(WaitForGameOverScreen());
    }

    private void ResetGame()
    {
        GoToStartScreen();
    }

    private IEnumerator WaitForGameOverScreen()
    {
        yield return new WaitForSeconds(UIController.GameOverScreenTime);
        ResetGame();
    }

    private void GoToStartScreen()
    {
        SceneManager.LoadScene("E5_Start");
    }

    public void GoToGameScreen()
    {
        SceneManager.LoadScene("E5");
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
