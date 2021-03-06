using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    [SerializeField]
    public GameObject impactEffect;

    private bool selfDestructing = false;

    [SerializeField]
    public int bulletLifeTime = 4;

    [SerializeField]
    public int bulletDamage;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!selfDestructing)
        {
            StartCoroutine(WaitNDestroy());
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.gameObject.tag)
        {
            case "Enemy":
                GameManager.Instance.GetComponent<ScoreManager>().IncreaseScore(1);
                Weapon wep = GameManager.Instance.Player.GetComponentInChildren<Weapon>();
                if (wep != null) 
                    collider.GetComponent<Enemy>().Damage(wep.weaponData.Damage);
                else
                    collider.GetComponent<Enemy>().Damage(1);
                Impact();
                break;
            case "Wall":
                Impact();
                break;
            case "Player":
                Debug.Log("Hit player");
                collider.GetComponent<Player>().Damage(bulletDamage);
                Impact();
                break;


        }
    }

    private void Impact()
    {
        Instantiate(impactEffect, transform.position, Quaternion.identity);
        StartCoroutine(WaitNDestroy());
    }

    private IEnumerator WaitNDestroy()
    {
        selfDestructing = true;
        yield return new WaitForSeconds(bulletLifeTime);
        Destroy(gameObject);
    }


}
