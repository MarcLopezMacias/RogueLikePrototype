using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private int EnemiesSlain;
    public List<GameObject> EnemiesInGame;

    void FixedUpdate()
    {
        if (EnemiesInGame.Count == 0 && GetEnemiesSlain() > 0)
        {
            GameManager.Instance.GetComponent<ScoreManager>().IncreaseScore(5);
            GameManager.Instance.GameOver();
        }
    }

    public int GetNumberOfEnemiesAlive()
    {
        return EnemiesInGame.Count;
    }

    public void IncreaseEnemiesSlain(int value)
    {
        EnemiesSlain += value;
    }

    public void Remove(GameObject toRemove)
    {
        EnemiesInGame.Remove(toRemove);
    }

    public int GetEnemiesSlain()
    {
        return EnemiesSlain;
    }

    public void DisableAll()
    {
        foreach(GameObject enemy in EnemiesInGame)
        {
            enemy.GetComponent<Enemy>().enabled = false;
        }
    }

    void OnEnable()
    {
        if (EnemiesInGame.Count == 0)
        {
            DisableAll();
        }
    }

}
