using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class RPG_Player : MonoBehaviour
{
    [Header("Velocidad de movimiento")]
    //Variables para el movimiento
    [SerializeField] private float moveSpeed;

    //Variables del objeto
    private Rigidbody2D PlayerRb;
    private Animator PlayerAnim;

    [Header("Elementos de UI")]
    public Text MgText01;

    [Header("Ishi")]
    [SerializeField] private RawImage IshiDialog;
    [SerializeField] private SpriteRenderer IshiImg;

    [Header("Ambar")]
    [SerializeField] private RawImage AmbarDialog;
    [SerializeField] private SpriteRenderer AmbarImg;

    [Header("Abril")]
    [SerializeField] private RawImage AbrilDialog;
    [SerializeField] private SpriteRenderer AbrilImg;

    [Header("Tamara")]
    [SerializeField] private RawImage TamDialog;
    [SerializeField] private SpriteRenderer TamImg;

    [Header("Natalia")]
    [SerializeField] private RawImage NataliaDialog;
    [SerializeField] private SpriteRenderer NataliaImg;

    [Header("Seba")]
    [SerializeField] private RawImage SebaDialog;
    [SerializeField] private SpriteRenderer SebaImg;

    [Header("Bmoreno")]
    [SerializeField] private RawImage BmorenoDialog;
    [SerializeField] private Text BmorenoText;
    [SerializeField] private SpriteRenderer BmorenoImg;
    [SerializeField] private Text Merkatext;

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
                SceneManager.LoadScene("MG01_NORMAL");
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

        // --------- Entrar a Dialogos ---------

        if (collision.CompareTag("Ishi_Dialog"))
        {
            IshiDialog.enabled = true;
            IshiImg.enabled = true;
        }
        if (collision.CompareTag("Ambar_Dialog"))
        {
            AmbarDialog.enabled = true;
            AmbarImg.enabled = true;
        }
        if (collision.CompareTag("Abril_Dialog"))
        {
            AbrilDialog.enabled = true;
            AbrilImg.enabled = true;
        }
        if (collision.CompareTag("Tam_Dialog"))
        {
            TamDialog.enabled = true;
            TamImg.enabled = true;
        }
        if (collision.CompareTag("Natalia_Dialog"))
        {
            NataliaDialog.enabled = true;
            NataliaImg.enabled = true;
        }
        if (collision.CompareTag("Seba_Dialog"))
        {
            SebaDialog.enabled = true;
            SebaImg.enabled = true;
        }
        if (collision.CompareTag("Dealer_Dialog"))
        {
            BmorenoDialog.enabled = true;
            BmorenoImg.enabled = true;
            BmorenoText.enabled = true;
            Merkatext.enabled = true;
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

        // --------- Salir de Dialogos ---------

        if (collision.CompareTag("Ishi_Dialog"))
        {
            IshiDialog.enabled = false;
            IshiImg.enabled = false;
        }
        if (collision.CompareTag("Ambar_Dialog"))
        {
            AmbarDialog.enabled = false;
            AmbarImg.enabled = false;
        }
        if (collision.CompareTag("Abril_Dialog"))
        {
            AbrilDialog.enabled = false;
            AbrilImg.enabled = false;
        }
        if (collision.CompareTag("Tam_Dialog"))
        {
            TamDialog.enabled = false;
            TamImg.enabled = false;
        }
        if (collision.CompareTag("Natalia_Dialog"))
        {
            NataliaDialog.enabled = false;
            NataliaImg.enabled = false;
        }
        if (collision.CompareTag("Seba_Dialog"))
        {
            SebaDialog.enabled = false;
            SebaImg.enabled = false;
        }
        if (collision.CompareTag("Dealer_Dialog"))
        {
            BmorenoDialog.enabled = false;
            BmorenoImg.enabled = false;
            BmorenoText.enabled = false;
            Merkatext.enabled = false;
        }
    }
}
