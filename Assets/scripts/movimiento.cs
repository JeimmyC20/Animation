using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementWithAnimations : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float rotationSpeed = 700f;

    public Transform cameraTransform;

    private CharacterController characterController;
    private Animator animator;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        // ===== MOVIMIENTO =====
        float horizontal =
            (Keyboard.current.dKey.isPressed ? 1 : 0) -
            (Keyboard.current.aKey.isPressed ? 1 : 0);

        float vertical =
            (Keyboard.current.wKey.isPressed ? 1 : 0) -
            (Keyboard.current.sKey.isPressed ? 1 : 0);

        Vector3 moveDirection =
            transform.forward * vertical +
            transform.right * horizontal;

        bool isRunning =
            Keyboard.current.leftShiftKey.isPressed;

        float currentSpeed =
            isRunning ? runSpeed : walkSpeed;

        if (moveDirection.magnitude > 0.1f)
        {
            characterController.Move(
                moveDirection.normalized *
                currentSpeed *
                Time.deltaTime
            );

            Quaternion targetRotation =
                Quaternion.LookRotation(moveDirection);

            transform.rotation =
                Quaternion.RotateTowards(
                    transform.rotation,
                    targetRotation,
                    rotationSpeed * Time.deltaTime
                );

            animator.SetBool("isWalking", true);
            animator.SetBool("isRunning", isRunning);
        }
        else
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
        }

        // ===== CAMARA ATRAS DEL PERSONAJE =====
        cameraTransform.position =
            transform.position +
            transform.TransformDirection(
                new Vector3(0, 0f, -0.5f)
            );

        cameraTransform.LookAt(
            transform.position + Vector3.up * 0f
        );
    }
}