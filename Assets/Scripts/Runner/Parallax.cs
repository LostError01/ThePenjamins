using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private float velocidadParallax = 8f;

    void Update()
    {
        // ---------- Mover el objeto de fondo ----------
        transform.Translate(Vector2.left * velocidadParallax * Time.deltaTime);
    }
}
