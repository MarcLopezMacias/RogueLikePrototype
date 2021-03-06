using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ICollectible collectible = collision.GetComponent<ICollectible>();
        if(collectible != null)
        {
            collectible.Collect();
            IncreaseScore();
        }
    }

    private void IncreaseScore()
    {
        GameManager.Instance.GetComponent<ScoreManager>().IncreaseScore(1);
    }
}
