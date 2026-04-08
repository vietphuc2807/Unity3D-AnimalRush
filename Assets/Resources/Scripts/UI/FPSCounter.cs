using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    public TextMeshProUGUI fpsText;

    private float deltaTime;
    private float timer;

    string L(string en, string vi)
    {
        if (LanguageManager.Instance != null &&
            LanguageManager.Instance.CurrentLanguage == Language.Vietnamese)
            return vi;

        return en;
    }

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        timer += Time.unscaledDeltaTime;

        if (timer < 0.5f) return; 
        timer = 0f;

        float fps = 1.0f / deltaTime;

        if (fps >= 60)
            fpsText.color = Color.green;
        else if (fps >= 30)
            fpsText.color = Color.yellow;
        else
            fpsText.color = Color.red;

        fpsText.text = L("FPS: ", "Tốc độ khung hình: ") + Mathf.Ceil(fps);
    }
}