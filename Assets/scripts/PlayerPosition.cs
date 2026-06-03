using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    public static Vector3 savedPosition = Vector3.zero;
    public static Quaternion savedRotation = Quaternion.identity;
    public static bool tienePosicionGuardada = false;

    void Awake()
    {
        if (tienePosicionGuardada)
        {
            CharacterController controller = GetComponent<CharacterController>();

            if (controller != null)
                controller.enabled = false;

            transform.position = savedPosition;
            transform.rotation = savedRotation;

            if (controller != null)
                controller.enabled = true;
        }
    }

    public static void Guardar(Transform player)
    {
        savedPosition = player.position;
        savedRotation = player.rotation;
        tienePosicionGuardada = true;
    }
}