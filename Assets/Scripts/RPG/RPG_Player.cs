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


    void Start()
    {
        PlayerRb = GetComponent<Rigidbody2D>();
        PlayerAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        //Movimiento
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveX, moveY);
        PlayerRb.linearVelocity = movement * moveSpeed;

        //Animaciones
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

        //Entrar a Minijuegos
        if (Input.GetKey(KeyCode.KeypadEnter))
        {
            SceneManager.LoadScene("MG01");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MG01"))
        {
            MgText01.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("MG01"))
        {
            MgText01.enabled = false;
        }
    }
}
