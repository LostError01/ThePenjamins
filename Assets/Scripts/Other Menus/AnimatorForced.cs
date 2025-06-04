using UnityEngine;
using UnityEngine.SceneManagement; // Ensure you have this line to use UI Text

public class AnimatorForced : MonoBehaviour
{
    [Header("Animators")]
    [SerializeField] private Animator roblosAnimator; // Animator for the player character
    [SerializeField] private Animator playerAnimator; // Animator for the enemy character
    [SerializeField] private Animator textAnimator1; // Animator for the enemy character
    [SerializeField] private Animator textAnimator2; // Animator for the enemy character
    [SerializeField] private Animator textAnimator3;
    [SerializeField] private Animator btnAnimator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        roblosAnimator.Play("bible_GAMEOVER");
        playerAnimator.Play("Char_GAMEOVER");
        textAnimator1.Play("txt01_GAMEOVER_1");
        textAnimator2.Play("txt02_GAMEOVER_1");
        textAnimator3.Play("chakalito_GAMEOVER_1");
        btnAnimator.Play("menu_GAMEOVER_1");
    }


    // Update is called once per frame
    void Update()
    {
        Time.timeScale = 1f; // Ensure the game runs at normal speed
    }

    public void Menu()
    {
        SceneManager.LoadScene("mainMenu"); // Load the main menu scene
    }

}
