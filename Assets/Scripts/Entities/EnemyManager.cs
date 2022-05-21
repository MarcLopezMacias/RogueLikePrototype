using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private int EnemiesSlain = 0;
    public List<GameObject> EnemiesInGame;

    void Start()
    {
        StartCoroutine(UpdateEnemiesInGame());
    }

    void FixedUpdate()
    {
        if ((EnemiesInGame.Count == 0 &&
            GameManager.Instance.GetComponent<SpawnManager>().GetEnemiesSpawned() > 0 &&
            EnemiesSlain == 0)
            ||
            (EnemiesInGame.Count > 0 &&
            GameManager.Instance.GetComponent<SpawnManager>().GetEnemiesSpawned() == GameManager.Instance.GetComponent<SpawnManager>().GetEnemiesToSpawn()))
        {
            EnemiesInGame.Clear();
            
        }


        if (EnemiesInGame.Count == 0 && GetEnemiesSlain() > 0)
        {
            GameManager.Instance.GetComponent<ScoreManager>().IncreaseScore(5);
            GameManager.Instance.GameOver();
        }
        
    }

    private IEnumerator UpdateEnemiesInGame()
    {
        Debug.Log($"Updating enemies in game");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            EnemiesInGame.Add(enemy);
        }
        yield return new WaitForSeconds(1);
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

    public void EnableAll()
    {
        foreach (GameObject enemy in EnemiesInGame)
        {
            enemy.GetComponent<Enemy>().enabled = true;
        }
    }

    void OnEnable()
    {
        Start();
        if (EnemiesInGame.Count == 0)
        {
            DisableAll();
        }
    }

    public void ResetEnemies()
    {
        foreach (GameObject enemy in EnemiesInGame)
        {
            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            if (enemyComponent.enabled == false) enemyComponent.enabled = true;
            enemyComponent.Reset();
        }
    }


}
