using System;
using UnityEngine;

public class Enemy : Character

{
    // EXP OR SCORE AWARDED
    [SerializeField]
    protected int XP;

    [SerializeField]
    protected float DropChance;

    [SerializeField]
    protected GameObject ItemDrop;

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
        if(GameManager.Instance.SuccessfulRol(DropChance))
        {
           Drop(ItemDrop);
        }
        GameManager.Instance.Player.GetComponent<Player>().IncreaseScore(XP);
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
        Instantiate(drop, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.identity);
    }



}
