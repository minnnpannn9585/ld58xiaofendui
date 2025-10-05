using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;

    // ��Ϸ�Ƿ���ͣ
    private bool isPaused = false;

    void Update()
    {
        // ��ⰴ�������� ESC ����
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    // ��ͣ��Ϸ
    public void PauseGame()
    {
        pauseMenu.SetActive(true);   // ��ʾ��ͣ�˵�
        Time.timeScale = 0f;         // ��ͣ��Ϸʱ��
        isPaused = true;
    }

    // �ָ���Ϸ
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);  // ������ͣ�˵�
        Time.timeScale = 1f;         // �ָ���Ϸʱ��
        isPaused = false;
    }

    // �˳���Ϸ��ص����˵�
    public void QuitGame()
    {
        Time.timeScale = 1f;
        // ������Ҫѡ���˳���ص��˵�����
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }
    public void RestartGame()
    {
        Time.timeScale = 1f; // ȷ��ʱ��ָ�
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name); // ���¼��ص�ǰ����
    }
}
