using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{
    [SerializeField]
    GameObject enemyPrefab;

    float timeSinceLastSpawn = 0;

    [SerializeField]
    float timeBetweenSpawns = 0.5f;

    void Update()
    {
        timeSinceLastSpawn = timeSinceLastSpawn + Time.deltaTime;
        if (timeSinceLastSpawn > timeBetweenSpawns)
        {
            Instantiate(enemyPrefab);
            timeSinceLastSpawn = 0;
        }
    }
}
