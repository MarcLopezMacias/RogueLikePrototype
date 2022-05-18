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

    public Text BulletText;

    // Start is called before the first frame update
    void Start()
    { 
        GameOverString = "G A M E  O V E R";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(GameManager.Instance.InGameScene())
        {
            if (GameManager.Instance.MainLoop)
            {
                UpdateLifes();
                UpdateHealth();

                UpdateThreatsInRoom();

                UpdateScore();

                UpdateBullets();

                HideGameOverUI();

            }
            else
            {
                HideRegularUI();
                ShowGameOverUI();
            }
        }
    }

    public void GameOver()
    {
        StartCoroutine(DisplayGameOverScreen());
    }

    private IEnumerator DisplayGameOverScreen()
    {
        yield return new WaitForSeconds(GameOverScreenTime);
        
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

    private void HideRegularUI()
    {
        LifesText.text = "";
        HealthText.text = "";
        EnemiesText.text = "";
        MaxScoreText.text = "";
    }

    private void ShowGameOverUI()
    {
        GameOverText.text = GameOverString;
        GameOverScoreText.text = "Final Score: " + GameManager.Instance.GetComponent<ScoreManager>().GetScore();

        EnemiesSlainText.text = "Enemies Slain: " + GameManager.Instance.GetComponent<EnemyManager>().GetEnemiesSlain();
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
}
