﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : Enemy, IAttack<int>
{
    private Vector3 playerPosition, ownPosition;

    [SerializeField]
    private int AttackDamage;

    private float HeightChasingOffset;

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

    private bool InPlayerRange()
    {
        playerPosition = GameManager.Instance.Player.transform.position;
        ownPosition = gameObject.transform.position;
        if (playerPosition.y >= ownPosition.y - HeightChasingOffset)
        {
            return true;
        } else
        {
            return false;
        }
    }

    public void Attack(int AttackDamage)
    {
        GameManager.Instance.Player.GetComponent<Player>().Damage(AttackDamage);
    }

}
