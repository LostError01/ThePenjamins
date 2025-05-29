using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Header("Objeto 1")]
    // Referencias públicas a los dos sprites del suelo
    [SerializeField] private SpriteRenderer obj1;
    [Header("Objeto 2")]
    [SerializeField] private SpriteRenderer obj2;

    [Header("Velocidad")]
    // Velocidad de desplazamiento
    [SerializeField] private float speed;

    private float spriteWidth; // Ancho de cada sprite de suelo

    void Start()
    {
        // Calcular el ancho del sprite (asumiendo que ambos son iguales)
        spriteWidth = obj1.sprite.bounds.size.x;

        // Posicionar inicialmente los suelos uno detrás del otro
        if (obj1 && obj2)
        {
            // Posición inicial del segundo suelo
            obj2.transform.position = new Vector2(
                obj1.transform.position.x + spriteWidth,
                obj2.transform.position.y);
        }
    }

    void Update()
    {
        // Mover ambos suelos hacia la izquierda
        MoveGround(obj1);
        MoveGround(obj2);
    }

    void MoveGround(SpriteRenderer ground)
    {
        // Actualizar posición
        ground.transform.Translate(Vector2.left * speed * Time.deltaTime);

        // Verificar si el suelo ha salido completamente de la pantalla
        if (ground.transform.position.x + spriteWidth / 2 < Camera.main.transform.position.x
            - Camera.main.orthographicSize * Camera.main.aspect)
        {
            // Reposicionar al frente del otro suelo
            ground.transform.position = new Vector2(
                ground.transform.position.x + 2 * spriteWidth,
                ground.transform.position.y
            );
        }
    }
}
