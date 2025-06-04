using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Asegúrate de tener esta línea para usar UI Text

public class Plataformer_Player : MonoBehaviour
{
    private Rigidbody2D PlayerRb;

    [Header("Movimiento")]

    private float movimientoHorizontal = 0f;

    [SerializeField] private float velocidadMovimiento = 0f;
    [Range(0.0f ,0.3f)][SerializeField] private float suavizadoMovimiento;

    private Vector3 velocidad = Vector3.zero;
    private bool mirandoDerecha = true;

    [Header("Salto")]

    [SerializeField] private float fuerzaSalto = 0f;
    [SerializeField] private LayerMask mascaraSuelo;
    public Transform posicionSuelo;
    [SerializeField] private Vector3 dimensionesCaja;
    [SerializeField] private bool enSuelo;

    [Header("Checkpoints")]
    [SerializeField] private Transform[] checkpoint; // Posición del checkpoint

    [Header("DialogoFinal")]
    [SerializeField] private Text dialogoFinal; // Referencia al objeto de diálogo final

    private bool salto = false;
    private int checkpointIndex = 1; // Índice del checkpoint actual, se puede usar para lógica adicional si es necesario
    private bool dialogoFlag = false;

    private void Start()
    {
        // ---------- Obtener componentes ----------
        PlayerRb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movimientoHorizontal = Input.GetAxis("Horizontal") * velocidadMovimiento;

        if(Input.GetButtonDown("Jump"))
        {
            salto = true;
        }   

        if(dialogoFlag)
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                SceneManager.LoadScene("RPG"); // Cargar la escena del menú principal al presionar Enter
            }
        }
    }

    private void FixedUpdate()
    {
        enSuelo = Physics2D.OverlapBox(posicionSuelo.position, dimensionesCaja, 0f, mascaraSuelo);

        Mover(movimientoHorizontal * Time.fixedDeltaTime, salto);

        salto = false;
    }

    void Mover(float mover, bool saltar)
    {
        Vector3 velocidadObjetivo = new Vector2(mover, PlayerRb.linearVelocity.y);
        PlayerRb.linearVelocity = Vector3.SmoothDamp(PlayerRb.linearVelocity, velocidadObjetivo, ref velocidad, suavizadoMovimiento);

        if(mover > 0 && !mirandoDerecha)
        {
            Girar();
        }
        else if (mover < 0 && mirandoDerecha)
        {
            Girar();
        }

        if(enSuelo && saltar)
        {
            enSuelo = false;
            PlayerRb.AddForce(new Vector2(0f, fuerzaSalto));
        }
    }

    void Girar()
    {
        mirandoDerecha = !mirandoDerecha;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(posicionSuelo.position, dimensionesCaja);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("WallNoJump"))
        {
            salto = false; // Evita que el jugador salte al tocar el muro
        }

        //Checkpoints

        if (collision.CompareTag("Damage") && checkpointIndex == 1)
        {
            salto = false; // Evita que el jugador salte al tocar el daño
            //Teletransportar al jugador a un checkpoint
            transform.position = checkpoint[0].position; // Teletransportar al jugador a la posición del checkpoint
        }
        if (collision.CompareTag("Damage") && checkpointIndex == 2)
        {
            salto = false; // Evita que el jugador salte al tocar el daño
            //Teletransportar al jugador a un checkpoint
            transform.position = checkpoint[1].position; // Teletransportar al jugador a la posición del checkpoint
        }
        if (collision.CompareTag("Damage") && checkpointIndex == 3)
        {
            salto = false; // Evita que el jugador salte al tocar el daño
            //Teletransportar al jugador a un checkpoint
            transform.position = checkpoint[2].position; // Teletransportar al jugador a la posición del checkpoint
        }

        // Checkpoint logic

        if (collision.CompareTag("CheckpointChange"))
        {
            // Incrementar el índice del checkpoint
            checkpointIndex = 2;
        }
        if (collision.CompareTag("CheckpointChange01"))
        {
            // Incrementar el índice del checkpoint
            checkpointIndex = 3;
        }

        // Meta

        if(collision.CompareTag("MetaPlataformer"))
        {
            dialogoFinal.enabled = true; // Habilitar el diálogo final
            dialogoFlag = true; // Activar la bandera de diálogo para permitir la interacción
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("MetaPlataformer"))
        {
            dialogoFinal.enabled = false; // Deshabilitar el diálogo final al salir de la meta
            dialogoFlag = false; // Desactivar la bandera de diálogo
        }
    }
}
