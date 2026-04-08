using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class DeathUI : MonoBehaviour
{
    public static DeathUI Instance;

    [SerializeField] private Image deathImage;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        deathImage.enabled = false;
    }

    public void ShowDeath()
    {
        StartCoroutine(DeathRoutine());
    }

    IEnumerator DeathRoutine()
    {
        deathImage.enabled = true;

        float duration = 0.25f;
        float t = 0;

        while (t < duration)
        {
            t += Time.deltaTime;

            float alpha = Mathf.Lerp(0.8f, 0f, t / duration);
            deathImage.color = new Color(1, 0, 0, alpha);

            yield return null;
        }

        RespawnBlink.wasRespawned = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}