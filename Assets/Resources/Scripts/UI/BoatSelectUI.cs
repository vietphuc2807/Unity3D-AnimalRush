using UnityEngine;
using DG.Tweening;

public class BoatSelectUI : MonoBehaviour
{
    public static bool isOpen = false;
    [SerializeField] private RectTransform popup;

    private void OnEnable()
    {
        isOpen = true;

        popup.localScale = Vector3.zero;

        popup.DOScale(1, 0.35f).SetEase(Ease.OutBack);
    }

    // button event open popup select boat
    public void OpenPopup()
    {
        if (isOpen) return;

        gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        isOpen = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePopup();
        }
    }

    public void ClosePopup()
    {
        popup.DOScale(0, 0.25f).SetEase(Ease.InBack).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
}