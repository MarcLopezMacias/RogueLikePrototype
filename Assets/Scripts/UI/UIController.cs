using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class UIController : MonoBehaviour
{
   [SerializeField]
    public bool inGame, inMenu, inShop, inOptions, musicMuted, effectsMuted;

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
    private string GameOverString = "G A M E  O V E R";
    public int GameOverScreenTime;

    public Text EnemiesText;
    public Text EnemiesSlainText;

    public Text BulletText;

    // MENU
    public Canvas MenuCanvas;
    public Text HighestScoreText;
    public Text RoomsCompletedText;
    public Text lastScoresText;

    private bool showingMenu;

    public Button optionsButton;

    public Button muteMusicButton, muteEffectsButton;

    // CONFIG
    public GameObject optionsPanel;
    public Slider musicSlider;
    public Slider effectsSlider;
    public Button closeOptionsButton;

    // SHOP
    public Canvas ShopCanvas;



    // Start is called before the first frame update
    void Start()
    { 
        playButton.onClick.AddListener(() => GameManager.Instance.StartGame());
        shopButton.onClick.AddListener(() => ShowShop());
        quitButton.onClick.AddListener(() => GameManager.Instance.Quit());

        optionsButton.onClick.AddListener(() => OpenOptions());
        closeOptionsButton.onClick.AddListener(() => CloseOptions());

        muteMusicButton.onClick.AddListener(() => MuteMusic());
        muteEffectsButton.onClick.AddListener(() => MuteEffects());

        CloseOptions();
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.GameLoop)
        {
            ShowGame();
        }
        else if(GameManager.Instance.gameOver)
        {
            ShowGameOver();
        }
        else if (GameManager.Instance.OptionsLoop)
        {
            UpdateVolumeValues();
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
        GameCanvas.enabled = true;

        UpdateLifes();
        UpdateHealth();

        UpdateThreatsInRoom();

        UpdateScore();

        UpdateBullets();

        HideGameOverUI();
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

        int roomsCompleted = GameManager.Instance.GetComponent<StageManager>().GetRoomsCompleted();
        RoomsCompletedText.text = ($"Rooms Completed\n" +
            $"{roomsCompleted}");

        yield return new WaitForSeconds(1);
    }

    public void ShowGame()
    {
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

    private void OpenOptions()
    {
        ShowPanel();
    }

    public void ShowPanel()
    {
        GameManager.Instance.OptionsLoop = true;
        GameManager.Instance.MenuLoop = false;
        optionsPanel.gameObject.SetActive(true);
        UpdateVolumeValues();
    }

    private void CloseOptions()
    {
        HidePanel();
        ShowMenu();
    }

    private void HidePanel()
    {
        GameManager.Instance.OptionsLoop = false;
        GameManager.Instance.MenuLoop = true;
        optionsPanel.gameObject.SetActive(false);
    }

    private void UpdateVolumeValues()
    {
        UpdateMusic();
        UpdateSFX();
    }

    private void UpdateMusic()
    {
        AudioSource music = GameManager.Instance.GetComponent<SoundController>().musicSource;
        if (!musicMuted) music.volume = musicSlider.value;
        else music.volume = 0;
    }

    private void UpdateSFX()
    {
        AudioSource effects = GameManager.Instance.GetComponent<SoundController>().effectsSource;
        if (!effectsMuted) effects.volume = effectsSlider.value;
        else effects.volume = 0;
    }

    private void MuteMusic()
    {
        musicMuted = !musicMuted;
    }

    private void MuteEffects()
    {
        effectsMuted = !effectsMuted;
    }
}
