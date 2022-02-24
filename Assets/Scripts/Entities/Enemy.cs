﻿using System;
using UnityEngine;

public class Enemy : Character

{
    [SerializeField]
    protected int XP;

    [SerializeField]
    protected int Score;

    [SerializeField]
    protected GameObject[] Drops;

    [SerializeField]
    protected float AggroRange;

    void Start()
    {
        GameManager.Instance.GetComponent<EnemyManager>().EnemiesInGame.Add(this.gameObject);
    }

    void Update()
    {

    }

    public void Die()
    {
        if(Drops.Length != 0) GameManager.Instance.GetComponent<DropManager>().AttemptDrop(Drops);
        GameManager.Instance.Player.GetComponent<Player>().IncreaseXP(XP);
        GameManager.Instance.GetComponent<ScoreManager>().IncreaseScore(Score);
        GameManager.Instance.GetComponent<EnemyManager>().IncreaseEnemiesSlain(1);
        GameManager.Instance.GetComponent<EnemyManager>().Remove(this.gameObject);
        // Animator.SetBool("Alive", false);
        Destroy(this.gameObject);
    }

    protected bool CollidedWithPlayer(Collider2D collision)
    {
        if (collision.CompareTag("Player")) return true;
        else return false;
    }

    public float GetAggroRange()
    {
        return AggroRange;
    }

}
