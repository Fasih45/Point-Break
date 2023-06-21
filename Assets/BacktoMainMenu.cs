using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    private float delay = 5f; // Delay in seconds before switching to the main menu
    private float timer = 0f; // Timer to track the elapsed time

    void Update()
    {
        timer += Time.deltaTime; // Increase the timer

        if (timer >= delay)
        {
            SwitchToMainMenu(); // Call the function to switch to the main menu
        }
    }

    void SwitchToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Replace "MainMenu" with the actual name of your main menu scene
    }
}
