using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunEnemy : MonoBehaviour
{

    [SerializeField]
    private int stunTime = 3;

    void Start() 
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            StartCoroutine(Stun(collision.gameObject.GetComponent<Enemy>()));
        }
    }

    private IEnumerator Stun(Enemy enemy)
    {
        enemy.Stunned = true;
        yield return new WaitForSeconds(stunTime);
        enemy.Stunned = false;
    }

}
