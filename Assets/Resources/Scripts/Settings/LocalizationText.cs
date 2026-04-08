using TMPro;
using UnityEngine;

public class LocalizationText : MonoBehaviour
{
    public string english;
    public string vietnamese;

    private TextMeshProUGUI textUI;

    void Awake()
    {
        textUI = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        switch (LanguageManager.Instance.CurrentLanguage)
        {
            case Language.English:
                textUI.text = english;
                break;

            case Language.Vietnamese:
                textUI.text = vietnamese;
                break;
        }
    }
}