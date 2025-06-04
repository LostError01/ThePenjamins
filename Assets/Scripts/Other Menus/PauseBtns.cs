using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseBtns : MonoBehaviour
{
    [SerializeField] GameObject BotonDePausa;
    private void Start()
    {
        gameObject.SetActive(false); // Asegurarse de que el menú de pausa esté desactivado al inicio
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f; // Reanudar el juego
        gameObject.SetActive(false); // Desactivar el menú de pausa
        BotonDePausa.SetActive(true); // Activar el botón de pausa
    }

    public void PauseGame()
    {
        Time.timeScale = 0f; // Pausar el juego
        gameObject.SetActive(true); // Activar el menú de pausa
        BotonDePausa.SetActive(false); // Desactivar el botón de pausa
    }

    public void QuitGame()
    {
        Time.timeScale = 1f; // Asegurarse de que el tiempo esté normalizado al salir
        //Si la escena se llama MG02, se lleva a la escena RPG
        if (SceneManager.GetActiveScene().name != "RPG")
        {
            SceneManager.LoadScene("RPG"); // Cargar la escena RPG
            return;
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("mainMenu"); // Cargar la escena del menú principal
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Asegurarse de que el tiempo esté normalizado al reiniciar
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name); // Reiniciar la escena actual
    }
}
