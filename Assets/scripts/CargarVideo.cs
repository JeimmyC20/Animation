using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CargarVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        if (videoPlayer == null)
            videoPlayer = GetComponent<VideoPlayer>();

        if (VideoManager.videoSeleccionado == null)
        {
            SceneManager.LoadScene("EscenaPrincipal");
            return;
        }

        videoPlayer.Stop();
        videoPlayer.clip = VideoManager.videoSeleccionado;
        videoPlayer.loopPointReached += Termino;
        videoPlayer.Play();
    }

    void Termino(VideoPlayer vp)
    {
        if (vp.clip.name == "Final")
        {
            Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
        else
        {
            SceneManager.LoadScene("EscenaPrincipal");
        }
    }

}