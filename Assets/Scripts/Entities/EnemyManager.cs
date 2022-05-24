using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int enemiesSlain = 0;
    public List<GameObject> EnemiesInGame;

    void Start()
    {
        StartCoroutine(UpdateEnemiesInGame());
    }

    void FixedUpdate()
    {
       
    }

    private IEnumerator UpdateEnemiesInGame()
    {
        Debug.Log($"Updating enemies in game");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        EnemiesInGame.Clear();
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
        enemiesSlain += value;
    }

    public void Remove(GameObject toRemove)
    {
        EnemiesInGame.Remove(toRemove);
    }

    public int GetEnemiesSlain()
    {
        return enemiesSlain;
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
