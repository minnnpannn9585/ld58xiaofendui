using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    // ��ͼ UI �� Canvas
    public GameObject mapUI;
    // ���Ƕ���
    public Transform player;
    // �����ڵ�ͼ�ϵ�ͼ��
    public RectTransform playerIcon;
    // ��ͼ�ı������������� -> ��ͼ���꣩
    public Vector2 mapScale = new Vector2(0.1f, 0.1f);

    // ��ͼĬ�Ϲر�
    private bool isMapOpen = false;

    void Update()
    {
        // ��� M ������
        if (Input.GetKeyDown(KeyCode.M))
        {
            isMapOpen = !isMapOpen; // �л���ͼ״̬
            mapUI.SetActive(isMapOpen); // �򿪻�رյ�ͼ

            if (isMapOpen)
            {
                UpdatePlayerIconPosition(); // �������λ��
            }
        }

        // �����ͼ�ǿ���״̬�����ϸ�������λ��
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

