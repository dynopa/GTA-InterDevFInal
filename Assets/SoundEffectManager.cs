using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager Instance;

    public AudioSource[] soundEffectsSources;
    [Space]
    public AudioClip[] hornHonks;
    int currentSourceToPlay;


    private void Awake()
    {
        Instance = this;
    }

    public void PlaySoundEffect(AudioClip soundEffect, float volume, bool randomPitch)
    {
        soundEffectsSources[currentSourceToPlay].clip = soundEffect;
        soundEffectsSources[currentSourceToPlay].volume = volume;
        if (randomPitch)
        {
            soundEffectsSources[currentSourceToPlay].pitch = Random.Range(.9f, 1.07f);
        }
        else soundEffectsSources[currentSourceToPlay].pitch = 1;

        soundEffectsSources[currentSourceToPlay].Play();

        if (currentSourceToPlay >= soundEffectsSources.Length - 1)
        {
            currentSourceToPlay = 0;
        }
        else currentSourceToPlay++;

    }


    public void CarHonk()
    {
        PlaySoundEffect(hornHonks[Random.Range(0, hornHonks.Length)], Random.Range(.6f, .7f), true);
    }





}
