using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    [SerializeField]
    float MoveSpeed;

    [SerializeField]
    private int DividingFactor;

    // Start is called before the first frame update
    void Start()
    {
        if (MoveSpeed != null) MoveSpeed /= DividingFactor;
        else MoveSpeed = 2 / DividingFactor;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 target = GameManager.Instance.Player.transform.position;
        transform.LookAt(target);
        transform.Rotate(new Vector3(0, -90, 0), Space.Self);//correcting the original rotation
        transform.Translate(new Vector3(MoveSpeed * Time.deltaTime, 0, 0));

    }

}
