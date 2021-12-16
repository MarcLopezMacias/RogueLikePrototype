using System;
using UnityEngine;

public class Enemy : Character

{
    // EXP OR SCORE AWARDED
    [SerializeField]
    protected int XP;

    [SerializeField]
    protected int Score;

    [SerializeField]
    protected GameObject[] Drops;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.GetComponent<EnemyManager>().EnemiesInGame.Add(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected void Die()
    {
        GameManager.Instance.GetComponent<DropManager>().AttemptDrop(Drops);
        GameManager.Instance.GetComponent<XPManager>().IncreaseXP(XP);
        GameManager.Instance.GetComponent<ScoreManager>().IncreaseScore(Score);
        GameManager.Instance.GetComponent<EnemyManager>().IncreaseEnemiesSlain(1);
        GameManager.Instance.GetComponent<EnemyManager>().Remove(this.gameObject);
        Destroy(this.gameObject);
    }

    protected bool CollidedWithPlayer(Collider2D collision)
    {
        if (collision.CompareTag("Player")) return true;
        else return false;
    }



}
