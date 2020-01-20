using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public static bool gameIsPaused;
    private bool gameIsInitiating;
    private GameObject initialMenuUi;
    private GameObject pauseMenuUi;
    private GameObject mainMenuUi;
    private GameObject endMenuUi;

    private void Start()
    {
        gameIsPaused = true;
        gameIsInitiating = true;
        initialMenuUi = gameObject.transform.Find("InitialMenu").gameObject;
        pauseMenuUi = gameObject.transform.Find("PauseMenu").gameObject;
        mainMenuUi = gameObject.transform.Find("MainMenu").gameObject;
        endMenuUi = gameObject.transform.Find("EndMenu").gameObject;
        GameEventsController.eventController.OnPlayerDestruction += FinalScreen;
        GameEventsController.eventController.OnPlayerWon += FinalScreen;
    }

    private void DisplayInitialMenu()
    {
        initialMenuUi.SetActive(true);
        Time.timeScale = 0f;
    }

    private void Update()
    {
        if (gameIsInitiating)
        {
            DisplayInitialMenu();
        }

        if (Input.GetButtonDown("Start"))
        {
            if (gameIsPaused && pauseMenuUi.activeSelf)
            {
                Resume();
            }
            else if (!mainMenuUi.activeSelf && !initialMenuUi.activeSelf && !endMenuUi.activeSelf)
            {
                Pause();
            }
        }
    }

    public void Play()
    {
        initialMenuUi.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        gameIsInitiating = false;
    }

    public void Resume()
    {
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    private void Pause()
    {
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void FinalScreen(string text)
    {
        Time.timeScale = 0f;
        endMenuUi.transform.Find("EndText").gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = text;
        endMenuUi.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Bye");
        Application.Quit();
    }

}
