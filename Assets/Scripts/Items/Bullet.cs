using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    [SerializeField]
    public Animation impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.gameObject.tag)
        {
            case "Enemy":
                GameManager.Instance.GetComponent<ScoreManager>().IncreaseScore(1);
                collider.GetComponent<Enemy>().Damage(GameManager.Instance.Player.GetComponentInChildren<Weapon>().weaponData.Damage);
                Impact();
                break;
            case "Wall":
                Impact();
                break;


        }
    }

    private void Impact()
    {
        animation.Play();
        // (impactEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }


}
