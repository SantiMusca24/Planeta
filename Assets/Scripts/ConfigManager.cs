using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConfigManager : MonoBehaviour
{
    public Button musicButton;
    public Button sfxButton;
    private AudioManager audioManager;
    public GameObject configCanvas;
    public GameObject cache;
    public GameObject storeMenu;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;
    private void Start()
    {
       
        if (PlayerPrefs.HasKey("GraphicQuality"))
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("GraphicQuality"));
        audioManager = FindObjectOfType<AudioManager>();

        UpdateMusicMute(PlayerPrefs.GetInt("MusicMuted", 0) == 1);
        UpdateSFXMute(PlayerPrefs.GetInt("SFXMuted", 0) == 1);

        musicButton.onClick.AddListener(ToggleMusic);
        sfxButton.onClick.AddListener(ToggleSFX);
        cache.SetActive(false);
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
    public void ToggleConfigCanvas()
    {
        if (storeMenu.activeSelf) storeMenu.SetActive(false);
        if (cache.activeSelf)
            cache.SetActive(false);

        configCanvas.SetActive(!configCanvas.activeSelf);
    }
    public void OpenStore()
    {
        ToggleConfigCanvas();
        storeMenu.SetActive(true);        
    }
    public void ResetGameData()
    {
        PlayerPrefs.DeleteAll();

        GameManager.Instance.count = 0;
        PlayerPrefs.SetFloat("Count", 0f);
        PlayerPrefs.SetInt("GraphicQuality", 1); 
        PlayerPrefs.SetInt("MusicMuted", 0);
        PlayerPrefs.SetInt("SFXMuted", 0);
        PlayerPrefs.Save();


        SceneManager.LoadScene("SampleScene");
    }
    public void AskResetConfirmation()
    {
        configCanvas.SetActive(false);
        cache.SetActive(true);

        yesButton.onClick.RemoveAllListeners();
        noButton.onClick.RemoveAllListeners();

        yesButton.onClick.AddListener(() =>
        {
            cache.SetActive(false);
            GameManager.Instance.ResetPlayerPrefs();
            
        });

        noButton.onClick.AddListener(() =>
        {
            cache.SetActive(false);
            configCanvas.SetActive(true); 
        });
    }

   
}
