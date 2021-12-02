using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerClass
{
    Mage,
    Archer,
    Warrior,
    Sorcerer,
    Summoner,
    Fighter,
    Assassin,
    Rogue,
    Necromancer
}

public class Player : Character
    
{
    [SerializeField]
    private PlayerClass PlayerClass;

    private int enemiesSlain;

    private int Score;

    // Start is called before the first frame update
    void Start()
    {
        InitialPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public PlayerClass GetKind()
    {
        return PlayerClass;
    }

    private void Die()
    {
        ResetPosition();
        ResetStats();
    }

    private bool IsPlayerAlive()
    {
        if (Lifes > 0) return true; else return false;
    }

    private void OnDestroy()
    {
        
    }

    /*
     * RESET PLAYER POSITION TO STARTING POSITION WHEN IT DIES, AND CAN CONTINUE PLAYING (NO GAME OVER)
     */

    public void ResetPosition()
    {
        if (!IsPlayerAlive())
        {
            GameOver();
        }
        else
        {
            gameObject.transform.position = InitialPosition;
        }
    }

    public void GameOver()
    {
        GameManager.Instance.GameOver();
    }

    /*
     * ENEMIES SLAIN
     */

    public int GetEnemiesSlain()
    {
        return enemiesSlain;
    }

    public void IncreaseEnemiesSlain(int amount)
    {
        enemiesSlain += amount;
    }

    private void ResetStats()
    {
        ResetHealth();
    }

    private void ResetHealth()
    {
        Health = MaxHealth;
    }

    public int GetScore()
    {
        return Score;
    }

    public void IncreaseScore(int amount)
    {
        Score += amount;
    }

}
