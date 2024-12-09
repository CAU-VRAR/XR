using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    private Dictionary<string, AudioClip> audioClipCache;
    private AudioSource audioSource;
    public AudioSource bgmAudioSource;
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioClipCache = new Dictionary<string, AudioClip>();
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public AudioClip GetAudioClip(string soundName)
    {
        if (!audioClipCache.TryGetValue(soundName, out AudioClip clip))
        {
            clip = Resources.Load<AudioClip>("Sounds/" + soundName);
            if (clip != null)
            {
                audioClipCache[soundName] = clip;
            }
            else
            {
                Debug.LogWarning("Sound not found: " + soundName);
            }
        }
        return clip;
    }

    public void PlaySoundOneShot(string soundName, float volume = 0.3f, float pitch = 1.0f)
    {
        AudioClip clip = GetAudioClip(soundName);
        if (clip != null)
        {
            audioSource.PlayOneShot(clip, volume);
        }
    }

    public void ChangeStageBGM()
    {
        bgmAudioSource.pitch = 1.0f + (GameManager.instance.nowStage - 1) * 0.1f;
    }

    IEnumerator MusicFade(AudioSource audioSource, float duration, float startVolume, float targetVolume)
    {
        float currentTime = 0;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, currentTime / duration);
            yield return null;
        }
    }
}