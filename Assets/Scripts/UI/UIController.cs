using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text LifesText;
    public Text HealthText;

    public Text MaxScoreText;

    public Text GameOverText;
    public Text GameOverScoreText;
    private string GameOverString;
    public int GameOverScreenTime;

    public Text EnemiesText;
    public Text EnemiesSlainText;

    private bool mainLoop;

    // Start is called before the first frame update
    void Start()
    {
        mainLoop = true;
        GameOverString = "G A M E  O V E R";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (mainLoop)
        {
            LifesText.text = "Lifes: " + GameManager.Instance.Player.GetComponent<Player>().GetLifes();
            HealthText.text = "HP: " + GameManager.Instance.Player.GetComponent<Player>().GetHealth()
            //   + " / " + GameManager.Instance.Player.GetComponent<Player>().GetMaxHealth();
            ;

            EnemiesText.text = "Threats: " + GameManager.Instance.GetComponent<EnemyManager>().GetNumberOfEnemiesAlive();

            MaxScoreText.text = "Top Score: " + GameManager.Instance.GetComponent<ScoreManager>().GetScore();

            GameOverText.text = "";
            GameOverScoreText.text = "";
            EnemiesSlainText.text = "";
        } else
        {
            LifesText.text = "";
            HealthText.text = "";
            EnemiesText.text = "";
            MaxScoreText.text = "";

            GameOverText.text = GameOverString;
            GameOverScoreText.text = "Final Score: " + GameManager.Instance.GetComponent<ScoreManager>().GetScore();

            EnemiesSlainText.text = "Enemies Slain: " + GameManager.Instance.GetComponent<EnemyManager>().GetEnemiesSlain();
        }
    }

    public void GameOver()
    {
        mainLoop = false;
        StartCoroutine(DisplayGameOverScreen());
    }

    private IEnumerator DisplayGameOverScreen()
    {
        mainLoop = false;
        yield return new WaitForSeconds(GameOverScreenTime);
        
    }
}
