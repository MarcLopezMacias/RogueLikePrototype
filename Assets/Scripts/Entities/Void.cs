using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Void : MonoBehaviour
{

    private float playerHeight;
    private float previousHeight;

    private Vector2 originalPosition;

    [SerializeField]
    private int followOffset;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        playerHeight = GameManager.Instance.Player.transform.position.y;
        if(playerHeight > previousHeight)
        {
            gameObject.transform.position = new Vector2(originalPosition.x, playerHeight - followOffset);
        }
        previousHeight = playerHeight;
    }

private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Jumping"))
        {
            GameObject cl = collision.gameObject;
            cl.GetComponent<Player>().DecreaseLifes(1);
        }
    }
}
