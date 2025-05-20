using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RPG_Player : MonoBehaviour
{
    //Variables para el movimiento
    public float moveSpeed = 5f;

    //Variables del objeto
    Rigidbody2D PlayerRb;
    Animator PlayerAnim;

    //Objetos externos
    public Text MgText01;

    //Flags de minijuegos
    private bool MG01 = false, MG02 = false, MG03 = false;


    void Start()
    {
        // ---------- Obtener componentes ----------

        PlayerRb = GetComponent<Rigidbody2D>();
        PlayerAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        // ---------- Movimiento del jugador ----------

        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveX, moveY);
        PlayerRb.linearVelocity = movement * moveSpeed;

        // ---------- Animaciones del jugador ----------

        if (movement != Vector2.zero)
        {
            PlayerAnim.SetBool("isMoving", true);
            if(moveX > 0)
            {
                PlayerAnim.SetBool("Right", true);
                PlayerAnim.SetBool("Left", false);
            }
            if (moveX < 0)
            {
                PlayerAnim.SetBool("Right", false);
                PlayerAnim.SetBool("Left", true);
            }
        }
        else
        {
            PlayerAnim.SetBool("isMoving", false);
        }

        // ---------- Presion de tecla en minijuegos -----------

        if (MG01)
        {
            if (Input.GetKey(KeyCode.KeypadEnter))
            {
                SceneManager.LoadScene("MG01");
            }
        }

        if (MG02)
        {
            if (Input.GetKey(KeyCode.KeypadEnter))
            {
                SceneManager.LoadScene("MG02");
            }
        }

        if (MG03)
        {
            if (Input.GetKey(KeyCode.KeypadEnter))
            {
                SceneManager.LoadScene("MG03");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // --------- Entrar a Minijuegos ---------

        if (collision.CompareTag("MG01"))
        {
            MgText01.enabled = true;
            MG01 = true;
        }
        if (collision.CompareTag("MG02"))
        {
            MgText01.enabled = true;
            MG02 = true;
        }
        if (collision.CompareTag("MG03"))
        {
            MgText01.enabled = true;
            MG03 = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // --------- Salir de Minijuegos ---------

        if (collision.CompareTag("MG01"))
        {
            MgText01.enabled = false;
            MG01 = false;
        }
        if (collision.CompareTag("MG02"))
        {
            MgText01.enabled = false;
            MG02 = false;
        }
        if (collision.CompareTag("MG03"))
        {
            MgText01.enabled = false;
            MG03 = false;
        }
    }
}
