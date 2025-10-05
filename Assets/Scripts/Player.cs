using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 



public class Player : MonoBehaviour
{
    bool canMove = true;

    float distance = 0;


    Transform targetObject;         // ���Ķ���
    public float detectionRange = 5f;      // ����QTE�ķ�Χ
    public GameObject qteUI;               // QTE��UI����
    public Image pointerImage;             // QTEָ��
    public Image targetSegment;            // ��ȷ������

    private bool isQTEActive = false;
    public float pointerSpeed = 0.5f;      // ָ���ƶ��ٶ�
    public bool increasePointer = true;    // ָ������״̬
    public GameObject deathPanel;


    [HideInInspector]
    public PlayerHealth playerHealth;

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
        playerHealth = GetComponent<PlayerHealth>();
        rb = GetComponent<Rigidbody2D>();
        if (deathPanel!=null)
            deathPanel.SetActive(false);
    }
    void FixedUpdate()
    {
        if (!canMove) return;   
        
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
        if (isQTEActive == true)
        {
            HandleQTEInput();
        }
        if (!canMove) return;   
        
        if(targetObject != null) 
            distance = Vector3.Distance(transform.position, targetObject.position);

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
            

        }

        
    }

    void StartQTE()
    {
        print(1111);
        isQTEActive = true;
        qteUI.SetActive(true);
        pointerImage.fillAmount = 0f;
        canMove = false;
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
            canMove = true;
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
    

    public void TakeDamage(int damage)
    {
        playerHealth.currentHealth -= damage;
        if (playerHealth.currentHealth < 0) playerHealth.currentHealth = 0;
        playerHealth.UpdateHearts();
        if (playerHealth.currentHealth <= 0)
        {
            Die();
        }
    }

    // �ظ�Ѫ������
    public void Heal(int amount)
    {
        playerHealth.currentHealth += amount;
        if (playerHealth.currentHealth > playerHealth.maxHealth) playerHealth.currentHealth = playerHealth.maxHealth;
        playerHealth.UpdateHearts();
    }


    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            inRange = true;
            npc = collision.GetComponent<NPCController>();
            targetObject = collision.transform;
            print(npc.name);
            print(npc.isGood);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            targetObject = null;
            inRange = false;
            npc = null;
            //print(npc.name);
        }
    }
    void Die()
    {
        playerHealth.currentHealth = 0;

        // 显示死亡界面
        deathPanel.SetActive(true);
        // 可选择禁用玩家控制脚本
        GetComponent<Player>().enabled = false;
    }
    public void RestartGame()
    {
        // 重新加载当前场景
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

}

