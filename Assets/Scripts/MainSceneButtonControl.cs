using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneButtonControl : MonoBehaviour
{
    public void StartGame()
    {
        // 替换 "GameScene" 为实际游戏关卡场景名称
        SceneManager.LoadScene("GameScene");
    }

    // 退出游戏
    public void QuitGame()
    {
        // 在编辑器中无效，仅在打包后有效
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
