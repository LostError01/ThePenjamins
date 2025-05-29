using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class GroundRepeat : MonoBehaviour
{
    [Header("Suelos")]
    // Referencias públicas a los dos sprites del suelo
    [SerializeField] private SpriteRenderer ground1;
    [SerializeField] private SpriteRenderer ground2;

    [Header("Velocidad")]
    // Velocidad de desplazamiento
    [SerializeField] private float speed;

    private float groundWidth; // Ancho de cada sprite de suelo

    void Start()
    {
        // Calcular el ancho del sprite (asumiendo que ambos son iguales)
        groundWidth = ground1.sprite.bounds.size.x;

        // Posicionar inicialmente los suelos uno detrás del otro
        if (ground1 && ground2)
        {
            // Posición inicial del segundo suelo
            ground2.transform.position = new Vector2(
                ground1.transform.position.x + groundWidth,
                ground2.transform.position.y);
        }
    }

    void Update()
    {
        // Mover ambos suelos hacia la izquierda
        MoveGround(ground1);
        MoveGround(ground2);
    }

    void MoveGround(SpriteRenderer ground)
    {
        // Actualizar posición
        ground.transform.Translate(Vector2.left * speed * Time.deltaTime);

        // Verificar si el suelo ha salido completamente de la pantalla
        if (ground.transform.position.x + groundWidth / 2 < Camera.main.transform.position.x
            - Camera.main.orthographicSize * Camera.main.aspect)
        {
            // Reposicionar al frente del otro suelo
            ground.transform.position = new Vector2(
                ground.transform.position.x + 2 * groundWidth,
                ground.transform.position.y
            );
        }
    }
}

