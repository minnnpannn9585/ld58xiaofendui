using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButtonManager : MonoBehaviour
{
    // Start is called before the first frame update
    bool clicked = false;
    public Animator[] animators;
    void Start()
    {
        
    }

    public void OnClick()
    {
        clicked = !clicked;
        if (clicked)
        {
            Open();
        }
        else
        {
            Close();
        }
    }

   public void Open()
    {
        foreach (var animator in animators)
        {
            animator.SetTrigger("SetOn");
        }
    }

    public void Close()
    {
        foreach (var animator in animators)
        {
            animator.SetTrigger("SetOff");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
