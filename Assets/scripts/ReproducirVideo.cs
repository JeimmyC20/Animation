using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class ReproducirVideo : MonoBehaviour
{
    public VideoClip video;
    public GameObject textoPresionaE;

    private bool jugadorCerca = false;

    void Start()
    {
        jugadorCerca = false;

        if (textoPresionaE != null)
            textoPresionaE.SetActive(false);
            
    }

    void Update()
    {
        if (jugadorCerca && Keyboard.current.eKey.wasPressedThisFrame)
        {
            Debug.Log("Video enviado por: " + gameObject.name +
                      " | Clip: " + video.name);

            GameObject player = GameObject.FindWithTag("Player");

            if (player != null)
                PlayerPosition.Guardar(player.transform);

            //PromptUI.Instance.Ocultar();

            VideoManager.videoSeleccionado = video;
            SceneManager.LoadScene("VideoScene");
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        jugadorCerca = true;

        if (VideoManager.introYaVista && textoPresionaE != null)
            textoPresionaE.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        jugadorCerca = false;

        if (textoPresionaE != null)
            textoPresionaE.SetActive(false);
    }

    void OnDisable()
    {
        if (textoPresionaE != null)
            textoPresionaE.SetActive(false);
    }
}