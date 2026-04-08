using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager Instance;

    public Language CurrentLanguage { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            LoadLanguage();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void LoadLanguage()
    {
        CurrentLanguage = (Language)PlayerPrefs.GetInt("Language", 0);
    }

    public void SetLanguage(Language lang)
    {
        CurrentLanguage = lang;

        PlayerPrefs.SetInt("Language", (int)lang);

        UpdateAllTexts();
    }

    void UpdateAllTexts()
    {
        LocalizationText[] texts = FindObjectsByType<LocalizationText>(FindObjectsSortMode.None);

        foreach (var t in texts)
        {
            t.UpdateText();
        }
    }
}