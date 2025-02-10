using UnityEngine;
using UnityEngine.SceneManagement;

public class CurrentScene : MonoBehaviour
{
    private bool isPaused = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartScene();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        Debug.Log(isPaused ? "Game Paused" : "Game Resumed");
    }

    void RestartScene()
    {
        Time.timeScale = 1; // Ensure the game is not paused when restarting
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Scene Restarted");
    }
}
