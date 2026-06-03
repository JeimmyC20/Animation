using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class IntroVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject jugador;
    public GameObject textoPresionaE;
    public GameObject controlesInicio;
    public float tiempoControles = 5f;

    private PlayerMovementWithAnimations movimiento;

    void Start()
    {
        if (textoPresionaE != null)
            textoPresionaE.SetActive(false);

        if (controlesInicio != null)
            controlesInicio.SetActive(false);

        if (VideoManager.introYaVista)
        {
            gameObject.SetActive(false);
            return;
        }

        movimiento = jugador.GetComponent<PlayerMovementWithAnimations>();

        if (movimiento != null)
            movimiento.enabled = false;

        videoPlayer.Play();
        videoPlayer.loopPointReached += TerminoIntro;
    }

    void TerminoIntro(VideoPlayer vp)
    {
        VideoManager.introYaVista = true;

        if (textoPresionaE != null)
            textoPresionaE.SetActive(false);

        if (movimiento != null)
            movimiento.enabled = true;

        if (controlesInicio != null)
        {
            controlesInicio.SetActive(true);
            StartCoroutine(OcultarControles());
        }

        gameObject.SetActive(false);
    }

    IEnumerator OcultarControles()
    {
        yield return new WaitForSeconds(tiempoControles);

        if (controlesInicio != null)
            controlesInicio.SetActive(false);
    }
}