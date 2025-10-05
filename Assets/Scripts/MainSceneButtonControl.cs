using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneButtonControl : MonoBehaviour
{
    public void StartGame()
    {
        // �滻 "GameScene" Ϊʵ����Ϸ�ؿ���������
        SceneManager.LoadScene("GameScene");
    }

    // �˳���Ϸ
    public void QuitGame()
    {
        // �ڱ༭������Ч�����ڴ������Ч
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
