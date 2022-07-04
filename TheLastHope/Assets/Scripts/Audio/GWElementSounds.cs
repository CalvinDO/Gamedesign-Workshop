using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWElementSounds : MonoBehaviour
{
    public AudioClip[] fireClips;
    public AudioClip[] waterClips;
    public AudioClip[] earthClips;
    public AudioClip[] airClips;
    public AudioClip[] sandClips;
    public AudioClip[] ironClips;
    public AudioClip[] plantClips;
    public AudioClip[] lightningClips;
    public AudioClip[] steamClips;
    public AudioClip[] iceClips;
    public static GWElementSounds instance;

    void Awake() {
        GWElementSounds.instance = this;
    }

    public void PlayElement(AudioSource source, GWEType type, int phase){
        Debug.Log(source);
        source.Stop();
        switch(type){
            case GWEType.FIRE:
                GWAudioManager.PlayClip(source, fireClips[phase]);
                return;
            case GWEType.WATER:
                GWAudioManager.PlayClip(source, waterClips[phase]);
                return;
            case GWEType.EARTH:
                GWAudioManager.PlayClip(source, earthClips[phase]);
                return;
            case GWEType.AIR:
                GWAudioManager.PlayClip(source, airClips[phase]);
                return;
            case GWEType.SAND:
                GWAudioManager.PlayClip(source, sandClips[phase]);
                return;
            case GWEType.IRON:
                GWAudioManager.PlayClip(source, ironClips[phase]);
                return;
            case GWEType.PLANT:
                GWAudioManager.PlayClip(source, plantClips[phase]);
                return;
            case GWEType.LIGHTNING:
                GWAudioManager.PlayClip(source, lightningClips[phase]);
                return;
            case GWEType.STEAM:
                GWAudioManager.PlayClip(source, steamClips[phase]);
                return;
            case GWEType.ICE:
                GWAudioManager.PlayClip(source, iceClips[phase]);
                return;
            default:
                return;
        }
    }
}
