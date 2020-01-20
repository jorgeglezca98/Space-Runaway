using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;
    [HideInInspector]
    public AudioSource audioSource;

    public void AddSource(AudioSource source)
    {
        source.clip = this.clip;
        source.volume = this.volume;
        source.pitch = this.pitch;
        this.audioSource = source;
    }

    public void Play()
    {
        this.audioSource.Play();
    }

    public void Stop()
    {
        this.audioSource.Stop();
    }

    public void SetVolume(float volume)
    {
        this.volume = volume;
        this.audioSource.volume = volume;
    }

    public float GetVolume()
    {
        return this.audioSource.volume;
    }

    public bool IsPlaying()
    {
        return this.audioSource.isPlaying;
    }
}
