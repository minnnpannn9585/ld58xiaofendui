using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdControll : MonoBehaviour
{
    public float moveSpeed = 5f;

    // 角色组件
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    // 输入变量
    private float moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 获取水平输入
        moveInput = Input.GetAxisRaw("Horizontal"); // -1 左 (A), 1 右 (D), 0 无输入

        // 翻转角色
        if (moveInput > 0)
        {
            spriteRenderer.flipX = false; // 面向右
        }
        else if (moveInput < 0)
        {
            spriteRenderer.flipX = true; // 面向左
        }
    }

    void FixedUpdate()
    {
        // 移动角色
        //rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }
}
