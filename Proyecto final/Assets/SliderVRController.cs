using UnityEngine;
using UnityEngine.UI;


public class SliderVRController : MonoBehaviour
{
    private bool isActive;
    private Transform reticle;
    private float previousX;
    private Slider slider;
    private float changeAmount;
    private AudioManager audioManager;

    private void Start()
    {
        isActive = false;
        changeAmount = 0.1f;
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        reticle = GameObject.Find("Main Camera").transform;
        slider = GetComponent<Slider>();
        previousX = reticle.position.x;
        SetInitialVolume();
    }

    private void SetInitialVolume()
    {
        if (gameObject.name == "MusicVolumeSlider")
        {
            slider.value = audioManager.GetMusicVolume();
        }
        else
        {
            slider.value = audioManager.GetSoundEffectsVolume();
        }
    }

    private void ChangeSliderValue()
    {
        if (gameObject.name == "MusicVolumeSlider")
        {
            audioManager.ChangeMusicVolume(slider.value);
        }
        else
        {
            audioManager.ChangeSoundEffectSVolume(slider.value);
        }
    }

    public void TurnVolumeUp()
    {
        if (slider.value < slider.maxValue)
        {
            slider.value += changeAmount;
            ChangeSliderValue();
        }
    }

    public void TurnVolumeDown()
    {
        if (slider.value > slider.minValue)
        {
            slider.value -= changeAmount;
            ChangeSliderValue();
        }
    }

    public void Activate()
    {
        isActive = true;
    }

    public void Deactivate()
    {
        isActive = false;
    }
}
