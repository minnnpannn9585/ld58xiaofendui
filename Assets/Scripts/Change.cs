using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    // 原始人物形象
    public Sprite originalSprite;

    // 按下E键时要显示的新形象
    public Sprite transformSprite;
    bool isLoaded = false;
    void Start()
    {
        // 获取SpriteRenderer组件
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 确保originalSprite有值，不然使用当前Sprite
        if (originalSprite == null)
        {
            originalSprite = spriteRenderer.sprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isLoaded = true;
          
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            isLoaded = false;
            
        }
    }
    

    void Update()
    {
        // 检测按 E 键
        if (isLoaded && Input.GetKeyDown(KeyCode.E))
        {
            if (transformSprite != null)
            {
                spriteRenderer.sprite = transformSprite;
            }
        }
        if(isLoaded && Input.GetKeyUp(KeyCode.E))
        {
            spriteRenderer.sprite = originalSprite;
        }
    }
}

