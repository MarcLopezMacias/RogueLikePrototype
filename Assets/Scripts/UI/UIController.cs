using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    /*
     * UI: comptador de vides. Comptador d'enemics a l'escena. 
     * Comptador de punts per enemic 5.
     */

    public Text FrameText;
    private int FrameCount;

    public Text LifesText;
    public Text EnemiesText;
    public Text MaxScoreText;

    public Text GameOverText;
    public Text GameOverScoreText;
    private string GameOverString;
    public int GameOverScreenTime;

    public Text EnemiesSlainText;

    private bool mainLoop;

    // Start is called before the first frame update
    void Start()
    {
        mainLoop = true;
        FrameCount = 0;
        GameOverString = "GAME OVER";
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if(mainLoop)
        {
            LifesText.text = "Lifes: " + GameManager.Instance.Player.GetComponent<DataPlayer>().Lifes;

            EnemiesText.text = "Threats: " + GameManager.Instance.GetNumberOfEnemiesAlive();

            MaxScoreText.text = "Top Score: " + GameManager.Instance.GetScore();

            GameOverText.text = "";
            GameOverScoreText.text = "";
            EnemiesSlainText.text = "";
        } else
        {
            FrameText.text = "Frames: " + FrameCount;
            LifesText.text = "";
            EnemiesText.text = "";
            MaxScoreText.text = "";

            GameOverText.text = GameOverString;
            GameOverScoreText.text = "Final Score: " + GameManager.Instance.GetScore();

            EnemiesSlainText.text = "Enemies Slain: " + GameManager.Instance.Player.GetComponent<DataPlayer>().GetEnemiesSlain();
        }
        FrameCount++;
        FrameText.text = "Frames: " + FrameCount;
    }

    public void GameOver()
    {
        StartCoroutine(DisplayGameOverScreen());
    }

    private IEnumerator DisplayGameOverScreen()
    {
        mainLoop = false;
        yield return new WaitForSeconds(GameOverScreenTime);
        
    }
}
