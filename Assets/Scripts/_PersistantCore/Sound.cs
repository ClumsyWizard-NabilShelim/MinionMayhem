using UnityEngine;
using UnityEngine.Audio;


[System.Serializable]
public class Sound 
{
    public AudioClip Clip;

    public bool Loop;

    [Range(0.0f, 1.0f)]
    public float Volume;

    public AudioSource Source { get; set; }
}
