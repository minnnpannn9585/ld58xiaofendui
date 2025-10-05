using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunctions : MonoBehaviour
{
    public Animator collectionAnimator;

    public void ChangeScene(string sceneName)
    {
        GameManager.ChangeScene(sceneName);
    }

    public void OpenCollection()
    {
        collectionAnimator.SetTrigger("SetOn");
    }

    public void CloseCollection()
    {
        collectionAnimator.SetTrigger("SetOff");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
