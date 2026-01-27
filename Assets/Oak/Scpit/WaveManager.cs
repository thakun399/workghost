using UnityEngine;
using System.Collections;

[System.Serializable]
public class WaveData
{
    public int enemyCount;        // จำนวนศัตรูใน Wave นี้
    public float spawnInterval;   // เวลาห่างระหว่างเกิด
    public int bonusHealth;       // เพิ่ม HP ศัตรูใน Wave นี้
}

public class WaveManager : MonoBehaviour
{
    [Header("Enemy")]
    public GameObject ghostPrefab;
    public Transform[] spawnPoints;

    [Header("Wave Settings")]
    public WaveData[] waves;
    public int currentWave = 1;
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

        int waveIndex = currentWave - 1;
        if (waveIndex >= waves.Length)
        {
            Debug.Log("ALL WAVES CLEARED");
            yield break;
        }

        WaveData wave = waves[waveIndex];

        Debug.Log("START WAVE " + currentWave);

        for (int i = 0; i < wave.enemyCount; i++)
        {
            SpawnEnemy(wave);
            yield return new WaitForSeconds(wave.spawnInterval);
        }

        spawning = false;
    }

    void SpawnEnemy(WaveData wave)
    {
        Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject ghost = Instantiate(ghostPrefab, point.position, Quaternion.identity);

        enemiesAlive++;

        GhostHealth health = ghost.GetComponent<GhostHealth>();
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
        yield return new WaitForSeconds(timeBetweenWaves);
        currentWave++;
        StartCoroutine(StartWave());
    }
}