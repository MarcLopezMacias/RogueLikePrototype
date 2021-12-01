using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    protected int XP;

    [SerializeField]
    protected int Damage;

    /*
     * Enemic: Crear un prefab amb un element visual dels que es proporcionen. 
     * Aquest objecte enemic quan sigui a l'escena caminarà en direcció al Player.
     * (Bonus) Prefab amb animacions
     * 
     * Mort: si el enemic toca al jugador aquest  l'enemic s'elimina i el player
     * perd una vida. Si l'enemic toca a un altre enemic els dos s'eliminen. 
     * Si player perd dos vides s'acaba el joc. Si el joc s'acaba torna a 
     * iniciar-se la partida.
     */

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.EnemiesInGame.Add(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected void Die()
    {
        GameManager.Instance.IncreaseScore(XP);
        GameManager.Instance.Player.GetComponent<DataPlayer>().IncreaseEnemiesSlain(1);
        GameManager.Instance.EnemiesInGame.Remove(this.gameObject);
        Destroy(this.gameObject);
    }

    protected bool CollidedWithPlayer(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("onFloor") || collision.CompareTag("Jumping")) return true;
        else return false;
    }
}
