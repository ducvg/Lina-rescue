﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public Sound[] sounds;
    public AudioMixerGroup BGM;
    public AudioMixerGroup SFX;

    private static SoundManager instance;
    public static SoundManager Instance
    {
        get => instance;
    }

    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            var sound = new GameObject(s.name);
            sound.transform.parent = this.transform;
            s.source = sound.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.isLoop;
            if (s.isSFX) s.source.outputAudioMixerGroup = SFX;
            else s.source.outputAudioMixerGroup = BGM;
        }

    }

    public void Play(string name)
    {
        Sound sound = sounds.FirstOrDefault(s => s.name == name);
        if (sound == null)
        {
            Debug.Log($"Sound {name} not found");
        }
        sound.source.Play();
    }

    public AudioSource CachedPlay(string name)
    {
        Sound sound = sounds.FirstOrDefault(s => s.name == name);
        if (sound == null)
        {
            Debug.Log($"Sound {name} not found");
        }
        sound.source.Play();
        return sound.source;
    }

    public void Play(string name, float time)
    {
        Sound sound = sounds.FirstOrDefault(s => s.name == name);
        if (sound == null)
        {
            Debug.Log($"Sound {name} not found");
        }
        sound.source.time = time;
        sound.source.Play();
    }

    public void Stop(string name)
    {
        Sound sound = sounds.FirstOrDefault(s => s.name == name);
        if (sound == null)
        {
            Debug.Log($"Sound {name} not found");
        }
        sound.source.Stop();
    }

    // Start is called before the first frame update
    void Start()
    {
        Play("background");
    }

    public void Stop()
    {
        
    }
}

[Serializable]
public class Sound
{
    public string name;
    [Range(0f,1f)] public float volume;
    public AudioClip clip;
    public bool isSFX;
    public bool isLoop;
    [HideInInspector] public AudioSource source;
}