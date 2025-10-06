using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        BookManager.instance.audioManager.Play(0, "themeBGM", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
