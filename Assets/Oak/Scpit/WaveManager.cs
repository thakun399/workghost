using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    [Header("Enemy")]
    public GameObject ghostPrefab;
    public Transform[] spawnPoints;

    [Header("Wave Settings")]
    public int currentWave = 1;
    public int enemiesPerWave = 3;
    public float timeBetweenSpawns = 1f;
    public float timeBetweenWaves = 3f;

    private int enemiesAlive = 0;
    private bool spawning = false;

    void Start()
    {
        StartCoroutine(StartWave());
    }

    IEnumerator StartWave()
    {
        spawning = true;
        Debug.Log("Wave " + currentWave + " Start");

        int enemiesToSpawn = enemiesPerWave + (currentWave - 1) * 2;

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(timeBetweenSpawns);
        }

        spawning = false;
    }

    void SpawnEnemy()
    {
        Transform spawnPoint =
            spawnPoints[Random.Range(0, spawnPoints.Length)];

        Instantiate(ghostPrefab, spawnPoint.position, Quaternion.identity);
        enemiesAlive++;
    }

    // 🔥 สำคัญมาก
    public void EnemyDied()
    {
        enemiesAlive--;

        if (enemiesAlive <= 0 && !spawning)
        {
            StartCoroutine(NextWave());
        }
    }

    IEnumerator NextWave()
    {
        Debug.Log("Wave Clear!");
        yield return new WaitForSeconds(timeBetweenWaves);

        currentWave++;
        StartCoroutine(StartWave());
    }
}