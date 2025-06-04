using UnityEngine;
using UnityEngine.UI; // Asegúrate de tener esta línea para usar UI Text
using System.Collections; // Necesario para usar corutinas
using UnityEngine.SceneManagement; // Para manejar escenas si es necesario

public class CombatManager : MonoBehaviour
{
    // Vidas públicas
    public int playerHealth = 100;
    public int enemyHealth = 5000;

    // Rango de daño del ataque
    public int minAttackDamage = 10;
    public int maxAttackDamage = 50;

    //Rango de daño del ataque enemigo
    public int enemyMinAttackDamage = 50;
    public int enemyMaxAttackDamage = 100;

    // Estados de defensa
    private bool isPlayerDefending = false;
    private bool isEnemyDefending = false;

    // Estado del combate
    private enum TurnState { PlayerTurn, EnemyTurn }
    private TurnState currentTurn = TurnState.PlayerTurn;

    [Header("Animaciones")]
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Animator enemyAnimator;

    [Header("Barras de vida")]
    [SerializeField] private Slider playerHealthBar;
    [SerializeField] private Slider enemyHealthBar;

    private bool waitingForPlayerAction = false;

    void Start()
    {
        Debug.Log("Combate iniciado!");
        StartPlayerTurn();
    }

    private void Update()
    {
        // Actualizar barras de vida
        playerHealthBar.value = playerHealth;
        enemyHealthBar.value = enemyHealth;

        if (playerHealth <= 0)
        {
            
            SceneManager.LoadScene("GameOver"); // Cargar escena de Game Over
        }
    }

    // --- Turnos ---

    void StartPlayerTurn()
    {
        Debug.Log("Turno del jugador");
        currentTurn = TurnState.PlayerTurn;
        waitingForPlayerAction = true;
    }

    IEnumerator EnemyTurnWithDelay()
    {
        yield return new WaitForSeconds(0.5f); // Breve pausa antes de acción enemiga

        if (Random.Range(0, 2) == 0)
        {
            yield return StartCoroutine(EnemyAttackWithAnimation());
        }
        else
        {
            yield return StartCoroutine(EnemyDefendWithAnimation());
        }

        EndTurn();
    }

    void EndTurn()
    {
        // Limpiar estados de defensa
        isPlayerDefending = false;
        isEnemyDefending = false;

        // Comenzar nuevo turno si nadie ha ganado aún
        if (playerHealth > 0 && enemyHealth > 0)
        {
            Invoke(nameof(StartPlayerTurn), 1f); // Pequeña pausa antes del próximo turno
        }
    }

    // --- Acciones del Jugador (llamadas desde botones UI) ---

    public void OnPlayerAttackButton()
    {
        if (currentTurn == TurnState.PlayerTurn && waitingForPlayerAction)
        {
            StartCoroutine(PlayerAttackWithAnimation());
        }
    }

    public void OnPlayerDefendButton()
    {
        if (currentTurn == TurnState.PlayerTurn && waitingForPlayerAction)
        {
            StartCoroutine(PlayerDefendWithAnimation());
        }
    }

    // --- Ataques y Defensas con retraso ---

    IEnumerator PlayerAttackWithAnimation()
    {
        waitingForPlayerAction = false;
        playerAnimator.SetTrigger("Attack"); // Activar animación de ataque

        float animationDuration = 0.5f; // Ajusta esto según la duración real de tu animación de ataque
        yield return new WaitForSeconds(animationDuration); // Esperar a que termine la animación

        // Calcular y aplicar daño
        int damage = Random.Range(minAttackDamage, maxAttackDamage + 1);
        int effectiveDamage = isEnemyDefending ? damage / 2 : damage;

        enemyHealth -= effectiveDamage;
        enemyHealth = Mathf.Max(enemyHealth, 0);

        Debug.Log($"Jugador ataca al enemigo con {damage} de daño. El enemigo recibe {effectiveDamage} puntos de daño.");
       

        // Completar los 1 segundos totales si la animación fue más corta
        float remainingTime = 1.0f - animationDuration;
        if (remainingTime > 0) yield return new WaitForSeconds(remainingTime);

        StartCoroutine(EnemyTurnWithDelay());
    }

    IEnumerator PlayerDefendWithAnimation()
    {
        waitingForPlayerAction = false;
        playerAnimator.SetTrigger("Defense"); // Activar animación de defensa

        float animationDuration = 0.5f; // Ajusta según la duración de tu animación de defensa
        yield return new WaitForSeconds(animationDuration);

        isPlayerDefending = true;
        Debug.Log("Jugador se defiende. Reducirá el daño recibido a la mitad este turno.");

        // Completar los 1 segundos totales si la animación fue más corta
        float remainingTime = 1.0f - animationDuration;
        if (remainingTime > 0) yield return new WaitForSeconds(remainingTime);

        StartCoroutine(EnemyTurnWithDelay());
    }

    IEnumerator EnemyAttackWithAnimation()
    {
        enemyAnimator.SetTrigger("GodAttack");

        float animationDuration = 0.5f;
        yield return new WaitForSeconds(animationDuration);

        int damage = Random.Range(enemyMinAttackDamage, enemyMaxAttackDamage + 1);
        int effectiveDamage = isPlayerDefending ? damage / 2 : damage;

        playerHealth -= effectiveDamage;
        playerHealth = Mathf.Max(playerHealth, 0);

        Debug.Log($"Enemigo ataca al jugador con {damage} de daño. El jugador recibe {effectiveDamage} puntos de daño.");

        float remainingTime = 1.0f - animationDuration;
        if (remainingTime > 0) yield return new WaitForSeconds(remainingTime);
    }

    IEnumerator EnemyDefendWithAnimation()
    {
        enemyAnimator.SetTrigger("GodDefense");

        float animationDuration = 0.5f;
        yield return new WaitForSeconds(animationDuration);

        isEnemyDefending = true;
        Debug.Log("Enemigo se defiende. Reducirá el daño recibido a la mitad este turno.");

        float remainingTime = 1.0f - animationDuration;
        if (remainingTime > 0) yield return new WaitForSeconds(remainingTime);
    }
}
