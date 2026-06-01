using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ── Referencias ──────────────────────────────────────────────
    private Animator animator;
    private CharacterController controller;

    // ── Parámetros de movimiento ──────────────────────────────────
    [Header("Movimiento")]
    public float walkSpeed = 3f;
    public float jumpForce = 5f;
    public float gravity = -9.81f;

    // ── Estado interno ────────────────────────────────────────────
    private Vector3 velocity;
    private bool isGrounded;
    public bool canMove = true; // false durante cinemáticas

    // ── Nombres de parámetros del Animator ───────────────────────
    // IMPORTANTE: deben coincidir exactamente con los parámetros
    // en tu Animator Controller de Unity
    private const string ANIM_IDLE = "Idle";
    private const string ANIM_BREATHE = "respirar";
    private const string ANIM_WALK = "caminar";
    private const string ANIM_JUMP = "salto";
    private const string ANIM_PICKUP = "recoger";
    private const string ANIM_GUARDIAN = "Guardian";

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!canMove) return; // bloquear input en cinemáticas

        HandleGravity();
        HandleAnimations();
        HandleMovement();
    }

    // ─────────────────────────────────────────────────────────────
    void HandleAnimations()
    {
        // Limpiar todos los estados primero
        SetAllFalse();

        // W → Respirar (idle con respiración)
        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool(ANIM_BREATHE, true);
            return;
        }

        // D → Caminar
        if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool(ANIM_WALK, true);
            return;
        }

        // A → Recoger objeto (una sola vez por pulsación)
        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.SetTrigger(ANIM_PICKUP);
            return;
        }

        // S → Postura de Guardián
        if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool(ANIM_GUARDIAN, true);
            return;
        }

        // Espacio → Saltar (solo si está en el suelo)
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            animator.SetTrigger(ANIM_JUMP);
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            return;
        }

        // Sin tecla → Idle base
        animator.SetBool(ANIM_IDLE, true);
    }

    void HandleMovement()
    {
        // Solo mover horizontalmente cuando D está presionado
        if (Input.GetKey(KeyCode.D))
        {
            Vector3 move = transform.right * walkSpeed;
            controller.Move(move * Time.deltaTime);
        }
    }

    void HandleGravity()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0f)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void SetAllFalse()
    {
        animator.SetBool(ANIM_IDLE, false);
        animator.SetBool(ANIM_BREATHE, false);
        animator.SetBool(ANIM_WALK, false);
        animator.SetBool(ANIM_GUARDIAN, false);
        // Los Triggers se limpian solos; no se tocan aquí
    }

    // Llamado externamente por CinematicTrigger
    public void SetCanMove(bool value) => canMove = value;

    // Llamado al final del juego (todos los objetos recogidos)
    public void PlayGuardianFinal()
    {
        canMove = false;
        SetAllFalse();
        animator.SetTrigger(ANIM_GUARDIAN);
    }
}