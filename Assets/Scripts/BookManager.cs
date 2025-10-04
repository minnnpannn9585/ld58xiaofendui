using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookManager : MonoBehaviour
{
    public static BookManager instance;
    public GameObject[] icons;
    public int score = 0;
    public GameObject winUI;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TurnObjectOn(int index)
    {
        if (!icons[index].activeInHierarchy)
        {
            icons[index].SetActive(true);
            score++;
            CheckWin();
        }
    }

    public void CheckWin()
    {
        if (score == icons.Length - 3)
            winUI.SetActive(true);
    }
}
