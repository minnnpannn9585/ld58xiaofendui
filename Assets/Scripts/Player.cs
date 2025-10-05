using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class Player : MonoBehaviour
{
    public float minX = -5f;
    public float maxX = 5f;
    public float minY = 0f;
    public float maxY = 5f;
    
    [HideInInspector]
    public bool canMove = true;

    float distance = 0;

    Transform targetObject;
    public float detectionRange = 5f;      // ����QTE
    
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
        
        float targetSpeed = moveInput * moveSpeed;
        
        if (moveInput != 0)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, moveSpeed / accelerationTime * Time.fixedDeltaTime);
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, moveSpeed / decelerationTime * Time.fixedDeltaTime);
        }
        
        rb.velocity = new Vector2(currentSpeed, rb.velocity.y);
    }
    void Update()
    {
        Vector3 pos = transform.position;

        // 限制物体在指定范围内
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
        
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
               if (npc.isRight)
               {
                   npc.isRightFinishTime++;
                   if(npc.isRightFinishTime == 2)
                       BookManager.instance.TurnObjectOn(npc.humanIndex);
               }
               else
               {
                   BookManager.instance.TurnObjectOn(npc.humanIndex);
               }
            }
            else
            {
                if (distance <= detectionRange && !npc.isQTEActive)
                {
                    npc.StartQTE();
                }
            }
        }
    }

    public void TakeDamage(int damage)
    {
        
        playerHealth.currentHealth -= damage;
        print(playerHealth.currentHealth);
        playerHealth.UpdateHearts();
        if (playerHealth.currentHealth <= 0)
        {
            Die();
        }
    }

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

