using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HumanBtn : MonoBehaviour
{
    private int index;
    Button button;
    public HumanCollection humanCollection;
    public GameObject humanCanvas;

    private void OnEnable()
    {
        button.onClick.AddListener(OnButtonClicked);
    }

    void Awake()
    {
        index = transform.GetSiblingIndex();
        button = GetComponent<Button>();
    }

    void OnButtonClicked()
    {
        humanCanvas.SetActive(true);
        // insert data into human ui
        humanCanvas.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = humanCollection.humans[index].humanImage;
        humanCanvas.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = humanCollection.humans[index].description;
        humanCanvas.transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>().text = humanCollection.humans[index].name;
    }
    
    private void OnDisable()
    {
        // 移除事件监听，防止内存泄漏
        if (button != null)
        {
            button.onClick.RemoveListener(OnButtonClicked);
        }
    }
}
