using UnityEngine;

public class GhostHealth : MonoBehaviour
{
    [Header("Health")]
    public int baseHealth = 1;
    private int currentHealth;
    public int scoreValue = 100;

    void Start()
    {
        currentHealth = baseHealth;
    }

    public void SetBonusHealth(int bonus)
    {
        currentHealth = baseHealth + bonus;
        Debug.Log("Ghost HP = " + currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager != null)
        {
            scoreManager.AddScore(scoreValue);
        }
        
        WaveManager wave = FindObjectOfType<WaveManager>();
        if (wave != null)
        {
            wave.EnemyDied();
        }

        Destroy(gameObject);
    }

}