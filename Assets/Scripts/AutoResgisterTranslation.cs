using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AutoResgisterTranslation : MonoBehaviour
{
    public string ID;

    private void Start()
    {
        var text = GetComponent<TextMeshProUGUI>();
        if (text == null) return;

        if (string.IsNullOrEmpty(ID))
            ID = gameObject.name;

        TranslationManager.instance.RegisterElement(text, ID);
    }
}
