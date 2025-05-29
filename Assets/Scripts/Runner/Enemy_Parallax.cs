using UnityEngine;

public class Enemy_Parallax : MonoBehaviour
{
    private float speed; // Velocidad de movimiento del parallax


    private void Start()
    {
        speed = Runner_EnemySpawn.velocidadEnemigo01;
    }
    private void Update()
    {
        // Mueve el objeto hacia la izquierda a una velocidad constante
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
