using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    // 地图 UI 的 Canvas
    public GameObject mapUI;
    // 主角对象
    public Transform player;
    // 主角在地图上的图标
    public RectTransform playerIcon;
    // 地图的比例（世界坐标 -> 地图坐标）
    public Vector2 mapScale = new Vector2(0.1f, 0.1f);

    // 地图默认关闭
    private bool isMapOpen = false;

    void Update()
    {
        // 检测 M 键按下
        if (Input.GetKeyDown(KeyCode.M))
        {
            isMapOpen = !isMapOpen; // 切换地图状态
            mapUI.SetActive(isMapOpen); // 打开或关闭地图

            if (isMapOpen)
            {
                UpdatePlayerIconPosition(); // 更新玩家位置
            }
        }

        // 如果地图是开启状态，不断更新主角位置
        if (isMapOpen)
        {
            UpdatePlayerIconPosition();
        }
        void UpdatePlayerIconPosition()
        {
            if (player != null && playerIcon != null)
            {
                Vector2 mapPos = new Vector2(player.position.x * mapScale.x, player.position.y * mapScale.y);
                playerIcon.anchoredPosition = mapPos;
            }
        }

    }
}

