using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : Enemy, IAttack<int>
{

    [SerializeField]
    private int AttackDamage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CollidedWithPlayer(collision))
        {
            Attack(AttackDamage);
            Damage(GameManager.Instance.Player.GetComponent<Player>().GetBumpDamage());
        }
    }

    public void Attack(int AttackDamage)
    {
        GameManager.Instance.Player.GetComponent<Player>().Damage(AttackDamage);
    }

}
