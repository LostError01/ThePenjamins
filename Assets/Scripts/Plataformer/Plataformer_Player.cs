using UnityEngine;

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

    private bool salto = false;

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
}
