using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartManager : MonoBehaviour
{
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            SoundManager.Instance.PlayMusic("03. Survive The Fall");
        }
    }
    public void StartGame()
    {
        Application.targetFrameRate = 144;
        SceneManager.LoadScene("Loading");
    }
}
