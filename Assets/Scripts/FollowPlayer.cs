using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // ��Ҷ���� Transform
    public Transform target;

    // �������ҵ�ƫ�����������ڱ༭���е���
    public Vector3 offset = new Vector3(0, 0, -10);

    // ��������ƽ��ϵ��
    public float smoothSpeed = 0.125f;

    void LateUpdate()
    {
        if (target != null)
        {
            // Ŀ��λ�� = ���λ�� + ƫ����
            Vector3 desiredPosition = target.position + offset;

            // ʹ�ò�ֵʵ��ƽ������
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // �������λ��
            transform.position = smoothedPosition;
        }
    }
}
