using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour {

    public static bool GameIsPaused = false;
    public GameObject PauseMenuUi;
    public GameObject MainMenuUi;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (GameIsPaused && PauseMenuUi.activeSelf)
            {
                Resume();
            }
            else if (!MainMenuUi.activeSelf)
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        PauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        PauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Debug.Log("Bye");
        Application.Quit();
    }

}
