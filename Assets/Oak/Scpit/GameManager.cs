using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject startPanel;

    void Awake()
    {
        instance = this;

        // เปิดเกมมา หยุดเกมก่อน
        Time.timeScale = 1;
        startPanel.SetActive(true);
    }

    // ปุ่ม Start
    public void StartGame()
    {
        startPanel.SetActive(false);
        Time.timeScale = 1;
    }

    // ปุ่ม Restart
    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // เรียกตอนแพ้ (ถ้ามี)
    public void GameOver()
    {
        startPanel.SetActive(true);
        Time.timeScale = 1;
    }
}