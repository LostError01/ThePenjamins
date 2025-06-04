using UnityEngine;

public class MainMenuBtns : MonoBehaviour
{

    public void PlayGame()
    {
        // Load the game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("RPG");
    }
    public void QuitGame()
    {
        // Quit the application
        Application.Quit();

        // If running in the editor, stop playing

    }

}
