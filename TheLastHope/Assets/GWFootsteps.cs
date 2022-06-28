using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWFootsteps : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] pawnStepClips;
    public void PlayFootstep() {
        AudioManager.PlayRandomClip(audioSource, pawnStepClips);
    }
}
