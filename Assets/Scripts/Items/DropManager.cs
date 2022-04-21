using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour
{
    public bool AttemptDrop(GameObject[] Drops)
    {
        foreach(GameObject Drop in Drops) {
            if (Drop.GetComponent<ItemData>().DropChance <= Random.Range(0, 100)) return true; else return false;
        }
        return false;
    }

    public void Drop(GameObject drop)
    {
        Instantiate(drop, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.identity);
    }
}
