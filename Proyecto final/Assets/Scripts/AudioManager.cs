using System.Collections;
using UnityEngine;

// TODO : When the user changes the volume in the menu this class should change for each sound
// its volume.

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] soundEffects;
    public Sound[] songs;
    private Sound currentSong;
    private float musicVolume;
    private float soundEffectsVolume;
    private int currentSongIndex;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            musicVolume = 1f;
            soundEffectsVolume = 1f;
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Sound sound in soundEffects)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            sound.AddSource(audioSource);
        }

        foreach (Sound song in songs)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            song.AddSource(audioSource);
        }
    }

    private void Start()
    {
        currentSong = songs[currentSongIndex];
        StartCoroutine(PlaySong());
    }

    public float GetMusicVolume()
    {
        return musicVolume;
    }

    public float GetSoundEffectsVolume()
    {
        return soundEffectsVolume;
    }

    public void ChangeSoundEffectSVolume(float volume)
    {
        soundEffectsVolume = volume;
        foreach (Sound sound in soundEffects)
        {
            sound.SetVolume(volume);
        }
    }

    public void ChangeMusicVolume(float volume)
    {
        musicVolume = volume;
        foreach (Sound song in songs)
        {
            song.SetVolume(volume);
        }
    }

    private IEnumerator PlaySong()
    {
        while (true)
        {
            if (!currentSong.IsPlaying())
            {
                if (++currentSongIndex == songs.Length)
                {
                    currentSongIndex = 0;
                }
                currentSong = songs[currentSongIndex];
                currentSong.Play();
            }
            yield return null;
        }
    }

    public void PlaySoundEffect(string soundEffectName)
    {
        Sound soundEffect = FindSoundEffect(soundEffectName);

        if (soundEffect != null)
        {
            soundEffect.Play();
        }
        else
        {
            Debug.LogWarning("Sound effect: " + soundEffectName + " not found.");
        }
    }

    public void StopSoundEffect(string soundEffectName)
    {
        Sound soundEffect = FindSoundEffect(soundEffectName);

        if (soundEffect != null)
        {
            soundEffect.Stop();
        }
        else
        {
            Debug.LogWarning("Sound effect: " + soundEffectName + " not found.");
        }
    }

    public Sound FindSoundEffect(string soundEffectName)
    {
        foreach (Sound sound in soundEffects)
        {
            if (sound.name == soundEffectName)
            {
                return sound;
            }
        }
        return null;
    }
}
