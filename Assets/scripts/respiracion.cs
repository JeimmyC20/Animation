using UnityEngine;

public class respiracion : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 700f;
    private CharacterController characterController;
    private Animator anim;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontal, 0, vertical);
        moveDirection = Camera.main.transform.TransformDirection(moveDirection);
        moveDirection.y = 0;

        bool moving = moveDirection.magnitude > 0.1f;

        // --- CAMINAR ---
        anim.SetBool("caminado", moving);

        if (moving)
        {
            characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, toRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        // --- SALTAR (Espacio) ---
        if (Input.GetKeyDown(KeyCode.Space))
            anim.SetTrigger("salto");

        // --- POSE GUARDIAN (S mantenido) ---
        if (Input.GetKeyDown(KeyCode.P))
            anim.SetTrigger("pose");

        // --- RECOGER OBJETO (A) ---
        if (Input.GetKeyDown(KeyCode.O))
            anim.SetTrigger("recoger");
    }
}