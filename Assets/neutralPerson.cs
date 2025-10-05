using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class neutralPerson : MonoBehaviour
{
    // 定义一个public布尔值，可以在Unity编辑器中看到并控制
    public bool isRight = true;

    // 定义切换间隔时间，单位为秒
    public float toggleInterval = 10f;

    void Start()
    {
        // 启动协程，每隔指定时间切换isGood状态
        StartCoroutine(ToggleIsGoodCoroutine());
    }

    // 协程方法，每 toggleInterval 秒切换一次isGood
    IEnumerator ToggleIsGoodCoroutine()
    {
        while (true)
        {
            // 等待指定时间
            yield return new WaitForSeconds(toggleInterval);

            // 切换布尔值
            isRight = !isRight;

            // 输出当前状态到控制台供调试
            
        }
    }
}
