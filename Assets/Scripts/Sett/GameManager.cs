using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject audioManagerPrefab;
    public Animator cutSceneAnimator;
    public static Animator cutSceneAnimatorStatic;
    private static GameManager instance;
    public AudioManager audioManager;
    
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(audioManager);
            audioManager = Instantiate(audioManagerPrefab).GetComponent<AudioManager>();
        }
        else
        {
            Destroy(gameObject);
        }
        cutSceneAnimatorStatic = cutSceneAnimator;
    }
    void Start()
    {
        if (!instance) {
            instance = this;
           
        }
    }

    public static void ChangeScene(string sceneName, Animator animator=null)
    {
        if (animator == null)
        {
            animator = cutSceneAnimatorStatic;
        }
        //delay 0.5s
        if (instance != null)
        {
            instance.StartCoroutine(LoadScene(sceneName));
        }
        if (animator != null)
        {
            animator.SetTrigger("ChangeScene");
        }
        //SceneManager.LoadScene(sceneName);
    }

    static IEnumerator LoadScene(string sceneName)
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
