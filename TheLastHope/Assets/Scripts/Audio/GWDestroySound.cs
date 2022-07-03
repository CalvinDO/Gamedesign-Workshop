using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWDestroySound : MonoBehaviour
{
    public AudioClip[] destroySounds;

    public void PlayBoxDestroy(AudioSource source){
        GWAudioManager.PlayRandomClip(source, destroySounds);
    }
}
