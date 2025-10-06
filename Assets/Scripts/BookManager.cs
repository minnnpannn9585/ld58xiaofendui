using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookManager : MonoBehaviour
{
    public static BookManager instance;
    public GameObject audioManagerPrefab;
    public GameObject[] icons;
    public int score = 0;
    public GameObject winUI;
    public AudioManager audioManager;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(audioManager);
            audioManager = Instantiate(audioManagerPrefab).GetComponent<AudioManager>();
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
            print(index);
            icons[index].SetActive(true);
            score++;
            CheckWin();
            
        }
    }

    public void CheckWin()
    {
        if (score == icons.Length)
            winUI.SetActive(true);
    }
}
