using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField]
    private float Cooldown;

    [SerializeField]
    private bool RandomCooldown;
    [SerializeField]
    private Vector2 RCRangeInSeconds;

    [SerializeField]
    private GameObject EnemyToSpawn;

    [SerializeField]
    private int NumberOfEnemiesToSpawn;

    [SerializeField]
    private Vector2 RangeX, RangeY;

    [SerializeField]
    private bool _active;
    public bool Active { get { return _active; } set { _active = value; } }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        if (NumberOfEnemiesToSpawn > 0 && !Active)
        {
            StartCoroutine(Spawn());
        }
    }

    private IEnumerator Spawn()
    {
        Active = true;
        if (RandomCooldown) Cooldown = Random.Range(RCRangeInSeconds.x, RCRangeInSeconds.y);
        float PosX = Random.Range(RangeX.x, RangeX.y);
        float PosY = Random.Range(RangeY.x, RangeY.y);
        Instantiate(EnemyToSpawn, new Vector3(PosX, PosY, 0), Quaternion.identity);
        NumberOfEnemiesToSpawn -= 1;
        yield return new WaitForSeconds(Cooldown);
        Active = false;
    }

}
