using UnityEngine;

public class GameStats : MonoBehaviour
{
    public static GameStats Instance;

    public int deathCount;
    public int boostCount;

    private void Start()
    {
        SoundManager.Instance.PlayMusic("10. Fall for the Queen Bean");
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddDeath()
    {
        deathCount++;
    }

    public void AddBoost()
    {
        boostCount++;
    }
}