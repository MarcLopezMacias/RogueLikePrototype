using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : Item
{

    // Start is called before the first frame update
    void Start()
    {
        Amount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CollidedWithPlayer(collision))
        {
            GameObject cl = collision.gameObject;
            cl.GetComponent<Player>().IncreaseLifes(Amount);
        }
        Die();
    }
}
