using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip themeBGM;
    public AudioClip birdMove;
    public AudioClip birdSmile;
    public AudioClip birdHurt;
    public AudioClip birdEat;
    public AudioClip humanQuestion;
    public AudioClip humanAngry;
    public AudioClip humanKind;
    public AudioClip successCollect;
    public AudioClip Loud;
    public AudioClip enterNewMap;
    List<AudioSource> audios = new List<AudioSource>();


    private void Awake()
    {
        for (int i = 0; i < 12; i++)
        {
            var audio = this.gameObject.AddComponent<AudioSource>();
            audios.Add(audio);
        }
    }
    private void Start()
    {
        
    }
    public void Play(int index, string name, bool isLoop)
    {
        var clip = GetAudioClip(name);
        if(clip != null)
        {
            var audio = audios[index];
            audios[index].clip = clip;
            audios[index].loop = isLoop;
            audio.Play();
        }
    }
    AudioClip GetAudioClip(string name)
    {
        switch (name)
        {
            case "themeBGM":
                return themeBGM;
            case "birdMove":
                return birdMove;
            case "birdSmile":
                return birdSmile;
            case "birdHurt":
                return birdHurt;
            case "birdEat":
                return birdEat;
            case "humanQuestion":
                return humanQuestion;
            case "humanAngry":
                return humanAngry;
            case "humanKind":
                return humanKind;
            case "successCollect":
                return successCollect;
            case "Loud":
                return Loud;
            case "enterNewMap":
                return enterNewMap;
        }
        return null;
    }
}
