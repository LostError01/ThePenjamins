using UnityEngine;

public class Runner_Player : MonoBehaviour
{
    [Header("Componentes")]
    private Rigidbody2D PlayerRb;
    private Animator PlayerAnim;

    [Header("Variables")]
    [SerializeField] private float fuerzaSalto;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask mascaraSuelo;
    [SerializeField] private float radio;

    private void Start()
    {
        // ---------- Obtener componentes ----------
        PlayerRb = GetComponent<Rigidbody2D>();
        PlayerAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, radio, mascaraSuelo);

        // ---------- Comprobar si el jugador ha saltado ----------
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                PlayerRb.AddForce(Vector2.up * fuerzaSalto);
                PlayerAnim.SetTrigger("Jump");
                PlayerAnim.SetBool("Running", false);
            }
        }

        if (isGrounded)
        {
            PlayerAnim.SetBool("Running", true);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, radio);
    }
}
