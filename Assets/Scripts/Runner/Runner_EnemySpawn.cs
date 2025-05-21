using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class Runner_EnemySpawn : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private GameObject[] enemyPrefabs; // Array de prefabs de enemigos

    [Header("Variables Spawn")]
    [SerializeField] private float minTime;
    [SerializeField] private float maxTime;
    [SerializeField] private float AlturaEnemigo01;
    [SerializeField] private float AlturaEnemigo02;

    private void Start()
    {
        StartCoroutine(Spawn()); // Inicia la corrutina de spawn
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            int RandomIndex = Random.Range(0, enemyPrefabs.Length); // Genera un índice aleatorio

            Vector3 EnemyTransform = Vector3.zero;

            if (RandomIndex == 0)
            {
                EnemyTransform = new Vector3(transform.position.x, transform.position.y + AlturaEnemigo01); ;
            }
            if (RandomIndex == 1)
            {
                EnemyTransform = new Vector3(transform.position.x, transform.position.y + AlturaEnemigo02);
            }

            float randomTime = Random.Range(minTime, maxTime); // Genera un tiempo aleatorio entre el rango

            Instantiate(enemyPrefabs[RandomIndex], EnemyTransform , Quaternion.identity); // Instancia el enemigo en la posición del spawner
            yield return new WaitForSeconds(randomTime); // Espera 1 segundo antes de volver a generar
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
            Destroy(collision.gameObject); // Destruye el objeto al entrar en el trigger
    }

}
