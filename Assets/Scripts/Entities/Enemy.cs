﻿using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : Character, IKillable, IDamageable<float>, IHealable<float>

{
    [SerializeField]
    public EnemyData enemyData;
    public Animator animator;

    void Start()
    {
        GameManager.Instance.GetComponent<EnemyManager>().EnemiesInGame.Add(this.gameObject);
        enemyData.ResetStats();
    }

    void Update()
    {

    }

    public void Die()
    {
        GameManager.Instance.Player.GetComponent<Player>().playerData.IncreaseXP(enemyData.XP);
        GameManager.Instance.GetComponent<ScoreManager>().IncreaseScore(enemyData.Score);
        GameManager.Instance.GetComponent<EnemyManager>().IncreaseEnemiesSlain(1);
        GameManager.Instance.GetComponent<EnemyManager>().Remove(this.gameObject);
        // Animator.SetBool("Alive", false);
        AttemptDrop(enemyData.DropList);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CollidedWithPlayer(collision))
        {
            Attack(enemyData.AttackDamage);
            Damage(GameManager.Instance.Player.GetComponent<Player>().playerData.BumpDamage);
        }
    }

    public void Attack(int AttackDamage)
    {
        GameManager.Instance.Player.GetComponent<Player>().Damage(AttackDamage);
    }

    protected bool CollidedWithPlayer(Collider2D collision)
    {
        if (collision.CompareTag("Player")) return true;
        else return false;
    }

    private bool AttemptDrop(GameObject[] Drops)
    {
        foreach (GameObject Drop in Drops)
        {
            if (Drop.GetComponent<ItemData>().DropChance <= UnityEngine.Random.Range(0, 100)) return true; else return false;
        }
        return false;
    }

    private void Drop(GameObject drop)
    {
        Instantiate(drop, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.identity);
    }

    public void Kill()
    {
        enemyData.Lifes -= 1;
        if (enemyData.Lifes <= 0)
            gameObject.GetComponent<Enemy>().Die();
    }

    public void Heal(float amountHealed)
    {
        enemyData.Health += amountHealed;
    }

    public void Damage(float damageTaken)
    {
        enemyData.Health -= damageTaken;
        if (enemyData.Health <= 0)
        {
            Kill();
        }
    }

}