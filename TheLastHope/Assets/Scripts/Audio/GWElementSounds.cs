using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWElementSounds : MonoBehaviour
{
    public AudioClip[] fireClips;
    public AudioClip[] waterClips;
    public AudioClip[] earthClips;
    public AudioClip[] airClips;
    public static GWElementSounds instance;
    public void PlayElement(AudioSource source, GWEType type, int phase){
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
            default:
                return;
        }
    }
}
