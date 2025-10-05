using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;

    // 游戏是否暂停
    private bool isPaused = false;

    void Update()
    {
        // 检测按键（例如 ESC 键）
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    // 暂停游戏
    public void PauseGame()
    {
        pauseMenu.SetActive(true);   // 显示暂停菜单
        Time.timeScale = 0f;         // 暂停游戏时间
        isPaused = true;
    }

    // 恢复游戏
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);  // 隐藏暂停菜单
        Time.timeScale = 1f;         // 恢复游戏时间
        isPaused = false;
    }

    // 退出游戏或回到主菜单
    public void QuitGame()
    {
        Time.timeScale = 1f;
        // 根据需要选择退出或回到菜单场景
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }
    public void RestartGame()
    {
        Time.timeScale = 1f; // 确保时间恢复
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name); // 重新加载当前场景
    }
}
