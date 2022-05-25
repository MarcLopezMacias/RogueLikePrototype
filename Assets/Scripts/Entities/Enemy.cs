using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : Character, IKillable, IDamageable<float>, IHealable<float>, IShootable<int>

{
    [SerializeField]
    public EnemyData enemyData;
    public Animator animator;
    public Rigidbody2D rb;

    private Vector3 faceDirection;
    private Vector3 target;

    private int minShootingCDTime = 1, maxShootingCDTime = 20;
    private int hitMissChance = 60;
    private bool canShoot = true;

    private EnemyData enemyInstance;

    [SerializeField]
    public GameObject bulletType;
    [SerializeField]
    public Transform firePoint;

    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void Start()
    {
        enemyInstance = Instantiate(enemyData);
        enemyInstance.ResetStats();
        GameManager.Instance.GetComponent<EnemyManager>().EnemiesInGame.Add(this.gameObject);
    }

    void Update()
    {
        if (enemyInstance.Health > 0) animator.SetBool("Alive", true);
        else animator.SetBool("Alive", false);
    }

    void FixedUpdate()
    {
        // IF ITS A CRATE OR WHATEVER
        if (!enemyInstance.Aggressive && enemyInstance.Static)
        {
            // celebrate
        }
        // IF ITS A REGULAR ENEMY
        else if (enemyInstance.Aggressive && !enemyInstance.Static) 
        {
            Aim();
            Move();
            Animate(false);
        }
        // IF ITS A STATIC ENEMY
        else if(enemyInstance.Aggressive && enemyInstance.Static)
        {
            Aim();
            if (canShoot && UnityEngine.Random.Range(0, 100) <= hitMissChance) Shoot(enemyInstance.AttackDamage);
            Animate(true);
        }
    }

    public void Die()
    {
        GameManager.Instance.Player.GetComponent<Player>().playerData.IncreaseXP(enemyInstance.XP);
        GameManager.Instance.GetComponent<ScoreManager>().IncreaseScore(enemyInstance.Score);
        GameManager.Instance.GetComponent<EnemyManager>().IncreaseEnemiesSlain(1);
        GameManager.Instance.GetComponent<EnemyManager>().Remove(this.gameObject);
        AttemptDrop(enemyInstance.DropList);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CollidedWithPlayer(collision))
        {
            Attack(enemyInstance.AttackDamage);
            Damage(GameManager.Instance.Player.GetComponent<Player>().playerData.BumpDamage);
        }
    }

    public void Attack(int AttackDamage)
    {
        GameManager.Instance.Player.GetComponent<Player>().Damage(AttackDamage);
    }

    protected bool CollidedWithPlayer(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IncreaseScore();
            return true;
        }
        else return false;
    }

    private bool AttemptDrop(GameObject[] dropList)
    {
        foreach (GameObject drop in dropList)
        {
            ItemData item = drop.GetComponent<Item>().specificItemData;
            float chance = item.DropChance;
            float upToOneHundred = UnityEngine.Random.Range(0, 100);
            if (upToOneHundred <= chance)
            {
                Debug.Log($"Dropped {item.Name}");
                Drop(drop);
            }
        }
        return false;
    }

    private void Drop(GameObject drop)
    {
        Instantiate(drop, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.identity);
    }

    public void Kill()
    {
        enemyInstance.Lifes -= 1;
        if (enemyInstance.Lifes <= 0)
            Die();
    }

    public void Heal(float amountHealed)
    {
        enemyInstance.Health += amountHealed;
    }

    public void Damage(float damageTaken)
    {
        enemyInstance.Health -= damageTaken;
        if (enemyInstance.Health <= 0)
        {
            Kill();
        }
    }

    private void IncreaseScore()
    {
        GameManager.Instance.GetComponent<ScoreManager>().IncreaseScore(1);
    }

    private void Aim()
    {
        // ColliderDistance2D Distance = gameObject.GetComponent<BoxCollider2D>().Distance(GameManager.Instance.Player.GetComponent<BoxCollider2D>());
        ColliderDistance2D Distance = gameObject.GetComponent<BoxCollider2D>().Distance(GameObject.FindWithTag("Player").GetComponent<BoxCollider2D>());
        if (Distance.distance <= enemyInstance.AggroRange)
        {
            // target = GameManager.Instance.Player.transform.position;
            target = GameObject.FindWithTag("Player").transform.position;
            faceDirection = target - gameObject.transform.position;
        }
    }

    private void Move()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(faceDirection.x, faceDirection.y).normalized;
    }

    private void Animate(bool isStatic)
    {
        if (!isStatic)
        {
            animator.SetFloat("Horizontal", faceDirection.x);
            animator.SetFloat("Vertical", faceDirection.y);
        }
        else
        {
            float aimAngle = Mathf.Atan2(faceDirection.y, faceDirection.x) * Mathf.Rad2Deg;
            rb.rotation = aimAngle;
        }
    }

    public void Shoot(int amount)
    {
        StartCoroutine(ShootCD());

        GameObject projectile = Instantiate(bulletType, firePoint.position, gameObject.transform.rotation);
        projectile.GetComponent<Rigidbody2D>().AddForce(faceDirection * amount, ForceMode2D.Impulse);

        // RaycastHit2D hit = Physics2D.Raycast(transform.position, target);
        // Player player = hit.collider.transform.gameObject.GetComponent<Player>();
        // if (player != null)
        // {
        //     player.Damage(amount);
        // }
    }

    private IEnumerator ShootCD()
    {
        canShoot = false;
        float shootingCD = UnityEngine.Random.Range(minShootingCDTime, maxShootingCDTime);
        yield return new WaitForSeconds(shootingCD);
        canShoot = true;
    }

    public void Reset()
    {
        Start();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}