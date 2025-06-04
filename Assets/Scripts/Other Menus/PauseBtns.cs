using UnityEngine;

public class PauseBtns : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false); // Asegurarse de que el menú de pausa esté desactivado al inicio
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f; // Reanudar el juego
        gameObject.SetActive(false); // Desactivar el menú de pausa
    }

    public void PauseGame()
    {
        Time.timeScale = 0f; // Pausar el juego
        gameObject.SetActive(true); // Activar el menú de pausa
    }

    public void QuitGame()
    {
        Time.timeScale = 1f; // Asegurarse de que el tiempo esté normalizado al salir
        UnityEngine.SceneManagement.SceneManager.LoadScene("mainMenu"); // Cargar la escena del menú principal
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Asegurarse de que el tiempo esté normalizado al reiniciar
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name); // Reiniciar la escena actual
    }
}
