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
    public GameObject[] EnemiesToSpawn;

    [SerializeField]
    public int NumberOfEnemiesToSpawn;
    public int enemiesSpawned;

    [SerializeField]
    private Vector2 RangeX, RangeY;

    [SerializeField]
    private bool _active;
    public bool Active { get { return _active; } set { _active = value; } }

    // Start is called before the first frame update
    void Start()
    {
        if (NumberOfEnemiesToSpawn > 0) StartCoroutine(Spawn());
    }

    void OnEnable()
    {
        Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesSpawned < NumberOfEnemiesToSpawn && !Active)
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
        Instantiate(GetRandomEnemy(), new Vector3(transform.position.x + PosX, transform.position.y + PosY, 0), Quaternion.identity);
        enemiesSpawned++;
        yield return new WaitForSeconds(Cooldown);
        Active = false;
    }

    private GameObject GetRandomEnemy()
    {
        return EnemiesToSpawn[Random.Range(0, EnemiesToSpawn.Length)];
    }

    public void Reset()
    {
        Start();
    }

}
