using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 3;
    private int currentHealth;

    [Header("UI")]
    public Image[] heartImages; // ลาก Heart UI Image 3 อันเข้ามา
    public Sprite fullHeart;
    public Sprite emptyHeart;

    [Header("Game Over")]
    public GameObject gameOverPanel; // Panel แสดงเมื่อตาย

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
        
        UpdateHealthUI();
        
        Debug.Log("Player Hit! Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < currentHealth)
            {
                heartImages[i].sprite = fullHeart;
            }
            else
            {
                heartImages[i].sprite = emptyHeart;
            }
        }
    }

    void Die()
    {
        Debug.Log("Player Died!");
        
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // หยุดเวลา หรือ ปิดการควบคุม
        Time.timeScale = 0f;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}