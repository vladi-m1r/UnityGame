using System;
using UnityEngine;

[Serializable]
public class ShipAudioClips
{
    // No require stop
    public AudioClip collision;
    public AudioClip recoverFuel;
    public AudioClip deadSong;
    public AudioClip win;

    // require stop
    public AudioSource lowFuel;
    public AudioSource propulse;
}