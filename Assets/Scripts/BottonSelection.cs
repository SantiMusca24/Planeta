using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottonSelection : MonoBehaviour
{
    [Header("Botones de calidad")]
    [SerializeField] private Button botonAlta;
    [SerializeField] private Button botonMedia;
    [SerializeField] private Button botonBaja;

    [Header("Botones de audio")]
    [SerializeField] private Button botonMusica;
    [SerializeField] private Button botonSFX;

    [Header("Colores")]
    [SerializeField] private Color colorSeleccionado = Color.green;
    [SerializeField] private Color colorNormal = Color.white;
    [SerializeField] private Color colorAudioOn = Color.green;
    [SerializeField] private Color colorAudioOff = Color.gray;

    private Button botonCalidadActivo;
    private bool musicaActiva;
    private bool sfxActivo;

    private void Start()
    {
        int calidadGuardada = PlayerPrefs.GetInt("Calidad", 1);
        QualitySettings.SetQualityLevel(calidadGuardada);

        switch (calidadGuardada)
        {
            case 0: SeleccionarCalidad(botonBaja); break;
            case 1: SeleccionarCalidad(botonMedia); break;
            case 2: SeleccionarCalidad(botonAlta); break;
        }

        musicaActiva = PlayerPrefs.GetInt("Musica", 1) == 1;
        sfxActivo = PlayerPrefs.GetInt("SFX", 1) == 1;

        ActualizarColorAudio();
    }

    public void SetCalidadBaja()
    {
        SeleccionarCalidad(botonBaja);
        QualitySettings.SetQualityLevel(0);
        PlayerPrefs.SetInt("Calidad", 0);
    }

    public void SetCalidadMedia()
    {
        SeleccionarCalidad(botonMedia);
        QualitySettings.SetQualityLevel(1);
        PlayerPrefs.SetInt("Calidad", 1);
    }

    public void SetCalidadAlta()
    {
        SeleccionarCalidad(botonAlta);
        QualitySettings.SetQualityLevel(2);
        PlayerPrefs.SetInt("Calidad", 2);
    }

    private void SeleccionarCalidad(Button botonSeleccionado)
    {
        if (botonCalidadActivo != null)
            botonCalidadActivo.image.color = colorNormal;

        botonSeleccionado.image.color = colorSeleccionado;
        botonCalidadActivo = botonSeleccionado;
    }

    public void ToggleMusica()
    {
        musicaActiva = !musicaActiva;
        PlayerPrefs.SetInt("Musica", musicaActiva ? 1 : 0);
        ActualizarColorAudio();
    }

    public void ToggleSFX()
    {
        sfxActivo = !sfxActivo;
        PlayerPrefs.SetInt("SFX", sfxActivo ? 1 : 0);
        ActualizarColorAudio();
    }

    private void ActualizarColorAudio()
    {
        botonMusica.image.color = musicaActiva ? colorAudioOn : colorAudioOff;
        botonSFX.image.color = sfxActivo ? colorAudioOff : colorAudioOn;
    }


}
