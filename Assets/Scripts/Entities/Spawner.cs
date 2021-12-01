using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField]
    private int Cooldown = 5;

    [SerializeField]
    private GameObject EnemyToSpawn;

    [SerializeField]
    private Vector2 RangeX, RangeY;

    [SerializeField]
    private bool _active;
    public bool Active { get { return _active; } set { _active = value; } }

    /*
     * Spawner: objecte en escena que genera enemics.  Aquest objecte invoca 
     * enemics als marges de la pantalla.
     */

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            if(Active)
            {
                yield return new WaitForSeconds(Cooldown);
                int PosX = Random.Range(5, 10);
                int PosY = Random.Range(5, 10);
                Instantiate(EnemyToSpawn, new Vector3(PosX, PosY, 0), Quaternion.identity);
            }
            else
            {
                yield return new WaitForSeconds(1);
            }
        }
    }
}
