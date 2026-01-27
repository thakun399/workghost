using UnityEngine;

public class GhostHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int baseHealth = 1;  
    public int healthPerWave = 1;
    private int currentHealth;

    [Header("Death Effects")]
    public ParticleSystem deathEffect;
    public AudioClip deathSound;
    public int scoreValue = 100; // คะแนนที่ได้

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        WaveManager wave = FindObjectOfType<WaveManager>();
        int w = wave != null ? wave.currentWave : 1;

        currentHealth = baseHealth + (w - 1) * healthPerWave;

        Debug.Log("Ghost Spawn | Wave " + w + " | HP = " + currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " took damage! Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " died!");

        // เพิ่มคะแนน
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager != null)
        {
            scoreManager.AddScore(scoreValue);
        }

        // Effect
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        // Sound
        if (deathSound != null && audioSource != null)
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
        }
        
        WaveManager waveManager = FindObjectOfType<WaveManager>();
        if (waveManager != null)
        {
            waveManager.EnemyDied();
        }

        Destroy(gameObject);
    }
}