using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class ReproducirVideo : MonoBehaviour
{
    public VideoClip video;

    public void LanzarVideo()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
            PlayerPosition.Guardar(player.transform);

        VideoManager.videoSeleccionado = video;
        SceneManager.LoadScene("VideoScene");
    }
}