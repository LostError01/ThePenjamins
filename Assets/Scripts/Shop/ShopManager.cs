using UnityEngine;
using UnityEngine.UI; // Asegúrate de tener esta línea para usar UI Text
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    [System.Serializable]
    public class DraggableObject
    {
        public GameObject item;
        public Transform slot;
        public Vector3 originalPosition;
    }

    public static class ShopData
    {
        public static bool[] boughtItems = new bool[9]; // Ajusta el tamaño según tus draggables
    }

    public DraggableObject[] draggables = new DraggableObject[9];

    private GameObject draggingObject = null;
    private Vector3 offset;
    private bool isDragging = false;

    [Header("Dinero")]
    [SerializeField] private Text moneyText;
    private int playerMoney = 0;

    void Start()
    {
        playerMoney = RPG_Player.puntos;

        for (int i = 0; i < draggables.Length; i++)
        {
            if (draggables[i].item != null)
            {
                draggables[i].originalPosition = draggables[i].item.transform.position;

                // Si ya fue comprado, lo colocamos directamente en el slot
                if (ShopData.boughtItems[i])
                {
                    draggables[i].item.transform.position = draggables[i].slot.position;
                    draggables[i].originalPosition = draggables[i].slot.position;
                }
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (playerMoney < 5)
            {
                Debug.Log("No tienes suficiente dinero para arrastrar objetos.");
                return;
            }

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                for (int i = 0; i < draggables.Length; i++)
                {
                    if (draggables[i].item == hit.collider.gameObject)
                    {
                        // Evitar arrastrar objetos ya comprados
                        if (ShopData.boughtItems[i])
                        {
                            draggingObject = null;
                            isDragging = false;
                            break;
                        }

                        draggingObject = draggables[i].item;
                        offset = draggingObject.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        isDragging = true;
                        break;
                    }
                }
            }
        }

        if (isDragging && draggingObject != null)
        {
            draggingObject.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            bool placedCorrectly = false;

            for (int i = 0; i < draggables.Length; i++)
            {
                if (draggables[i].item == draggingObject)
                {
                    Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Collider2D slotCollider = draggables[i].slot.GetComponent<Collider2D>();

                    if (slotCollider != null && slotCollider.OverlapPoint(mousePos))
                    {
                        draggingObject.transform.position = draggables[i].slot.position;
                        placedCorrectly = true;

                        playerMoney -= 5; // Descontar dinero al comprar
                        RPG_Player.puntos = playerMoney;
                        ShopData.boughtItems[i] = true; // Marcar como comprado
                    }

                    if (!placedCorrectly)
                    {
                        draggingObject.transform.position = draggables[i].originalPosition;
                    }

                    break;
                }
            }

            isDragging = false;
            draggingObject = null;
        }

        if (moneyText != null)
        {
            moneyText.text = playerMoney.ToString();
        }
    }

    public void Regresar()
    {
        SceneManager.LoadScene("RPG");
    }
}
