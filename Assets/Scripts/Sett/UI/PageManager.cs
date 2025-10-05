using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PageManager : MonoBehaviour
{
    public int totalPage;
    public int currentPage = 1;

    public Image[] images;
    public Image defaultImage;

    public TextMeshProUGUI pageText;

    // Start is called before the first frame update
    void Start()
    {
        UpdateTexts();
        ReloadHumans(1);
    }

    void ReloadHumans(int page)
    {
        for (int i = page * 12 - 12; i <= page * 12 - 1; i++)
        {
            GameObject gameObject = FindObjectByName($"HumanPlaceholder ({i - page * 12 + 12})");
            
            if (gameObject == null)
            {
                Debug.LogError("GameObject NOt found!");
            }
            
            Image imageComponent = gameObject.GetComponent<Image>();
            if (imageComponent == null) continue;
            
            // 检查 images 数组是否为空或超出范围
            if (images == null || i >= images.Length || i < 0)
            {
                // 检查 defaultImage 是否为空
                if (defaultImage != null)
                {
                    imageComponent.sprite = defaultImage.sprite;
                    imageComponent.color = defaultImage.color;
                }
            }
            else if (images[i] != null)
            {
                imageComponent.sprite = images[i].sprite;
                imageComponent.color = images[i].color;
                
                HumanExtractor humanExtractor = gameObject.GetComponent<HumanExtractor>();
                if (humanExtractor != null)
                {
                    humanExtractor.id = i;
                    humanExtractor.UpdateImageWithId(i);
                }
            }
            else
            {
                // 如果 images[i] 为空，使用默认图片
                if (defaultImage != null)
                {
                    imageComponent.sprite = defaultImage.sprite;
                    imageComponent.color = defaultImage.color;
                }
            }
        }
        //For test only
        Debug.Log("Updatetext");
        UpdateTexts();
    }

    void UpdateTexts()
    {
        if (pageText != null)
        {
            pageText.text = $"{currentPage}/{totalPage}";
        }
        //TODO: find all HumanExtractor (n) in the scene. n ranging from 0 to 11.
        //set the id of the HumanExtractor to n.
        //set the text of the HumanExtractor to n.
        for (int i = 0; i < 12; i++)
        {
            GameObject gameObject = FindObjectByName($"CollectionCanvas/UIMover/Background/HumanPlaceholder ({i})");
            if (gameObject != null)
            {
                HumanExtractor humanExtractor = gameObject.GetComponent<HumanExtractor>();
                if (humanExtractor != null)
                {
                    humanExtractor.id = i-12+12*currentPage;
                    humanExtractor.UpdateImageWithId(humanExtractor.id);
                }
            }
        }
    }

    public void PageChange(bool increase)
    {
        //Debug.Log("Changing page");
        if (increase)
        {
            if (currentPage < totalPage)
            {
                //Debug.Log("change++");
                currentPage++;
                UpdateTexts();
                ReloadHumans(currentPage);
            }
        }
        else
        {
            if (currentPage > 1)
            {
                currentPage--;
                UpdateTexts();
                ReloadHumans(currentPage);
            }
        }
    }

    private GameObject FindObjectByName(string name)
    {
        // 如果包含路径分隔符，使用 Transform.Find 方法
        if (name.Contains("/"))
        {
            // 查找根对象（路径的第一部分）
            string[] pathParts = name.Split('/');
            GameObject rootObject = GameObject.Find(pathParts[0]);
            
            if (rootObject != null)
            {
                // 沿着路径查找子对象
                Transform current = rootObject.transform;
                for (int i = 1; i < pathParts.Length; i++)
                {
                    current = current.Find(pathParts[i]);
                    if (current == null) return null;
                }
                return current.gameObject;
            }
        }
        else
        {
            // 直接按名称查找
            GameObject[] allObjects = FindObjectsOfType<GameObject>();
            foreach (GameObject obj in allObjects)
            {
                if (obj.name == name)
                {
                    return obj;
                }
            }
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
