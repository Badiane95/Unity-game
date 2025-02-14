using UnityEngine;
using UnityEngine.SceneManagement;

public class CurrentScene : MonoBehaviour
{
    private bool isPaused = false;

    public GameObject game;

    public VoidEventChannel onPlayerDeath;

    private void OnEnable()
    {
        if (onPlayerDeath != null)
        {
            onPlayerDeath.OnEventRaised += Die;
        }
    }

    private void OnDisable()
    {
        if (onPlayerDeath != null)
        {
            onPlayerDeath.OnEventRaised -= Die;
        }
    }

    private void Die()
    {
        game.SetActive(true);
        // Implement game over logic here
        Debug.Log("Player died");
    }

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

    public void RestartScene()
    {
        Time.timeScale = 1; // Ensure the game is not paused when restarting
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Scene Restarted");
    }

    void Start()
    {
        game.SetActive(false);
    }


}


