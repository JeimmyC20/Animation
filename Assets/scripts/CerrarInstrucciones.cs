using UnityEngine;
using UnityEngine.InputSystem;

public class CerrarInstrucciones : MonoBehaviour
{
    public GameObject panelInstrucciones;
    public PlayerMovementWithAnimations movimientoJugador;

    void Start()
    {
        if (movimientoJugador != null)
            movimientoJugador.enabled = false;
    }

    void Update()
    {
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            if (panelInstrucciones != null)
                panelInstrucciones.SetActive(false);

            if (movimientoJugador != null)
                movimientoJugador.enabled = true;

            enabled = false;
        }
    }
}