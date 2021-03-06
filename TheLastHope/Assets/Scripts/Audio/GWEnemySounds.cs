using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWEnemySounds : MonoBehaviour
{
    public AudioClip[] hurtSounds;
    public AudioClip[] chantSounds;

    public void PlayHurt(AudioSource source){
        source.Stop();
        source.volume = 1f;
        GWAudioManager.PlayRandomClip(source, hurtSounds);
    }

    public void PlayChant(AudioSource source){
        source.Stop();
        AudioClip clip;
        clip = GWAudioManager.GetRandomClip(chantSounds);
        source.volume = 0.5f;
        GWAudioManager.PlayClip(source, clip);
    }
}
