using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource soundSource;
    
    [Header("Audio Clips")]
    public AudioClip[] musicClips;
    public AudioClip[] soundClips;
    
    [Header("Audio Settings")]
    [Range(0f, 1f)]
    public float musicVolume = 1f;
    [Range(0f, 1f)]
    public float soundVolume = 1f;
    
    private static AudioManager instance;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        // 初始化音频源
        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = false;
        }
        
        if (soundSource == null)
        {
            soundSource = gameObject.AddComponent<AudioSource>();
            soundSource.loop = false;
        }
    }

    public static void PlayMusic(string musicName, bool loop = true)
    {
        if (instance == null)
        {
            Debug.LogWarning("AudioManager instance not found!");
            return;
        }
        
        AudioClip clip = instance.FindMusicClip(musicName);
        if (clip != null)
        {
            instance.musicSource.clip = clip;
            instance.musicSource.loop = loop;
            instance.musicSource.volume = instance.musicVolume;
            instance.musicSource.Play();
        }
        else
        {
            Debug.LogWarning($"Music clip '{musicName}' not found!");
        }
    }

    public static void PlaySound(string soundName, bool loop = false)
    {
        if (instance == null)
        {
            Debug.LogWarning("AudioManager instance not found!");
            return;
        }
        
        AudioClip clip = instance.FindSoundClip(soundName);
        if (clip != null)
        {
            instance.soundSource.PlayOneShot(clip, instance.soundVolume);
        }
        else
        {
            Debug.LogWarning($"Sound clip '{soundName}' not found!");
        }
    }
    
    private AudioClip FindMusicClip(string clipName)
    {
        foreach (AudioClip clip in musicClips)
        {
            if (clip.name == clipName)
                return clip;
        }
        return null;
    }
    
    private AudioClip FindSoundClip(string clipName)
    {
        foreach (AudioClip clip in soundClips)
        {
            if (clip.name == clipName)
                return clip;
        }
        return null;
    }
    
    // 音量控制方法
    public static void SetMusicVolume(float volume)
    {
        if (instance != null)
        {
            instance.musicVolume = Mathf.Clamp01(volume);
            instance.musicSource.volume = instance.musicVolume;
        }
    }
    
    public static void SetSoundVolume(float volume)
    {
        if (instance != null)
        {
            instance.soundVolume = Mathf.Clamp01(volume);
        }
    }
    
    // 停止音乐
    public static void StopMusic()
    {
        if (instance != null && instance.musicSource != null)
        {
            instance.musicSource.Stop();
        }
    }
}
