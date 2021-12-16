using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    [SerializeField]
    float MoveSpeed;

    [SerializeField]
    private int MoveSpeedDividingFactor;

    private bool Chasing;

    float AggroRange;

    // Start is called before the first frame update
    void Start()
    {
        MoveSpeed /= MoveSpeedDividingFactor;
        Chasing = false;
        // TO FIX
        AggroRange = gameObject.GetComponent<Enemy>().GetAggroRange();
    }

    // Update is called once per frame
    void Update()
    {
        // TO FIX
        ColliderDistance2D Distance = gameObject.GetComponent<BoxCollider2D>().Distance(GameManager.Instance.Player.GetComponent<BoxCollider2D>());
        while (Distance.distance < AggroRange)
        {
            Vector3 target = GameManager.Instance.Player.transform.position;
            transform.LookAt(target);
            transform.Rotate(new Vector3(0, -90, 0), Space.Self);//correcting the original rotation
            transform.Translate(new Vector3(MoveSpeed * Time.deltaTime, 0, 0));
        }
    }

}
