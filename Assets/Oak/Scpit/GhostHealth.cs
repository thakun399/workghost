using UnityEngine;

public class GhostHealth : MonoBehaviour
{
    [Header("Health")]
    public int baseHealth = 1;
    private int currentHealth;

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
        WaveManager wave = FindObjectOfType<WaveManager>();
        if (wave != null)
        {
            wave.EnemyDied();
        }

        Destroy(gameObject);
    }
}