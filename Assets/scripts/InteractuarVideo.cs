using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InteractuarVideo : MonoBehaviour
{
    private bool jugadorCerca = false;

    void Update()
    {
        if (jugadorCerca &&
            Keyboard.current.eKey.wasPressedThisFrame)
        {
            GameObject player = GameObject.FindWithTag("Player");

            if (player != null)
            {
                PlayerPosition.savedPosition = player.transform.position;
            }

            SceneManager.LoadScene("VideoScene");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;
            Debug.Log("Presiona E");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
        }
    }
}