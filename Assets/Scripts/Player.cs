using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 



public class Player : MonoBehaviour
{
    
    public Transform targetObject;         // ���Ķ���
    public float detectionRange = 5f;      // ����QTE�ķ�Χ
    public GameObject qteUI;               // QTE��UI����
    public Image pointerImage;             // QTEָ��
    public Image targetSegment;            // ��ȷ������

    private bool isQTEActive = false;
    public float pointerSpeed = 0.5f;      // ָ���ƶ��ٶ�
    public bool increasePointer = true;    // ָ������״̬
    public GameObject heartPrefab;       // Ԥ������
    public Sprite heartFull;             // ��Ѫ��
    public Sprite heartEmpty;            // ��Ѫ��
    public Transform heartContainer;     // ���θ�����
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

        // ��������UI
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
        // ����Ŀ���ٶ�
        float targetSpeed = moveInput * moveSpeed;

        // �����Ƿ�������ѡ����ٻ����
        if (moveInput != 0)
        {
            // ƽ������
            currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, moveSpeed / accelerationTime * Time.fixedDeltaTime);
        }
        else
        {
            // ƽ������
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, moveSpeed / decelerationTime * Time.fixedDeltaTime);
        }

        // ���� Rigidbody2D ���ٶ�
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
        // ��QTE�߼������ո�ָֹͣ��
        //pointerImage.fillAmount += Time.deltaTime; // ָ���Զ�����
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
            pointerImage.fillAmount = 1f; // ���ֵ
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsCorrectSegment(pointerImage.fillAmount))
            {
                // �ɹ��߼�
                Debug.Log("QTE Success!");
                //Destroy(targetObject.gameObject); // �������
                EndQTE();
            }
            else
            {
                // ʧ���߼�
                Debug.Log("QTE Fail! Player -1");
                // �۳����1���������������Ҫʵ��Player����
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
        // �ж�ָ���Ƿ�����ȷ��Χ
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

    // �ظ�Ѫ������
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        UpdateHearts();
    }


    void Die()
    {
        Debug.Log(gameObject.name + " ����");
        
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

