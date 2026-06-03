using UnityEngine;

public class MusicaFondo : MonoBehaviour
{
    private static MusicaFondo instancia;

    void Awake()
    {
        if (instancia != null)
        {
            Destroy(gameObject);
            return;
        }

        instancia = this;
        DontDestroyOnLoad(gameObject);
    }
}