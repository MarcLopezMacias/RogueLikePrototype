using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    [SerializeField]
    float MoveSpeed;

    [SerializeField]
    private int factor = 10;

    private float playerX;

    // Start is called before the first frame update
    void Start()
    {
        if (MoveSpeed != null) MoveSpeed /= factor;
        else MoveSpeed = 2 / factor;
    }

    // Update is called once per frame
    void Update()
    {
        playerX = GameManager.Instance.Player.transform.position.x;
        if(playerX > gameObject.transform.position.x)
        {
            transform.Translate(new Vector3(MoveSpeed * Time.deltaTime, 0f, 0f));
        }
        else
        {
            transform.Translate(new Vector3((-1) * MoveSpeed * Time.deltaTime, 0f, 0f));
        }
    }

}
