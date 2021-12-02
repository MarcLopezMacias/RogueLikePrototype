﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : Enemy, IDropper<GameObject>, IAttack<float>
{

    private Chase chaserino;
    private Vector3 playerPosition, ownPosition;

    [SerializeField]
    private float AttackDamage;

    private float HeightChasingOffset;



    // Start is called before the first frame update
    void Start()
    {
        XP = 1;
        AttackDamage = 1;
        chaserino = gameObject.GetComponent<Chase>();
        HeightChasingOffset = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(InPlayerRange())
        {
            chaserino.enabled = true;
        } else
        {
            chaserino.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CollidedWithPlayer(collision))
        {
            Attack(AttackDamage);
        }
        Kill();
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

    public void Attack(float damageDone)
    {
        GameManager.Instance.Player.Damage(AttackDamage);
    }
}
