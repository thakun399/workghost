using UnityEngine;

public class GhostHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 1;
    private int currentHealth;

    [Header("Death Effects")]
    public ParticleSystem deathEffect;
    public AudioClip deathSound;
    public int scoreValue = 100; // คะแนนที่ได้

    private AudioSource audioSource;

    void Start()
    {
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
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

        // ทำลายผี
        Destroy(gameObject);
    }
}