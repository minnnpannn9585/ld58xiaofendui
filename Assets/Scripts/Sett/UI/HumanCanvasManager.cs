using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UI;

public class HumanCanvasManager : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI infoText;
    public Animator animator;
    public PageManager pageManager;
    public Collider2D collider2D;
    bool isOn;
    public void UpdateWithInfo(int id)
    {
        //Find info/image in future data space. TODO
    }

    public void Open(int entry)
    {
        Debug.Log("Opening!");
        animator.SetTrigger("SetOn");
        UpdateWithInfo(entry+pageManager.currentPage*12-12);
        isOn = true;
    }

    public void Close()
    {
        animator.SetTrigger("SetOff");
        isOn = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        collider2D = GetComponent<Collider2D>();
        
        // 自动查找 PageManager
        GameObject pageManagerObject = FindObjectByName("CollectionCanvas/UIMover/PageManager");
        if (pageManagerObject != null)
        {
            pageManager = pageManagerObject.GetComponent<PageManager>();
            if (pageManager == null)
            {
                Debug.LogError("PageManager component not found on the GameObject!");
            }
        }
        else
        {
            Debug.LogError("PageManager GameObject not found at path: CollectionCanvas/UIMover/PageManager");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 检测鼠标点击
        if (Input.GetMouseButtonDown(0) && isOn)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            // 检查点击是否在 collider2D 上
            if (collider2D != null && !collider2D.OverlapPoint(mousePosition))
            {
                // 如果点击不在 collider2D 上，触发 Close()
                Close();
            }
        }
        
        // 检测触摸点击（移动设备）
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            
            // 检查触摸是否在 collider2D 上
            if (collider2D != null && !collider2D.OverlapPoint(touchPosition))
            {
                // 如果触摸不在 collider2D 上，触发 Close()
                Close();
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
}
