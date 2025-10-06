using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    // ԭʼ��������
    public Sprite originalSprite;

    // ����E��ʱҪ��ʾ��������
    public Sprite transformSprite;
    bool isLoaded = false;
    void Start()
    {
        // ��ȡSpriteRenderer���
        spriteRenderer = GetComponent<SpriteRenderer>();

        // ȷ��originalSprite��ֵ����Ȼʹ�õ�ǰSprite
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
        // ��ⰴ E ��
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

