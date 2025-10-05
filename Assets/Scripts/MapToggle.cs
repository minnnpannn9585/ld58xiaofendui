using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapToggle : MonoBehaviour
{
    // 地图 UI Panel
    public GameObject mapPanel;

    // 玩家对象
    public Transform player;

    // 地图上表示玩家的位置的图标
    public RectTransform playerIcon;

    // 地图大小与实际场景的比例
    public Vector2 mapSize = new Vector2(200, 200); // 地图 UI 的宽高
    public Vector2 worldSize = new Vector2(50, 50);  // 场景实际大小

    void Start()
    {
        // 初始化时隐藏地图
        mapPanel.SetActive(false);
    }

    void Update()
    {
        // 按 M 键切换地图显示
        if (Input.GetKeyDown(KeyCode.M))
        {
            mapPanel.SetActive(!mapPanel.activeSelf);
        }

        // 如果地图显示，则更新玩家图标位置
        if (mapPanel.activeSelf)
        {
            UpdatePlayerIconPosition();
        }
    }

    void UpdatePlayerIconPosition()
    {
        // 将玩家世界坐标映射到地图坐标
        Vector2 mapPos = new Vector2(
            (player.position.x / worldSize.x) * mapSize.x,
            (player.position.y / worldSize.y) * mapSize.y
        );

        // 调整为地图中心为原点
        mapPos -= mapSize / 2;

        // 更新玩家图标位置
        playerIcon.anchoredPosition = mapPos;
    }
}
