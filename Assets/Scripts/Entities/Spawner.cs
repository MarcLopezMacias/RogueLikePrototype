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
    public bool active = false;

    public bool doneSpawning;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.GetComponent<SpawnManager>().sceneSpawners.Add(gameObject);
        GetMovin();
    }

    void OnEnable()
    {
        GetMovin();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesSpawned < NumberOfEnemiesToSpawn && !active) StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        active = true;
        if (RandomCooldown) Cooldown = Random.Range(RCRangeInSeconds.x, RCRangeInSeconds.y);
        float PosX = Random.Range(RangeX.x, RangeX.y);
        float PosY = Random.Range(RangeY.x, RangeY.y);
        yield return new WaitForSeconds(Cooldown);
        Instantiate(GetRandomEnemy(), new Vector3(transform.position.x + PosX, transform.position.y + PosY, 0), Quaternion.identity);
        enemiesSpawned++;
        if (enemiesSpawned == NumberOfEnemiesToSpawn) doneSpawning = true;
        active = false;
    }

    private GameObject GetRandomEnemy()
    {
        return EnemiesToSpawn[Random.Range(0, EnemiesToSpawn.Length)];
    }

    public void Reset()
    {
        enemiesSpawned = 0;
        GetMovin();
    }

    private void GetMovin()
    {
        if (NumberOfEnemiesToSpawn > 0 && NumberOfEnemiesToSpawn < enemiesSpawned) StartCoroutine(Spawn());
    }


}

