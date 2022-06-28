using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWAttackSounds : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip throwClipBuilup;
    public AudioClip shootClipBuildup;
    public AudioClip stompClipBuildup;

    public AudioClip throwClipSummon;
    public AudioClip shootClipSummon;
    public AudioClip stompClipSummon;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuildupStomp() {
        this.audioSource.Stop();
        this.audioSource.PlayOneShot(this.stompClipBuildup);
    }

    public void BuildupShoot() {
        this.audioSource.Stop();

        this.audioSource.PlayOneShot(this.shootClipBuildup);

    }

    public void BuildupThrow() {
        this.audioSource.Stop();

        this.audioSource.PlayOneShot(this.throwClipBuilup);

    }

    public void SummonStomp() {
        this.audioSource.Stop();

        this.audioSource.PlayOneShot(this.stompClipSummon);

    }

    public void SummonShoot() {
        this.audioSource.Stop();

        this.audioSource.PlayOneShot(this.shootClipSummon);
    }

    public void SummonThrow() {
        this.audioSource.Stop();

        this.audioSource.PlayOneShot(this.throwClipSummon);

    }
}
