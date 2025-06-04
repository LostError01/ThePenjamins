using UnityEngine;
using UnityEngine.UI; // Asegúrate de tener esta línea para usar UI Text

public class CombatManager : MonoBehaviour
{
    // Vidas públicas
    public int playerHealth = 100;
    public int enemyHealth = 5000;

    // Rango de daño del ataque
    public int minAttackDamage = 10;
    public int maxAttackDamage = 50;

    //Rango de da del ataque enemigo
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
        // Verificar si el jugador o el enemigo ha ganado
        if (playerHealth <= 0 || enemyHealth <= 0)
        {
            CheckVictory();
        }
    }

    // --- Turnos ---

    void StartPlayerTurn()
    {
        Debug.Log("Turno del jugador");
        currentTurn = TurnState.PlayerTurn;
        waitingForPlayerAction = true;
    }

    void StartEnemyTurn()
    {
        Debug.Log("Turno del enemigo");
        currentTurn = TurnState.EnemyTurn;

        // Decidir acción aleatoria del enemigo
        if (Random.Range(0, 2) == 0)
        {
            EnemyAttack();
        }
        else
        {
            EnemyDefend();
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
            PlayerAttack();
            waitingForPlayerAction = false;
            StartEnemyTurn();
        }
    }

    public void OnPlayerDefendButton()
    {
        if (currentTurn == TurnState.PlayerTurn && waitingForPlayerAction)
        {
            PlayerDefend();
            waitingForPlayerAction = false;
            StartEnemyTurn();
        }
    }

    // --- Ataques y Defensas ---

    void PlayerAttack()
    {
        int damage = Random.Range(minAttackDamage, maxAttackDamage + 1);
        int effectiveDamage = isEnemyDefending ? damage / 2 : damage;

        enemyHealth -= effectiveDamage;
        enemyHealth = Mathf.Max(enemyHealth, 0);

        Debug.Log($"Jugador ataca al enemigo con {damage} de daño. El enemigo recibe {effectiveDamage} puntos de daño.");
        CheckVictory();
    }

    void PlayerDefend()
    {
        isPlayerDefending = true;
        Debug.Log("Jugador se defiende. Reducirá el daño recibido a la mitad este turno.");
    }

    void EnemyAttack()
    {
        int damage = Random.Range(enemyMinAttackDamage, enemyMaxAttackDamage + 1);
        int effectiveDamage = isPlayerDefending ? damage / 2 : damage;

        playerHealth -= effectiveDamage;
        playerHealth = Mathf.Max(playerHealth, 0);

        Debug.Log($"Enemigo ataca al jugador con {damage} de daño. El jugador recibe {effectiveDamage} puntos de daño.");
        CheckVictory();
    }

    void EnemyDefend()
    {
        isEnemyDefending = true;
        Debug.Log("Enemigo se defiende. Reducirá el daño recibido a la mitad este turno.");
    }

    // --- Verificar victoria ---
    void CheckVictory()
    {
        if (playerHealth <= 0)
        {
            Debug.Log("El enemigo ha ganado!");
            Time.timeScale = 0; // Detiene el juego
        }
        else if (enemyHealth <= 0)
        {
            Debug.Log("El jugador ha ganado!");
            Time.timeScale = 0; // Detiene el juego
        }
    }
}
