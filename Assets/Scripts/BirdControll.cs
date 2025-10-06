using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdControll : MonoBehaviour
{
    public float moveSpeed = 5f;

    // ��ɫ���
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    // �������
    private float moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // ��ȡˮƽ����
        moveInput = Input.GetAxisRaw("Horizontal"); // -1 �� (A), 1 �� (D), 0 ������

        // ��ת��ɫ
        if (moveInput > 0)
        {
            spriteRenderer.flipX = false; // ������
        }
        else if (moveInput < 0)
        {
            spriteRenderer.flipX = true; // ������
        }
    }

    void FixedUpdate()
    {
        // �ƶ���ɫ
        //rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }
}
