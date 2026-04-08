using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += PlayVideo;
        videoPlayer.loopPointReached += EndVideo;
    }

    void PlayVideo(VideoPlayer vp)
    {
        vp.Play();
    }

    void EndVideo(VideoPlayer vp)
    {
        SceneManager.LoadScene("Menu");
    }
}