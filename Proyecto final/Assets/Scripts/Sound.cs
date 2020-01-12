using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound{
    public string Name;
    public AudioClip Clip;

    [Range(0f, 1f)]
    public float Volume;
    [Range(.1f, 3f)]
    public float Pitch;

    public bool Loop;
    [HideInInspector]
    public AudioSource AudioSource;

    public void AddSource(AudioSource source)
    {
        source.clip = this.Clip;
        source.volume = this.Volume;
        source.pitch = this.Pitch;
        this.AudioSource = source;
    }

    public void Play()
    {
        this.AudioSource.Play();
    }

    public void Stop()
    {
        this.AudioSource.Stop();
    }

    public void SetVolume(float volume)
    {
        this.Volume = volume;
        this.AudioSource.volume = volume;
    }
    
    public bool IsPlaying()
    {
        return this.AudioSource.isPlaying;
    }
}
