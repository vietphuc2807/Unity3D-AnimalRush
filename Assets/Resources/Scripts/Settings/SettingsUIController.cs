using UnityEngine;
using DG.Tweening;

public class SettingsUIController : MonoBehaviour
{
    public GameObject settingsPanel;
    public RectTransform popup;

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);

        popup.localScale = Vector3.zero;

        popup.DOScale(1, 0.35f).SetEase(Ease.OutBack);
    }

    public void CloseSettings()
    {
        popup.DOScale(0f, 0.25f)
             .SetEase(Ease.InBack)
             .OnComplete(() =>
             {
                 settingsPanel.SetActive(false);
             });
    }

    public void ExitGame()
    {
        Debug.Log("Exit Game");

        Application.Quit();
    }
}
