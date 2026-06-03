using UnityEngine;
using System.Collections;

public class respiracion : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 700f;

    private CharacterController characterController;
    private Animator anim;
    private ReproducirVideo videoObjeto;

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
        {
            anim.SetTrigger("recoger");
            videoObjeto = FindAnyObjectByType<ReproducirVideo>();
            if (videoObjeto != null)
                StartCoroutine(EsperarYReproducir());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        ReproducirVideo rv = other.GetComponent<ReproducirVideo>();
        if (rv != null)
            videoObjeto = rv;
    }

    void OnTriggerExit(Collider other)
    {
        ReproducirVideo rv = other.GetComponent<ReproducirVideo>();
        if (rv != null)
            videoObjeto = null;
    }

    IEnumerator EsperarYReproducir()
    {
        yield return new WaitForSeconds(0.1f);
        float duracion = anim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(duracion);
        if (videoObjeto != null)
            videoObjeto.LanzarVideo();
    }
}