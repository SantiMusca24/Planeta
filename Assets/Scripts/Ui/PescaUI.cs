using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PescaUI : BaseContadorUI
{
    public override void OcultarMinigameTextos()
    {
        if (resumenText != null) resumenText.gameObject.SetActive(false);
    }
    public override void OcultarMinigameTextos2()
    {
        if (tiempoText != null) tiempoText.gameObject.SetActive(false);
        if (cortesText != null) cortesText.gameObject.SetActive(false);
        if (troncosText != null) troncosText.gameObject.SetActive(false);
    }
    public override void ShowPanel(MinigamePanelType panelType, string resumen = "")
    {
        if (minigamePanel != null) minigamePanel.SetActive(panelType == MinigamePanelType.Minigame);
        if (resumenPanel != null) resumenPanel.SetActive(panelType == MinigamePanelType.Summary);

        if (panelType == MinigamePanelType.Summary && resumenText != null)
        {
            resumenText.text = resumen;
        }
    }
    public override void UpdateMinigameUI(int? cortes = null, int? troncos = null, float? tiempo = null, int? cortesNecesarios = null)
    {
        if (cortes.HasValue && cortesNecesarios.HasValue && cortesText != null)
            cortesText.text = $"Peces: {cortes}/{cortesNecesarios}";

        if (troncos.HasValue && troncosText != null)
            troncosText.text = $"Barriles: {troncos}";

        if (tiempo.HasValue && tiempoText != null)
            tiempoText.text = $": {tiempo.Value:F1}s";
    }
}
