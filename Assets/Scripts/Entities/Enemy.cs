using System;
using UnityEngine;

public class Enemy : Character, IDropper<GameObject>

{

    // EXP OR SCORE AWARDED
    [SerializeField]
    protected int XP;

    // DAMAGE THAT IS ABLE TO DEAL
    [SerializeField]
    protected int Damage;

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
        if(SuccessfulRol())
        {
            // Drop(Loot[GameManager.Instance.RandomNumber]);
        }
        GameManager.Instance.IncreaseScore(XP);
        GameManager.Instance.Player.GetComponent<Player>().IncreaseEnemiesSlain(1);
        GameManager.Instance.EnemiesInGame.Remove(this.gameObject);
        Destroy(this.gameObject);
    }

    protected bool CollidedWithPlayer(Collider2D collision)
    {
        if (collision.CompareTag("Player")) return true;
        else return false;
    }

    public void Drop(GameObject drop)
    {
        // TODO
    }

    private bool SuccessfulRol()
    {
        // BULLSHIE
        if (0 == 0)
        {
            return true;
        }
        // TODO
    }
}
