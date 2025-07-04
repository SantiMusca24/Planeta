using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager instance = default;
    public SystemLanguage language = default;

    [SerializeField] private DataLocalization[] _data = default;

    private Dictionary<SystemLanguage, Dictionary<string, string>> _translate = new();

    private void Awake()
    {
        instance = this;
       
        _translate = LanguageU.LoadTranslate(_data);
        DontDestroyOnLoad(gameObject);
    }

    public string GetTranslate(string ID)
    {
        if (!_translate.ContainsKey(language))//puedo setear un idioma default
            return "No lang";

        if (!_translate[language].ContainsKey(ID))//valor default: NtF
            return "No ID";

        return _translate[language][ID];
    }
}