using UnityEngine;
using System.Collections;

public class MostrarControlesInicio : MonoBehaviour
{
    public GameObject controlesInicio;
    public float tiempoVisible = 5f;

    void Start()
    {
        if (controlesInicio != null)
            controlesInicio.SetActive(true);

        StartCoroutine(OcultarDespues());
    }

    IEnumerator OcultarDespues()
    {
        yield return new WaitForSeconds(tiempoVisible);

        if (controlesInicio != null)
            controlesInicio.SetActive(false);
    }
}