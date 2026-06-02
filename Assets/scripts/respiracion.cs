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
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        bool moving = Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f;

        anim.SetBool("caminado", moving);
        Debug.Log("moving=" + moving + " | GetBool=" + anim.GetBool("caminado"));

        if (moving)
        {
            Vector3 moveDirection = new Vector3(horizontal, 0, vertical).normalized;
            characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                toRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        if (Input.GetKeyDown(KeyCode.Space))
            anim.SetTrigger("salto");

        if (Input.GetKeyDown(KeyCode.Q))
            anim.SetTrigger("pose");

        if (Input.GetKeyDown(KeyCode.E))
            anim.SetTrigger("recoger");
    }
}