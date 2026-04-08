using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Collections;
using UnityEngine.SceneManagement;

public class WinUI : MonoBehaviour
{
    public static WinUI Instance;

    [Header("Popup")]
    [SerializeField] private RectTransform popup;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI deathText;
    [SerializeField] private TextMeshProUGUI boostText;
    [SerializeField] private TextMeshProUGUI bestTimeText;

    private RaceTimer raceTimer;

    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void ShowWin()
    {
        SoundManager.Instance.PlayMusic("06. Didn't Fall! (You Win)");

        gameObject.SetActive(true);

        popup.gameObject.SetActive(true);

        popup.localScale = Vector3.zero;
        popup.DOScale(1, 0.35f).SetEase(Ease.OutBack);

        raceTimer = FindFirstObjectByType<RaceTimer>();

        raceTimer.StopTimer();

        float time = raceTimer.GetTime();

        timeText.text = L("Time: ", "Thời gian: ") + FormatTime(time);

        deathText.text = L("Total Deaths: ", "Số lần chết: ") + GameStats.Instance.deathCount;

        boostText.text = L("Boosts Used: ", "Số lần tăng tốc: ") + GameStats.Instance.boostCount;

        ShowBestTime(time);

        Cursor.visible = true;

        StartCoroutine(PlayAnimation());
    }

    IEnumerator PlayAnimation()
    {
        timeText.transform.localScale = Vector3.zero;
        deathText.transform.localScale = Vector3.zero;
        boostText.transform.localScale = Vector3.zero;
        bestTimeText.transform.localScale = Vector3.zero;

        yield return new WaitForSecondsRealtime(0.7f);

        Pop(timeText.transform);
        yield return new WaitForSecondsRealtime(0.6f);

        Pop(deathText.transform);
        yield return new WaitForSecondsRealtime(0.6f);

        Pop(boostText.transform);
        yield return new WaitForSecondsRealtime(0.6f);
    
        Pop(bestTimeText.transform);
    }

    void Pop(Transform target)
    {
        target.DOScale(1.6f, 0.35f)
        .SetEase(Ease.OutBack)
        .OnComplete(() =>
        {
            target.DOScale(1f, 0.25f);
        });
    }

    string L(string en, string vi)
    {
        if (LanguageManager.Instance.CurrentLanguage == Language.Vietnamese)
            return vi;

        return en;
    }

    string FormatTime(float t)
    {
        int minutes = Mathf.FloorToInt(t / 60);
        int seconds = Mathf.FloorToInt(t % 60);
        int milliseconds = Mathf.FloorToInt((t * 100) % 100);

        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }

    void ShowBestTime(float time)
    {
        float best = PlayerPrefs.GetFloat("BestTime", 9999f);

        if (time < best)
        {
            best = time;
            PlayerPrefs.SetFloat("BestTime", best);
        }

        bestTimeText.text = L("Best Record: ", "Kỷ lục tốt nhất: ") + FormatTime(best);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Gameplay-001");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}