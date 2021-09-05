﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class SoundManager : MonoBehaviour
{
    public AudioMixer mixer;
    public AudioSource bgSound;
    public AudioClip[] bglist;

    public static SoundManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
            SceneManager.sceneLoaded += OnSceneLoaded;

        }
        else
        {
            Destroy(gameObject);
        }
        
    }


    void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        for (int i = 0; i < bglist.Length; i++)
        {
            if (arg0.name == bglist[i].name)
            {
                BGSoundPlay(bglist[i]);
            }
        }
    }

    public void BGSoundVolume(float val)
    {
        mixer.SetFloat("BGSoundVolume", Mathf.Log10(val) * 20);
    }

    public void SFXVolume(float val)
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10(val) * 20);
    }

    public void SFXPlay(string sfxName, AudioClip clip)
    {
        GameObject go = new GameObject(sfxName + "Sound");
        AudioSource audiosource = go.AddComponent<AudioSource>();
        audiosource.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0];
        audiosource.clip = clip;
        audiosource.Play();

        Destroy(go, clip.length);
    }

    public void BGSoundPlay(AudioClip clip)
    {
        bgSound.outputAudioMixerGroup = mixer.FindMatchingGroups("BGSound")[0];
        bgSound.clip = clip;
        bgSound.loop = true;
        bgSound.volume = 0.1f;
        bgSound.Play();
    }
}
