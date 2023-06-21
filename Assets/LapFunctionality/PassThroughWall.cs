using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PassThroughWall : MonoBehaviour
{
    public Text countdownText;  // Reference to the UI Text component for countdown display
    public Text winText;  // Reference to the UI Text component for win display
    public int laps;
    private int passCount1 = 0;  // Counter to keep track of the number of bodies passed through the wall
    private int passCount2 = 0;  // Counter to keep track of the number of bodies passed through the wall
    private bool isSceneStarted = false;  // Flag to check if the scene has started
    private bool isScenePaused = true;  // Flag to check if the scene is paused

    public float returnTimer = 5f; // Timer for returning to a different scene
    private bool shouldReturn = false; // Flag indicating whether to return to a different scene

    private void Start()
    {
        // Find the UI Text component and assign it to the countdownText variable
        countdownText = GameObject.Find("Countdown").GetComponent<Text>();

        // Start the coroutine to start the scene
        StartCoroutine(StartScene(0f));
    }

    private System.Collections.IEnumerator StartScene(float startDelay)
    {
        yield return new WaitForSecondsRealtime(startDelay);  // Wait for the start delay

        isSceneStarted = true;  // Set the flag to indicate that the scene has started

        Time.timeScale = 0f;  // Pause the scene
        Debug.Log("Scene started (paused)!");
        countdownText.gameObject.SetActive(true);  // Show the countdown text

        // Display countdown sequence on the screen
        for (int count = 3; count > 0; count--)
        {
            countdownText.text = count.ToString();
            yield return new WaitForSecondsRealtime(1f);
        }

        countdownText.text = "GO!";
        yield return new WaitForSecondsRealtime(1f);

        Time.timeScale = 1f;  // Resume the scene
        Debug.Log("Scene resumed!");
        countdownText.gameObject.SetActive(false);  // Hide the countdown text
        isScenePaused = false;  // Reset the flag to indicate that the scene is resumed
    }

    private System.Collections.IEnumerator PauseScene(float duration)
    {
        isScenePaused = true;  // Set the flag to indicate that the scene is paused

        Time.timeScale = 0f;  // Pause the scene
        Debug.Log("Scene paused!");

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = 1f;  // Resume the scene
        Debug.Log("Scene resumed!");

        isScenePaused = false;  // Reset the flag to indicate that the scene is resumed
    }

    private void Update()
    {
        if (isSceneStarted && !isScenePaused && (passCount1 == laps || passCount2 == laps))
        {
            
            winText = GameObject.Find("Win").GetComponent<Text>();

            if (passCount1 == laps)
                winText.text = "Player 1 WON!";
            else
                winText.text = "Player 2 WON!";

            shouldReturn = true; // Set the flag to return to a different scene
            //Time.timeScale = 0f;  // Pause the scene by setting the time scale to 0
        }

        if (shouldReturn)
        {
            returnTimer -= Time.deltaTime; // Decrease the return timer

            if (returnTimer <= 0f)
            {
                ReturnToDifferentScene(); // Call the function to return to a different scene
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is a body
        if (other.CompareTag("Player1"))
        {
            passCount1++;  // Increment the pass count
            Debug.Log("Player1 Count: " + passCount1);
        }
        else if (other.CompareTag("Player2"))
        {
            passCount2++;  // Increment the pass count
            Debug.Log("Player2 Count: " + passCount2);
        }
    }

    private void ReturnToDifferentScene()
    {
        // Replace "YourSceneName" with the name of the scene you want to return to
        SceneManager.LoadScene("MainMenu");
    }
}
