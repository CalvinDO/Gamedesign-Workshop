using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWHurtSounds : MonoBehaviour {

    public AudioClip[] hurtSounds;
    public void SetMovementBlocked(int value) {
        GWPawnController.instance.isMovementBlocked = value != 0;
    }
    public void PlayHurt(AudioSource source){
        GWAudioManager.PlayRandomClip(source, hurtSounds);
    }
}
