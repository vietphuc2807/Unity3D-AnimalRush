using UnityEngine;
using System.Collections;

public class RespawnBlink : MonoBehaviour
{
    public static bool wasRespawned = false;

    private Renderer[] renderers;

    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();

        if (wasRespawned)
        {
            SoundManager.Instance.PlaySFX("revive");
            StartCoroutine(BlinkEffect());
            wasRespawned = false;
        }
    }

    IEnumerator BlinkEffect()
    {
        float duration = 2f;
        float timer = 0f;

        while (timer < duration)
        {
            SetVisible(false);
            yield return new WaitForSeconds(0.15f);

            SetVisible(true);
            yield return new WaitForSeconds(0.15f);

            timer += 0.3f;
        }

        SetVisible(true);
    }

    void SetVisible(bool value)
    {
        foreach (Renderer r in renderers)
        {
            r.enabled = value;
        }
    }
}
