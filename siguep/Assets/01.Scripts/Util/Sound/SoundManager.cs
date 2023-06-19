using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource _bgmAudioSource;
    public AudioSource _sfxAudioSource;

    private void Awake()
    {
        Instance = this;
    }

    public void BGMPlay(AudioClip clip)
    {
        _bgmAudioSource.PlayOneShot(clip);
        _bgmAudioSource.loop = true;
    }
    public void SFXPlay(AudioClip clip)
    {
        _sfxAudioSource.PlayOneShot(clip);
        _sfxAudioSource.loop= false;
    }
}
