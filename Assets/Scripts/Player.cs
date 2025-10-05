using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 



public class Player : MonoBehaviour
{
    
    public Transform targetObject;         // 检测的对象
    public float detectionRange = 5f;      // 触发QTE的范围
    public GameObject qteUI;               // QTE的UI界面
    public Image pointerImage;             // QTE指针
    public Image targetSegment;            // 正确的区段

    private bool isQTEActive = false;
    public float pointerSpeed = 0.5f;      // 指针移动速度
    public bool increasePointer = true;    // 指针增减状态
    public GameObject heartPrefab;       // 预制心形
    public Sprite heartFull;             // 满血心
    public Sprite heartEmpty;            // 空血心
    public Transform heartContainer;     // 心形父对象
    private Image[] hearts;



    public int maxHealth = 5;
    
    public int currentHealth ;

    public Slider healthBar;

    public float moveSpeed = 5f;
    public float accelerationTime = 0.5f;
    public float decelerationTime = 0.5f;
    private float currentSpeed = 0f;
    private float moveInput = 0f;

    private Rigidbody2D rb;

    bool inRange;
    NPCController npc;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        hearts = new Image[maxHealth];

        // 创建心形UI
        for (int i = 0; i < maxHealth; i++)
        {
            GameObject heart = Instantiate(heartPrefab, heartContainer);
            Image heartImage = heart.GetComponent<Image>();
            heartImage.sprite = heartFull;
            hearts[i] = heartImage;
        }


        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }
    void FixedUpdate()
    {
        // 计算目标速度
        float targetSpeed = moveInput * moveSpeed;

        // 根据是否有输入选择加速或减速
        if (moveInput != 0)
        {
            // 平滑加速
            currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, moveSpeed / accelerationTime * Time.fixedDeltaTime);
        }
        else
        {
            // 平滑减速
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, moveSpeed / decelerationTime * Time.fixedDeltaTime);
        }

        // 设置 Rigidbody2D 的速度
        rb.velocity = new Vector2(currentSpeed, rb.velocity.y);
    }
    void Update()
    {
        float distance = Vector3.Distance(transform.position, targetObject.position);

        float moveX = Input.GetAxis("Horizontal"); 
        float moveY = Input.GetAxis("Vertical");   

        
        Vector3 move = new Vector3(moveX, moveY, 0f);

        
        transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);

        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            if (npc.isGood)
            {
               Heal(1);

            }
            else
            {
                if (distance <= detectionRange && !isQTEActive)
                {
                    StartQTE();
                }

                
            }
            //else
            //{
            //    TakeDamage(10);
            //}



        }

        if (isQTEActive == true)
        {
            HandleQTEInput();
        }
    }

    void StartQTE()
    {
        isQTEActive = true;
        qteUI.SetActive(true);
        pointerImage.fillAmount = 0f;
    }

    void HandleQTEInput()
    {
        // 简单QTE逻辑：按空格停止指针
        //pointerImage.fillAmount += Time.deltaTime; // 指针自动增长
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
            pointerImage.fillAmount = 1f; // 最大值
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsCorrectSegment(pointerImage.fillAmount))
            {
                // 成功逻辑
                Debug.Log("QTE Success!");
                //Destroy(targetObject.gameObject); // 物体损毁
                EndQTE();
            }
            else
            {
                // 失败逻辑
                Debug.Log("QTE Fail! Player -1");
                // 扣除玩家1点生命或分数，需要实现Player属性
                Player playerHealth = GetComponent<Player>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(1);
                }
                EndQTE();
            }
        }
    }

    bool IsCorrectSegment(float pointerValue)
    {
        // 判断指针是否在正确范围
        float min = targetSegment.fillAmount - 0.1f;
        float max = targetSegment.fillAmount + 0.1f;
        return pointerValue >= min && pointerValue <= max;
    }

    void EndQTE()
    {
        isQTEActive = false;
        qteUI.SetActive(false);
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

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;
        UpdateHearts();
    }

    // 回复血量方法
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        UpdateHearts();
    }


    void Die()
    {
        Debug.Log(gameObject.name + " 死亡");
        
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            inRange = true;
            npc = collision.GetComponent<NPCController>();
            print(npc.name);
            print(npc.isGood);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            inRange = false;
            npc = null;
             
            //print(npc.name);
        }
    }
}

