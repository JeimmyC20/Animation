using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    [Header("Configuración del objeto")]
    public string objectName = "Fuego"; // Fuego, Agua, Naturaleza, Energía, Éter
    public CinematicTrigger cinematicTrigger; // arrastra el trigger correspondiente
    public Animator objectAnimator;          // animator propio del objeto
    public float activationRadius = 2f;
    public bool isCollected = false;

    // Partículas o efectos opcionales
    [Header("Efectos visuales (opcional)")]
    public ParticleSystem idleEffect;
    public GameObject collectGlow;

    private PlayerController player;
    private bool playerInRange = false;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        if (idleEffect != null) idleEffect.Play();
    }

    void Update()
    {
        if (isCollected || player == null) return;

        float dist = Vector3.Distance(transform.position, player.transform.position);
        playerInRange = dist <= activationRadius;

        // Activar con tecla A (recoger) cuando está cerca
        if (playerInRange && Input.GetKeyDown(KeyCode.A))
        {
            Collect();
        }
    }

    void Collect()
    {
        isCollected = true;

        // 1. Lanzar cinemática
        if (cinematicTrigger != null)
            cinematicTrigger.PlayCinematic();

        // 2. Reproducir animación del objeto
        if (objectAnimator != null)
            objectAnimator.SetTrigger("Activate");

        // 3. Efecto de recogida
        if (collectGlow != null)
            collectGlow.SetActive(true);

        Debug.Log($"[Guardián] Objeto recogido: {objectName}");
    }

    // Visualizar radio en el editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, activationRadius);
    }
}