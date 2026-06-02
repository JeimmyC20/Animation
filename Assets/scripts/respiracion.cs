using UnityEngine;

public class respiracion : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 700f;

    private CharacterController characterController;
    private Animator anim;  // ← ESTO faltaba

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();  // ← ESTO faltaba
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontal, 0, vertical);
        moveDirection = Camera.main.transform.TransformDirection(moveDirection);
        moveDirection.y = 0;  // correcto, sin movimiento en Y

        bool moving = moveDirection.magnitude > 0.1f;

        anim.SetBool("caminar", moving);  // ← ESTO faltaba

        if (moving)
        {
            characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                toRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        // Salto
        if (Input.GetKeyDown(KeyCode.Space))
            anim.SetTrigger("saltar");

        // Pose guardián (S mantenido)
        anim.SetBool("pose", Input.GetKey(KeyCode.S));
    }
}