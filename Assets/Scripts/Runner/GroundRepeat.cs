using UnityEngine;

public class GroundRepeat : MonoBehaviour
{
    [Header("Variables")]
    public float speed = 8f;          // Velocidad de movimiento
    private float spriteWidth;

    [Header("Script del otro suelo")]
    public GroundRepeat otherGround; // Referencia al otro suelo
    private BoxCollider2D boxCollider2D;

    void Start()
    {
        // Inicializar componentes y calcular el ancho del sprite
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteWidth = boxCollider2D.size.x;
    }

    void Update()
    {
        // Mover el suelo hacia la izquierda
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        // Calcular los límites de la cámara
        float cameraLeftEdge = Camera.main.transform.position.x -
                              (Camera.main.orthographicSize * Camera.main.aspect);

        // Posición del borde derecho del sprite actual
        float groundRightEdge = transform.position.x + (spriteWidth / 2);

        // Verificar si el sprite ha salido de la pantalla
        if (groundRightEdge <= cameraLeftEdge)
        {
            // Calcular nueva posición al final del otro suelo
            float newX = otherGround.transform.position.x +
                        (otherGround.spriteWidth / 2) +
                        (spriteWidth / 2);

            // Reposicionar el sprite
            transform.position = new Vector2(newX, transform.position.y);
        }
    }
}
