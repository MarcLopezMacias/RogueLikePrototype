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

    public void SetNewPosition(Transform location)
    {
        gameObject.transform.position = location.position;
    }
}
