using UnityEngine;
using TMPro;

public class ButtonTranslate : MonoBehaviour
{
    public string ID = default;
    public TextMeshProUGUI textUI = default;

    void Start()
    {
        if (gameObject.name != ID) ID = gameObject.name;
        textUI.text = LocalizationManager.instance.GetTranslate(ID);
    }
}