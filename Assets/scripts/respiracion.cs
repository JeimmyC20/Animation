using UnityEngine;

public class respiracion : MonoBehaviour

{
    public float moveSpeed = 5f;     // Velocidad de movimiento
    public float rotationSpeed = 700f; // Velocidad de rotación

    private CharacterController characterController;

    void Start()
    {
        // Obtener el CharacterController del personaje
        characterController = GetComponent<CharacterController>();

        if (characterController == null)
        {
            Debug.LogError("El personaje necesita un componente CharacterController.");
        }
    }

    void Update()
    {
        // Obtener el input del jugador
        float horizontal = Input.GetAxis("Horizontal"); // A, D o flechas izquierda/derecha
        float vertical = Input.GetAxis("Vertical");     // W, S o flechas arriba/abajo

        // Calcular la dirección del movimiento
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical);

        // Convertir la dirección local a global en relación con la cámara
        moveDirection = Camera.main.transform.TransformDirection(moveDirection);

        // Asegurarse de que el personaje no se mueva en el eje Y
        moveDirection.y = 0;

        // Mover al personaje
        if (moveDirection.magnitude > 0.1f)
        {
            characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

            // Rotar al personaje hacia la dirección del movimiento
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                toRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }
}