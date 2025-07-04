using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TranslationManager : MonoBehaviour
{
    public static TranslationManager instance;

    [SerializeField] private List<TranslatableElement> translatableElements = new();
    [SerializeField] private SystemLanguage defaultLanguage = SystemLanguage.English;

    [System.Serializable]
    public class TranslatableElement
    {
        public string ID;
        public TextMeshProUGUI textUI;
    }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); 
    }
    private void Start()
    {
        
       
        if (LocalizationManager.instance.language == default)
            LocalizationManager.instance.language = defaultLanguage;

        ApplyTranslations();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
        SetLanguage(SystemLanguage.English);
    
        }  
    else if (Input.GetKeyDown(KeyCode.K))
        {
        SetLanguage(SystemLanguage.Slovenian);
       
        }
    }

    public void SetLanguage(SystemLanguage newLanguage)
    {
        LocalizationManager.instance.language = newLanguage;
        ApplyTranslations();
    }

    public void ApplyTranslations()
    {
        foreach (var element in translatableElements)
        {
            if (element.textUI != null && !string.IsNullOrEmpty(element.ID))
            {
                element.textUI.text = LocalizationManager.instance.GetTranslate(element.ID);
            }
        }
    }

    public void RegisterElement(TextMeshProUGUI textUI, string ID)
    {
        var existing = translatableElements.Find(e => e.textUI == textUI);
        if (existing == null)
        {
            translatableElements.Add(new TranslatableElement { ID = ID, textUI = textUI });
        }
    }
}
