using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class neutralPerson : MonoBehaviour
{
    // ����һ��public����ֵ��������Unity�༭���п���������
    public bool isRight = true;

    // �����л����ʱ�䣬��λΪ��
    public float toggleInterval = 10f;

    void Start()
    {
        // ����Э�̣�ÿ��ָ��ʱ���л�isGood״̬
        StartCoroutine(ToggleIsGoodCoroutine());
    }

    // Э�̷�����ÿ toggleInterval ���л�һ��isGood
    IEnumerator ToggleIsGoodCoroutine()
    {
        while (true)
        {
            // �ȴ�ָ��ʱ��
            yield return new WaitForSeconds(toggleInterval);

            // �л�����ֵ
            isRight = !isRight;

            // �����ǰ״̬������̨������
            
        }
    }
}
