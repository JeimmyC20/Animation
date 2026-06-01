using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class CinematicTrigger : MonoBehaviour
{
    [Header("Video")]
    public VideoPlayer videoPlayer;
    public Canvas cinematicCanvas;    // canvas que cubre la pantalla
    public VideoClip clip;             // arrastra el video .mp4

    [Header("Notificar al GameManager")]
    public FinalEvent finalEvent;       // referencia al manager final

    private PlayerController player;
    private bool played = false;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        if (cinematicCanvas != null) cinematicCanvas.gameObject.SetActive(false);

        if (videoPlayer != null)
        {
            videoPlayer.clip = clip;
            videoPlayer.playOnAwake = false;
            videoPlayer.loopPointReached += OnVideoEnd;
        }
    }

    public void PlayCinematic()
    {
        if (played) return;
        played = true;
        StartCoroutine(CinematicRoutine());
    }

    IEnumerator CinematicRoutine()
    {
        // 1. Bloquear al jugador
        player?.SetCanMove(false);

        // 2. Pequeña pausa para que la animación de "recoger" arranque
        yield return new WaitForSeconds(0.8f);

        // 3. Mostrar canvas y reproducir video
        if (cinematicCanvas != null) cinematicCanvas.gameObject.SetActive(true);
        videoPlayer?.Play();

        // 4. Esperar fin del video (OnVideoEnd lo maneja)
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        StartCoroutine(EndCinematic());
    }

    IEnumerator EndCinematic()
    {
        // Ocultar canvas
        if (cinematicCanvas != null) cinematicCanvas.gameObject.SetActive(false);

        // Pequeña pausa antes de devolver control
        yield return new WaitForSeconds(0.3f);

        // Devolver control al jugador
        player?.SetCanMove(true);

        // Notificar al FinalEvent que se recogió este objeto
        finalEvent?.RegisterCollected();

        Debug.Log("[Guardián] Cinemática terminada. Control devuelto.");
    }
}