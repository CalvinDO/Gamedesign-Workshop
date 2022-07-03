using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWCardSounds : MonoBehaviour
{
    public AudioClip pickupSound;
    public AudioClip combineSound;

    public void PlayPickUp(AudioSource source){
        GWAudioManager.PlayClip(source, pickupSound);
    }
    public void PlayCombine(AudioSource source){
        GWAudioManager.PlayClip(source, combineSound);
    }
}
