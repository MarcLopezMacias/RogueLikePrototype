using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    [SerializeField]
    private int MoveSpeedDividingFactor;

    float AggroRange;

    float MoveSpeed;

    private Animator Animator;

    private Rigidbody2D EnemyRigidBody;

    Vector3 target;

    ColliderDistance2D Distance;

    Vector2 faceDirection;

    // Start is called before the first frame update
    void Start()
    {
        MoveSpeed = gameObject.GetComponent<Enemy>().GetSpeed();
        MoveSpeed /= MoveSpeedDividingFactor;
        // TO FIX
        AggroRange = gameObject.GetComponent<Enemy>().GetAggroRange();

        Animator = gameObject.GetComponent<Animator>();

        EnemyRigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        Animate();
    }

    private void Move()
    {
        Distance = gameObject.GetComponent<BoxCollider2D>().Distance(GameManager.Instance.Player.GetComponent<BoxCollider2D>());
        
        //ShowDistanceFromPlayer();

        if (Distance.distance < AggroRange)
        {
            target = GameManager.Instance.Player.transform.position;
            faceDirection = target - gameObject.transform.position;
            EnemyRigidBody.velocity = new Vector2(faceDirection.x, faceDirection.y).normalized;
        }
    }

    private void Animate()
    {
        Animator.SetFloat("Horizontal", faceDirection.x);
        Animator.SetFloat("Vertical", faceDirection.y);
    }

    private void ShowDistanceFromPlayer()
    {
        Debug.Log("Enemy distance from Player: " + Distance.distance);
    }

}
