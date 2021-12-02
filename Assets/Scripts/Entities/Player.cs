using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * - Crear un component que recopila dades del personatge: 
 * Nom, Cognom, Tipus de personatge, alçada, velocitat, 
 * distància a recórrer. Aquest component ha de ser consultat 
 * per altres classes i editable des de l'inspector.
 * 
 * - Associar velocitat i pes: el personatge anirà més lent 
 * caminant com més gran sigui la variable "weight" del 
 * component "DataPlayer". La velocitat del personatge és 
 * inversament proporcional al pes del personatge.
 * 
 * Mort: si el enemic toca al jugador aquest  l'enemic s'elimina i 
 * el player perd una vida. Si l'enemic toca a un altre enemic els dos 
 * s'eliminen. Si player perd dos vides s'acaba el joc. Si el joc s'acaba
 * torna a iniciar-se la partida.
 * */

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

    [SerializeField]
    private int _lifes;
    public int Lifes { get { return _lifes; } }
    public int MaxLifes;

    private int Health;

    private int enemiesSlain;

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
        GameManager.Instance.ResetStage();
    }

    private bool IsPlayerAlive()
    {
        if (_lifes > 0) return true; else return false;
    }

    public int GetLifes()
    {
        return Lifes;
    }

    public void GameOver()
    {
        GameManager.Instance.GameOver();
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

}
