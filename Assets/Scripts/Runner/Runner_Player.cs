using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Asegúrate de tener esta línea para usar UI Text

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

    [Header("Escena Actual")]
    [SerializeField] private string escenaActual;

    [Header("Barra de Progreso")]
    [SerializeField] private Slider barraProgreso; // Barra de progreso para el minijuego

    [Header("Velocidad de Progreso en Barra")]
    [SerializeField] private float velocidadProgreso; // Velocidad de incremento de la barra de progreso

    [Header("Niño")]
    [SerializeField] AudioSource sonidoNiño; // Sonido al recoger un niño
    [SerializeField] AudioClip matarNiño; // Clip de sonido al recoger un niño

    private int puntaje;

    private void Start()
    {
        // ---------- Obtener componentes ----------
        PlayerRb = GetComponent<Rigidbody2D>();
        PlayerAnim = GetComponent<Animator>();
        Time.timeScale = 1f; // Asegurarse de que el tiempo del juego esté normalizado al inicio
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

        // Cada 5 segundos aumentar la barra de progreso 1 unidad
        if (barraProgreso.value < barraProgreso.maxValue)
        {
            barraProgreso.value += Time.deltaTime * velocidadProgreso; // Ajusta la velocidad de incremento según sea necesario
        }

        //Si llega a la maxima barra de progreso, cargar la escena RPG
        if (barraProgreso.value >= barraProgreso.maxValue)
        {
            if (SceneManager.GetActiveScene().name == "MG02")
            {
                puntaje = UnityEngine.Random.Range(2, 5); // Generar un puntaje aleatorio entre 1 y 10
                RPG_Player.puntos += puntaje; // Incrementar puntos al completar el diálogo
            }
            if (SceneManager.GetActiveScene().name == "MG02_Distorted")
            {
                puntaje = UnityEngine.Random.Range(5, 10); // Generar un puntaje aleatorio entre 1 y 10
                RPG_Player.puntos += puntaje; // Incrementar puntos al completar el diálogo
            }
            SceneManager.LoadScene("RPG"); // Cargar la escena RPG
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, radio);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("R_Enemy"))
        {
            SceneManager.LoadScene(escenaActual);
        }
        if (collision.CompareTag("Kid"))
        {
            //Aumentar el delta time
            Time.timeScale += 0.02f; // Aumenta la velocidad del juego al recoger un niño
            sonidoNiño.PlayOneShot(matarNiño); // Reproducir sonido al recoger un niño
        }
    }
}
