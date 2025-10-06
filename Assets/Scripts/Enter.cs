using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enter : MonoBehaviour
{
    private Animator anim;
    private Enter enter;
    

    public void SetAnimation()
    {
        anim.SetBool("isEnter", false);
    }
    public bool isEnter;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        enter = GetComponent<Enter>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isEnter = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            isEnter = false;
            anim.SetBool("isEnter", false);
        }
    }
    

    
}
