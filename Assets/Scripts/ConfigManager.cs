using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigManager : MonoBehaviour
{
    public Button musicButton;
    public Button sfxButton;
    private AudioManager audioManager;

    private void Start()
    {
       
        if (PlayerPrefs.HasKey("GraphicQuality"))
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("GraphicQuality"));
        audioManager = FindObjectOfType<AudioManager>();

        UpdateMusicMute(PlayerPrefs.GetInt("MusicMuted", 0) == 1);
        UpdateSFXMute(PlayerPrefs.GetInt("SFXMuted", 0) == 1);

        musicButton.onClick.AddListener(ToggleMusic);
        sfxButton.onClick.AddListener(ToggleSFX);
    }
    public void SetCalidadAlta() => SetQuality(2);
    public void SetCalidadMedia() => SetQuality(1);
    public void SetCalidadBaja() => SetQuality(0);

    private void SetQuality(int level)
    {
        QualitySettings.SetQualityLevel(level, true);
        PlayerPrefs.SetInt("GraphicQuality", level);
        PlayerPrefs.Save();
        Debug.Log("Calidad gráfica establecida en: " + level);
    }
    void ToggleMusic()
    {
        bool isMuted = PlayerPrefs.GetInt("MusicMuted", 0) == 1;
        UpdateMusicMute(!isMuted);
        PlayerPrefs.SetInt("MusicMuted", !isMuted ? 1 : 0);
        PlayerPrefs.Save();
    }

    void ToggleSFX()
    {
        bool isMuted = PlayerPrefs.GetInt("SFXMuted", 0) == 1;
        UpdateSFXMute(!isMuted);
        PlayerPrefs.SetInt("SFXMuted", !isMuted ? 1 : 0);
        PlayerPrefs.Save();
    }

    void UpdateMusicMute(bool mute)
    {
        foreach (Sound s in audioManager.sounds)
        {
            if (s.isMusic)
                s.source.mute = mute;
        }
    }

    void UpdateSFXMute(bool mute)
    {
        foreach (Sound s in audioManager.sounds)
        {
            if (s.isSFX)
                s.source.mute = mute;
        }
    }
}
