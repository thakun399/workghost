using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class WaveData
{
    [Header("Enemy Settings")]
    public GameObject[] enemyPrefabs;   
    public int enemyCount;             
    public float spawnInterval;         
    public int bonusHealth;             
}

public class WaveManager : MonoBehaviour
{
    [Header("Wave Settings")]
    public WaveData[] waves;
    public int currentWave = 1;
    public float timeBetweenWaves = 3f;

    [Header("Spawn Points")]
    public Transform[] spawnPoints;

    private List<Transform> availablePoints = new List<Transform>();
    private int enemiesAlive = 0;
    private bool spawning = false;

    void Start()
    {
        StartCoroutine(StartWave());
    }

    
    IEnumerator StartWave()
    {
        int waveIndex = currentWave - 1;

        if (waveIndex >= waves.Length)
        {
            Debug.Log("ALL WAVES CLEARED!");
            yield break;
        }

        WaveData wave = waves[waveIndex];

        Debug.Log("START WAVE " + currentWave);

        PrepareSpawnPoints();
        spawning = true;

        for (int i = 0; i < wave.enemyCount; i++)
        {
            SpawnEnemy(wave);
            yield return new WaitForSeconds(wave.spawnInterval);
        }

        spawning = false;
    }

    
    void PrepareSpawnPoints()
    {
        availablePoints.Clear();
        availablePoints.AddRange(spawnPoints);
    }

    
    void SpawnEnemy(WaveData wave)
    {
        if (availablePoints.Count == 0)
        {
            Debug.LogWarning("No available spawn points!");
            return;
        }

        
        Transform point =
            availablePoints[Random.Range(0, availablePoints.Count)];

        
        availablePoints.Remove(point);

        
        GameObject prefab =
            wave.enemyPrefabs[Random.Range(0, wave.enemyPrefabs.Length)];

        GameObject enemy =
            Instantiate(prefab, point.position, Quaternion.identity);

        enemiesAlive++;

       
        GhostHealth health = enemy.GetComponent<GhostHealth>();
        if (health != null)
        {
            health.SetBonusHealth(wave.bonusHealth);
        }
    }
    
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
        Debug.Log("WAVE CLEARED");
        yield return new WaitForSeconds(timeBetweenWaves);
        currentWave++;
        StartCoroutine(StartWave());
    }
}