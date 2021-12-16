using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    protected int Amount;

    protected int DropChance;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    protected void Die()
    {
        Destroy(this.gameObject);
    }

    protected bool CollidedWithPlayer(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("onFloor") || collision.CompareTag("Jumping")) return true;
        else return false;
    }

    public float GetDropChance()
    {
        return DropChance;
    }
}
