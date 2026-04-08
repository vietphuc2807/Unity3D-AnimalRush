using UnityEngine;

public class ButtonClickSound : MonoBehaviour
{
    [SerializeField] private string soundName = "click";

    public void PlayClickSound()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySFX(soundName);
        }
    }
}