using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioManager
{
    public static void PlayRandomClip(AudioSource source, AudioClip[] clips){
        AudioClip randClip = clips[(int)Random.Range(0, clips.Length)];
        source.PlayOneShot(randClip);
    }
}
