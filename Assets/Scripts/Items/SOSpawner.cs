using UnityEngine;

public class SOSpawner : MonoBehaviour
{
    // The GameObject to instantiate.
    [SerializeField]
    public GameObject entityToSpawn;

    // An instance of the ScriptableObject defined above.
    public Item ScriptableObjectInstance;

    // This will be appended to the name of the created entities and increment when each is created.
    int instanceNumber = 1;

    void Start()
    {
        SpawnEntities();
    }

    void SpawnEntities()
    {
        int currentSpawnPointIndex = 0;

        // for (int i = 0; i < ScriptableObjectInstance.numberOfPrefabsToCreate; i++)
        // {
            // Creates an instance of the prefab at the current spawn point.
            GameObject currentEntity = Instantiate(entityToSpawn, gameObject.transform.position, Quaternion.identity);

            // Sets the name of the instantiated entity to be the string defined in the ScriptableObject and then appends it with a unique number. 
            currentEntity.name = ScriptableObjectInstance.Name + instanceNumber;

            // Moves to the next spawn point index. If it goes out of range, it wraps back to the start.
            // currentSpawnPointIndex = (currentSpawnPointIndex + 1) % ScriptableObjectInstance.spawnPoints.Length;

            instanceNumber++;
        // }
    }
}