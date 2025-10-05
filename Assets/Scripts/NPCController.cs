using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCController : MonoBehaviour
{
    public Player player;
    public bool isGood;
    public int humanIndex;
    public bool isRight;
    public int isRightFinishTime;
    public float toggleInterval = 10f;
    
    [Header("qte")]
    public GameObject qteUI;               // QTE��UI
    public Image pointerImage;             // QTEָ
    public Image targetSegment;

    [HideInInspector]
    public bool isQTEActive = false;
    public float pointerSpeed = 0.5f;
    public bool increasePointer = true;

    void Start()
    {
        player = FindObjectOfType<Player>();
        if(isRight)
            StartCoroutine(ToggleIsGoodCoroutine());
    }

    private void Update()
    {
        if (isQTEActive == true)
        {
            HandleQTEInput();
        }
    }
    
    public void StartQTE()
    {
        //print(1111);
        isQTEActive = true;
        qteUI.SetActive(true);
        pointerImage.fillAmount = 0f;
        player.canMove = false;
    }
    
    void HandleQTEInput()
    {
        if (increasePointer)
        {
            pointerImage.fillAmount += pointerSpeed * Time.deltaTime;
            if (pointerImage.fillAmount >= 1f)
            {
                pointerImage.fillAmount = 1f;
                increasePointer = false;
            }
        }
        else
        {
            pointerImage.fillAmount -= pointerSpeed * Time.deltaTime;
            if (pointerImage.fillAmount <= 0f)
            {
                pointerImage.fillAmount = 0f;
                increasePointer = true;
            }
        }
        if (pointerImage.fillAmount >= 1f)
        {
            pointerImage.fillAmount = 1f; 
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.canMove = true;
            if (IsCorrectSegment(pointerImage.fillAmount))
            {
                if (isRight)
                {
                    isRightFinishTime++;
                    if(isRightFinishTime == 2)
                        BookManager.instance.TurnObjectOn(humanIndex);
                }
                else
                {
                    BookManager.instance.TurnObjectOn(humanIndex);
                }

                Debug.Log("QTE Success!");
                EndQTE();
            }
            else
            {
                Debug.Log("QTE Fail! Player -1");
                player.TakeDamage(1);
                EndQTE();
            }
        }
    }
    
    bool IsCorrectSegment(float pointerValue)
    {
        float min = targetSegment.fillAmount - 0.1f;
        float max = targetSegment.fillAmount + 0.1f;
        return pointerValue >= min && pointerValue <= max;
    }

    void EndQTE()
    {
        isQTEActive = false;
        qteUI.SetActive(false);
    }

    IEnumerator ToggleIsGoodCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(toggleInterval);
            isGood = !isGood;
        }
    }
}
