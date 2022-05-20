using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public bool inGame, inMenu, inShop;

    [SerializeField]
    public Button playButton, shopButton, quitButton;

    // GAME
    public Canvas GameCanvas;

    public Text LifesText;
    public Text HealthText;

    public Text MaxScoreText;

    public Text GameOverText;
    public Text GameOverScoreText;
    private string GameOverString;
    public int GameOverScreenTime;

    public Text EnemiesText;
    public Text EnemiesSlainText;

    public Text BulletText;

    // MENU
    public Canvas MenuCanvas;


    // SHOP
    public Canvas ShopCanvas;



    // Start is called before the first frame update
    void Start()
    { 
        GameOverString = "G A M E  O V E R";

        playButton.onClick.AddListener(() => GameManager.Instance.StartGame());
        shopButton.onClick.AddListener(() => ShowShop());
        quitButton.onClick.AddListener(() => GameManager.Instance.Quit());

        ShowMenu();
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.GameLoop)
        {
            ShowGameUI();
        }
        else if (GameManager.Instance.MenuLoop)
        {
            ShowMenu();
        } 
        else if (GameManager.Instance.ShopLoop)
        {
            ShowShop();
        }
    }

    private void ShowGameUI()
    {
        UpdateLifes();
        UpdateHealth();

        UpdateThreatsInRoom();

        UpdateScore();

        UpdateBullets();

        HideGameOverUI();

        MenuCanvas.enabled = false;
        GameCanvas.enabled = true;
        ShopCanvas.enabled = false;
    }

    public void ShowGameOver()
    {
        ShowGameOverUI();
        HideGameUI();
    }

    private void UpdateLifes()
    {
        LifesText.text = "Lifes: " + GameManager.Instance.Player.GetComponent<Player>().playerData.Lifes;
    }

    private void UpdateHealth()
    {
        HealthText.text = "HP: " + GameManager.Instance.Player.GetComponent<Player>().playerData.Health
//   + " / " + GameManager.Instance.Player.GetComponent<Player>().GetMaxHealth();
;
    }

    private void UpdateThreatsInRoom()
    {
        EnemiesText.text = "Threats: " + GameManager.Instance.GetComponent<EnemyManager>().GetNumberOfEnemiesAlive();

    }

    private void UpdateScore()
    {
        MaxScoreText.text = "Top Score: " + GameManager.Instance.GetComponent<ScoreManager>().GetScore();

    }

    private void HideGameOverUI()
    {
        GameOverText.text = "";
        GameOverScoreText.text = "";
        EnemiesSlainText.text = "";
    }

    private void HideGameUI()
    {
        LifesText.text = "";
        HealthText.text = "";
        EnemiesText.text = "";
        EnemiesSlainText.text = "";
        MaxScoreText.text = "";
        BulletText.text = "";
    }

    private void ShowGameOverUI()
    {
        GameOverText.text = GameOverString;
        GameOverScoreText.text = "Final Score: " + GameManager.Instance.GetComponent<ScoreManager>().GetScore();

        EnemiesSlainText.text = "Enemies Slain: " + GameManager.Instance.GetComponent<EnemyManager>().GetEnemiesSlain();
        HideGameUI();
    }

    private void UpdateBullets()
    {
        Weapon weaponComponent;
        weaponComponent = GameManager.Instance.Player.GetComponentInChildren<Weapon>();
        if (weaponComponent.weaponData != null)
        {
            BulletText.text = weaponComponent.weaponData.CurrentBullets + " " + weaponComponent.weaponData.TotalBulletsLeft;
        }
        else
        {
            BulletText.text = "";
        }
    }

    public void ShowMenu()
    {
        MenuCanvas.enabled = true;
        HideGame();
        HideShop();
        GameManager.Instance.MenuLoop = true;
    }

    public void ShowGame()
    {
        GameCanvas.enabled = true;
        ShowGameUI();
        HideGameOverUI();

        HideMenu();
        HideShop();

        GameManager.Instance.GameLoop = true;
    }

    public void ShowShop()
    {
        ShopCanvas.enabled = true;
        HideGame();
        HideMenu();

        GameManager.Instance.ShopLoop = true;
    }

    private void HideShop()
    {
        ShopCanvas.enabled = false;
        GameManager.Instance.ShopLoop = false;
    }

    private void HideMenu()
    {
        MenuCanvas.enabled = false;
        GameManager.Instance.MenuLoop = false;
    }

    private void HideGame()
    {
        GameCanvas.enabled = false;
        GameManager.Instance.GameLoop = false;
    }
}
