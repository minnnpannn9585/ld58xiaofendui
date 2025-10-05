using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapToggle : MonoBehaviour
{
    // ��ͼ UI Panel
    public GameObject mapPanel;

    // ��Ҷ���
    public Transform player;

    // ��ͼ�ϱ�ʾ��ҵ�λ�õ�ͼ��
    public RectTransform playerIcon;

    // ��ͼ��С��ʵ�ʳ����ı���
    public Vector2 mapSize = new Vector2(200, 200); // ��ͼ UI �Ŀ��
    public Vector2 worldSize = new Vector2(50, 50);  // ����ʵ�ʴ�С

    void Start()
    {
        // ��ʼ��ʱ���ص�ͼ
        mapPanel.SetActive(false);
    }

    void Update()
    {
        // �� M ���л���ͼ��ʾ
        if (Input.GetKeyDown(KeyCode.M))
        {
            mapPanel.SetActive(!mapPanel.activeSelf);
        }

        // �����ͼ��ʾ����������ͼ��λ��
        if (mapPanel.activeSelf)
        {
            UpdatePlayerIconPosition();
        }
    }

    void UpdatePlayerIconPosition()
    {
        // �������������ӳ�䵽��ͼ����
        Vector2 mapPos = new Vector2(
            (player.position.x / worldSize.x) * mapSize.x,
            (player.position.y / worldSize.y) * mapSize.y
        );

        // ����Ϊ��ͼ����Ϊԭ��
        mapPos -= mapSize / 2;
        playerIcon.anchoredPosition = mapPos;
    }
}
