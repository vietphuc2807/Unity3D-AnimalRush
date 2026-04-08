using UnityEngine;
using TMPro;
using System.Collections;

public class RaceTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;

    private BoatMovement boat;

    private float timer;
    private bool timerStarted;
    private bool blinkStarted;

    void Start()
    {
        timeText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (boat == null)
        {
            boat = FindFirstObjectByType<BoatMovement>();
            return;
        }

        if (!timerStarted && !blinkStarted)
        {
            if (boat.IsMoving())
            {
                StartCoroutine(StartTimerEffect());
            }
        }

        if (timerStarted)
        {
            timer += Time.deltaTime;
            UpdateTimerUI();
        }
    }

    IEnumerator StartTimerEffect()
    {
        blinkStarted = true;

        timeText.gameObject.SetActive(true);

        for (int i = 0; i < 6; i++)
        {
            timeText.enabled = !timeText.enabled;
            yield return new WaitForSeconds(0.15f);
        }

        timeText.enabled = true;

        timerStarted = true;
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        int milliseconds = Mathf.FloorToInt((timer * 100) % 100);

        timeText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }

    public float GetTime()
    {
        return timer;
    }

    public void StopTimer()
    {
        timerStarted = false;
    }
}