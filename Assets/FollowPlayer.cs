using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // 玩家对象的 Transform
    public Transform target;

    // 相机与玩家的偏移量，可以在编辑器中调整
    public Vector3 offset = new Vector3(0, 0, -10);

    // 相机跟随的平滑系数
    public float smoothSpeed = 0.125f;

    void LateUpdate()
    {
        if (target != null)
        {
            // 目标位置 = 玩家位置 + 偏移量
            Vector3 desiredPosition = target.position + offset;

            // 使用插值实现平滑跟随
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // 设置相机位置
            transform.position = smoothedPosition;
        }
    }
}
