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
    public Text GameOverHighestScoreText;
    public Text SetHighScoreText;
    private string GameOverString;
    public int GameOverScreenTime;

    public Text EnemiesText;
    public Text EnemiesSlainText;

    public Text BulletText;

    // MENU
    public Canvas MenuCanvas;
    public Text HighestScoreText;
    public Text lastScoresText;

    private bool showingMenu;

    // SHOP
    public Canvas ShopCanvas;



    // Start is called before the first frame update
    void Start()
    { 
        GameOverString = "G A M E  O V E R";

        playButton.onClick.AddListener(() => GameManager.Instance.StartGame());
        shopButton.onClick.AddListener(() => ShowShop());
        quitButton.onClick.AddListener(() => GameManager.Instance.Quit());
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.GameLoop)
        {
            ShowGameUI();
        }
        else if (GameManager.Instance.MenuLoop)
        {
            if (!showingMenu) ShowMenu();
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
        GameOverHighestScoreText.text = "";
        SetHighScoreText.enabled = false;
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
        GameOverHighestScoreText.text = ($"Highest Score: {GameManager.Instance.GetComponent<ScoreManager>().maxScore}");
        EnemiesSlainText.text = "Enemies Slain: " + GameManager.Instance.GetComponent<EnemyManager>().GetEnemiesSlain();
        GameOverHighestScoreText.text = $"Highest Score\n{GameManager.Instance.GetComponent<ScoreManager>().maxScore}";
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
        showingMenu = true;
        MenuCanvas.enabled = true;
        HideGame();
        HideShop();
        StartCoroutine(UpdateMenu());
    }

    public IEnumerator UpdateMenu()
    {
        int highestScore = GameManager.Instance.GetComponent<ScoreManager>().maxScore;
        HighestScoreText.text = ($"Highest Score\n" +
        $"{highestScore}");
       
        string lastScores = GameManager.Instance.GetComponent<ScoreManager>().lastScores.ToString();
        lastScoresText.text = ($"Latest scores\n" +
        $"{lastScores}");

        yield return new WaitForSeconds(1);
    }

    public void ShowGame()
    {
        GameCanvas.enabled = true;
        ShowGameUI();
        HideGameOverUI();

        HideMenu();
        HideShop();
    }

    public void ShowShop()
    {
        ShopCanvas.enabled = true;
        HideGame();
        HideMenu();
    }

    private void HideShop()
    {
        ShopCanvas.enabled = false;
    }

    private void HideMenu()
    {
        MenuCanvas.enabled = false;
    }

    private void HideGame()
    {
        GameCanvas.enabled = false;
    }

    public void SetHighScore()
    {
        SetHighScoreText.enabled = true;
    }
}
