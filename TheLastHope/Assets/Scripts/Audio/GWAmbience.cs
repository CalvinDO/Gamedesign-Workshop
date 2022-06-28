using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWAmbience : MonoBehaviour
{
    public AudioSource source;
    public AudioClip crickets;
    public AudioClip[] crows;
    private float crowCooldown;
    private AudioClip lastCrowPlayed;
    private void Start()
    {
        InvokeRepeating("PlayCrickets", 0, crickets.length);
        crowCooldown = Random.Range(8, 20);
    }
    private void Update()
    {
        if(crowCooldown<=0){
            AudioClip crowToPlay = GWAudioManager.GetRandomClip(crows);
            if(crowToPlay != lastCrowPlayed){
                GWAudioManager.PlayClip(source, crowToPlay);
                crowCooldown = Random.Range(8, 20);
                lastCrowPlayed = crowToPlay;
            }
        }
        else {
            crowCooldown -= Time.deltaTime;
        }
    }
    private void PlayCrickets(){
        GWAudioManager.PlayClip(source, crickets);
    }
}
