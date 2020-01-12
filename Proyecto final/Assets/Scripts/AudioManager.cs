using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO : When the user changes the volume in the menu this class should change for each sound
// its volume.

public class AudioManager : MonoBehaviour {

    public Sound[] SoundEffects;
    public Sound[] Songs;
    private Sound CurrentSong;

    public static AudioManager instance;

    int CurrentSongIndex;

    private void Awake()
    {

        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }


        foreach (Sound sound in SoundEffects)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            sound.AddSource(audioSource);
        }

        foreach (Sound song in Songs)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            song.AddSource(audioSource);
        }

    }

    private void Start()
    {
        CurrentSong = Songs[CurrentSongIndex];
        StartCoroutine(PlaySong());
    }

    public void ChangeSoundEffectSVolume(float volume)
    {
        foreach (Sound sound in SoundEffects)
        {
            sound.SetVolume(volume);
        }
    }

    public void ChangeMusicVolume(float volume)
    {
        foreach (Sound song in Songs)
        {
            song.SetVolume(volume);
        }
    }

    IEnumerator PlaySong()
    {
        while (true)
        {
            if (!CurrentSong.IsPlaying())
            {
                if(++CurrentSongIndex == Songs.Length) {
                    CurrentSongIndex = 0;
                }
                CurrentSong = Songs[CurrentSongIndex];
                CurrentSong.Play();
            } yield return null;
        }
    }



    public void PlaySoundEffect(string soundEffectName)
    {
        Sound soundEffect = FindSoundEffect(soundEffectName);

        if (soundEffect != null)
            soundEffect.Play();
        else Debug.LogWarning("Sound effect: " + soundEffectName + " not found.");
    }

    public void StopSoundEffect(string soundEffectName)
    {
        Sound soundEffect = FindSoundEffect(soundEffectName);

        if (soundEffect != null)
            soundEffect.Stop();
        else Debug.LogWarning("Sound effect: " + soundEffectName + " not found.");
    }

    public Sound FindSoundEffect(string soundEffectName)
    {

        foreach (Sound sound in SoundEffects)
        {
            if (sound.Name == soundEffectName)
            {
                return sound;
            }
        }
        return null;
    }


}

