using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LoadGame());
    }

    IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(3f); 

        AsyncOperation operation = SceneManager.LoadSceneAsync("Gameplay-001");

        while (!operation.isDone)
        {
            yield return null;
        }
    }
}