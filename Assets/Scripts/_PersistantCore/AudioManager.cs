using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Audio;
using ClumsyWizard.Utilities;

public enum SoundKey
{
    Spawn,
    HitCharacter,
    HitStructure,
    CharacterDie,
    StructureDestroy,
    BowTwang
}

public class AudioManager : Persistant<AudioManager>
{
    [SerializeField] private ClumsyDictionary<SoundKey, Sound> sounds;
    [SerializeField] private Sound backgroundMusic;

    protected override void Awake()
    {
        base.Awake();

        foreach (Sound sound in sounds.Values)
        {
            sound.Source = gameObject.AddComponent<AudioSource>();
            sound.Source.clip = sound.Clip;
            sound.Source.loop = sound.Loop;
            sound.Source.volume = sound.Volume;
        }

        AudioSource backgroundSource = gameObject.AddComponent<AudioSource>();
        backgroundMusic.Source = backgroundSource;
        backgroundSource.clip = backgroundMusic.Clip;
        backgroundSource.loop = backgroundMusic.Loop;
        backgroundSource.volume = backgroundMusic.Volume;

        backgroundSource.Play();
    }

    public void PlayAudio(SoundKey key)
    {
        if(!sounds[key].Source.isPlaying)
            sounds[key].Source.Play();
    }

    public void StopAudio(SoundKey key)
    {
        sounds[key].Source.Stop();
    }

    protected override void CleanUpStaticData()
    {
    }
}
