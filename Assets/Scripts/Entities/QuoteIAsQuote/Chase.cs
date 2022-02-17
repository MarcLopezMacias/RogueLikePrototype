using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    [SerializeField]
    private int MoveSpeedDividingFactor;

    float AggroRange;

    float MoveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        MoveSpeed = gameObject.GetComponent<Enemy>().GetSpeed();
        MoveSpeed /= MoveSpeedDividingFactor;
        // TO FIX
        AggroRange = gameObject.GetComponent<Enemy>().GetAggroRange();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // TO FIX
        ColliderDistance2D Distance = gameObject.GetComponent<BoxCollider2D>().Distance(GameManager.Instance.Player.GetComponent<BoxCollider2D>());
        if (Distance.distance < AggroRange)
        {
            Vector3 target = GameManager.Instance.Player.transform.position;
            transform.LookAt(target);
            transform.Rotate(new Vector3(0, -90, 0), Space.Self);//correcting the original rotation
            transform.Translate(new Vector3(MoveSpeed * Time.deltaTime, 0, 0));
        }
    }

}
