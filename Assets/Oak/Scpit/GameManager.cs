using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    void Update()
    {
        // กด R เพื่อ Restart (ทดสอบ)
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    public void RestartGame()
    {
        Debug.Log("========== RESTARTING GAME ==========");
        
        // รีเซ็ตเวลา
        Time.timeScale = 1f;
        
        // โหลด Scene ใหม่
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}