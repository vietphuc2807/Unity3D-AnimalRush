using UnityEngine;
using UnityEngine.UI;

public class SoundSettingsUI : MonoBehaviour
{
    [Header("Sliders")]
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        if (SoundManager.Instance == null) return;

        masterSlider.SetValueWithoutNotify(SoundManager.Instance.GetMasterVolume());
        musicSlider.SetValueWithoutNotify(SoundManager.Instance.GetMusicVolume());
        sfxSlider.SetValueWithoutNotify(SoundManager.Instance.GetSFXVolume());

        masterSlider.onValueChanged.AddListener(OnMasterChanged);
        musicSlider.onValueChanged.AddListener(OnMusicChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXChanged);
    }

    public void OnMasterChanged(float value)
    {
        SoundManager.Instance.SetMasterVolume(value);
    }

    public void OnMusicChanged(float value)
    {
        SoundManager.Instance.SetMusicVolume(value);
    }

    public void OnSFXChanged(float value)
    {
        SoundManager.Instance.SetSFXVolume(value);
    }
}