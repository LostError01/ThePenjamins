using UnityEngine;

public class GroundRepeat : MonoBehaviour
{
    [Header("Variables")]
    private float anchoSprite;

    private void Start()
    {
        // ---------- Obtener el ancho del sprite ----------
        BoxCollider2D groundCollider = GetComponent<BoxCollider2D>();
        anchoSprite = groundCollider.size.x;
    }

    private void Update()
    {
        // ---------- Repetir el fondo ----------
        if (transform.position.x < -anchoSprite)
        {
            transform.Translate(Vector2.right * 2f * anchoSprite);
        }
    }
}
