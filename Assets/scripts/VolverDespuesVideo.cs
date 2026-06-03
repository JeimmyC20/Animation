using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VolverDespuesVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    public VideoClip[] videos;

    void Start()
    {
        string nombre =
            PlayerPrefs.GetString("VideoActual");

        foreach (var v in videos)
        {
            if (v.name == nombre)
            {
                videoPlayer.clip = v;
                videoPlayer.Play();
                break;
            }
        }

        videoPlayer.loopPointReached += TerminoVideo;
    }

    void TerminoVideo(VideoPlayer vp)
    {
        SceneManager.LoadScene("EscenaPrincipal");
    }
}