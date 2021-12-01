using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkingWalls : MonoBehaviour
{

    private float playerHeight;

    private Vector2 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        playerHeight = GameManager.Instance.Player.transform.position.y;
        gameObject.transform.position = new Vector2(originalPosition.x, playerHeight);
    }
}
