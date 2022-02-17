using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    Vector3 initialPosition;

    Vector3 newPosition;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float PlayerX = GameManager.Instance.Player.transform.position.x;
        float PlayerY = GameManager.Instance.Player.transform.position.y;

        newPosition = new Vector3(PlayerX, PlayerY, initialPosition.z);

        gameObject.transform.position = newPosition;
    }

    public void ResetPosition()
    {
        gameObject.transform.position = initialPosition;
    }

    public void SetNewInitialPosition(float x, float y, float z)
    {
        initialPosition = new Vector3(x, y, z);
    }

    public void SetNewInitialPosition(Vector3 newInitialPosition)
    {
        initialPosition = newInitialPosition;
    }
}
