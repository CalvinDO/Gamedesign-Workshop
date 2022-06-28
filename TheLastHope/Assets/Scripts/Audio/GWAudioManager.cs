using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GWAudioManager
{
    public static void PlayRandomClip(AudioSource source, AudioClip[] clips){
        AudioClip randClip = clips[(int)Random.Range(0, clips.Length)];
        source.PlayOneShot(randClip);
    }
    public static void PlayClip(AudioSource source, AudioClip clip){
        source.PlayOneShot(clip);
    }
    public static AudioClip GetRandomClip(AudioClip[] clips){
        AudioClip randClip = clips[(int)Random.Range(0, clips.Length)];
        return randClip;
    }
}
