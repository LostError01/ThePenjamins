using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [System.Serializable]
    public class DraggableObject
    {
        public GameObject item;            // El objeto arrastrable
        public Transform slot;             // Su contenedor destino
        public Vector3 originalPosition;   // Posición original
    }

    public DraggableObject[] draggables = new DraggableObject[9];

    private GameObject draggingObject = null;
    private Vector3 offset;
    private bool isDragging = false;

    void Start()
    {
        for (int i = 0; i < draggables.Length; i++)
        {
            if (draggables[i].item != null)
            {
                draggables[i].originalPosition = draggables[i].item.transform.position;
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                for (int i = 0; i < draggables.Length; i++)
                {
                    if (draggables[i].item == hit.collider.gameObject)
                    {
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
    }
}
