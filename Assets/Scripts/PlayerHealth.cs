using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 


public class PlayerHealth : MonoBehaviour
{
    public Sprite heartFull;             // ��Ѫ��
    public Sprite heartEmpty;            // ��Ѫ��
    public Transform heartContainer;     // ���θ�����
    private Image[] hearts;
    
    public int maxHealth = 5;
    public int currentHealth ;
    //public Slider healthBar;
    public GameObject heartPrefab;
    
    void Start()
    {
        currentHealth = maxHealth;
        hearts = new Image[maxHealth];
        
        for (int i = 0; i < maxHealth; i++)
        {
            GameObject heart = Instantiate(heartPrefab, heartContainer);
            Image heartImage = heart.GetComponent<Image>();
            heartImage.sprite = heartFull;
            hearts[i] = heartImage;
        }
    }
    
    public void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
                hearts[i].sprite = heartFull;
            else
                hearts[i].sprite = heartEmpty;
        }
    }
}
