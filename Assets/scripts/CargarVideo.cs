using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using System.Collections;

public class CargarVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        if (videoPlayer == null)
            videoPlayer = GetComponent<VideoPlayer>();

        if (VideoManager.videoSeleccionado == null)
        {
            SceneManager.LoadScene("SampleScene");
            return;
        }

        videoPlayer.clip = VideoManager.videoSeleccionado;
        videoPlayer.loopPointReached += Termino;
        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += Preparado;
    }

    void Preparado(VideoPlayer vp)
    {
        Debug.Log("Video preparado, duracion: " + vp.clip.length + " segundos");
        vp.Play();
    }

    void Termino(VideoPlayer vp)
    {
        Debug.Log("Video terminado");
        SceneManager.LoadScene("SampleScene");
    }
}