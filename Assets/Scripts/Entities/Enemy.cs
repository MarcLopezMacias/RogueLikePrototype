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

    private bool shootingCooldown;
    private int shootingCooldownTime;

    private EnemyData enemyInstance;

    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void Start()
    {
        GameManager.Instance.GetComponent<EnemyManager>().EnemiesInGame.Add(this.gameObject);
        enemyInstance = Instantiate(enemyData);
        enemyInstance.ResetStats();
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
            Animate();
        }
        // IF ITS A STATIC ENEMY
        else if(enemyInstance.Aggressive && enemyInstance.Static)
        {
            Aim();
            Shoot(enemyInstance.AttackDamage);
            Animate();
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
            gameObject.GetComponent<Enemy>().Die();
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
        ColliderDistance2D Distance = gameObject.GetComponent<BoxCollider2D>().Distance(GameManager.Instance.Player.GetComponent<BoxCollider2D>());
        if (Distance.distance <= enemyInstance.AggroRange)
        {
            target = GameManager.Instance.Player.transform.position;
            faceDirection = target - gameObject.transform.position;
        }
    }

    private void Move()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(faceDirection.x, faceDirection.y).normalized;
    }

    private void Animate()
    {
        animator.SetFloat("Horizontal", faceDirection.x);
        animator.SetFloat("Vertical", faceDirection.y);
    }

    public void Shoot(int amount)
    {
        // "ANIMATE"
        float aimAngle = Mathf.Atan2(faceDirection.y, faceDirection.x) * Mathf.Rad2Deg;
        rb.rotation = aimAngle;

        // ATTEMPT SHOT
        if (!shootingCooldown)
        {
            Debug.Log("Shootin Machine");
            StartCoroutine(ShootingCooldown());
            RaycastHit2D hit = Physics2D.Raycast(transform.position, target);
            Player player = hit.collider.transform.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.Damage(amount);
            }
        }
    }

    private IEnumerator ShootingCooldown()
    {
        shootingCooldown = true;
        yield return new WaitForSeconds(shootingCooldownTime);
        shootingCooldown = false;
    }
}