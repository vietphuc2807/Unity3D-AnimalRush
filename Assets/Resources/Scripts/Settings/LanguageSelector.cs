using TMPro;
using UnityEngine;

public class LanguageSelector : MonoBehaviour
{
    public TextMeshProUGUI languageText;

    private Language currentLanguage;

    void Start()
    {
        currentLanguage = LanguageManager.Instance.CurrentLanguage;
        UpdateUI();
    }

    public void NextLanguage()
    {
        currentLanguage++;

        if ((int)currentLanguage >= System.Enum.GetValues(typeof(Language)).Length)
            currentLanguage = 0;

        Apply();
    }

    public void PreviousLanguage()
    {
        currentLanguage--;

        if ((int)currentLanguage < 0)
            currentLanguage = (Language)(System.Enum.GetValues(typeof(Language)).Length - 1);

        Apply();
    }

    void Apply()
    {
        LanguageManager.Instance.SetLanguage(currentLanguage);
        UpdateUI();
    }

    void UpdateUI()
    {
        switch (currentLanguage)
        {
            case Language.English:
                languageText.text = "English";
                break;

            case Language.Vietnamese:
                languageText.text = "Tiếng Việt";
                break;
        }
    }
}