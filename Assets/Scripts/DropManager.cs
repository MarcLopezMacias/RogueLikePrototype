using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool AttemptDrop(GameObject[] Drops)
    {
        foreach(GameObject Drop in Drops) {
            if (Drop.GetComponent<Item>().GetDropChance() <= Random.Range(0, 100)) return true; else return false;
        }
        return false;
    }

    public void Drop(GameObject drop)
    {
        Instantiate(drop, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.identity);
    }
}
