using UnityEngine.Audio;
using UnityEngine;


[System.Serializable]
public class Sound
{
    public string name;
    public string category;
    public AudioClip clip;


    [Range(0f, 1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;

    public bool loop;

    public AudioMixer audioMixer;


    [HideInInspector]
    public AudioSource source;
}
