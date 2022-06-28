using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWFootsteps : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] pawnStepClips;

    private AudioClip lastStepPlayed;
    public void PlayFootstep()
    {
        bool stepFound = false;
        while (!stepFound)
        {
            AudioClip stepToPlay = GWAudioManager.GetRandomClip(pawnStepClips);
            if (stepToPlay != lastStepPlayed)
            {
                GWAudioManager.PlayClip(audioSource, stepToPlay);
                lastStepPlayed = stepToPlay;
                stepFound = true;
            }
        }
    }
}
