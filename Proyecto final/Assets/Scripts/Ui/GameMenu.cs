using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public static bool gameIsPaused;
    private bool gameIsInitiating;
    private GameObject initialMenuUi;
    private GameObject pauseMenuUi;
    private GameObject mainMenuUi;
    private GameObject endMenuUi;

    public GameObject FindObject(string name)
    {
        Transform[] trs = GetComponentsInChildren<RectTransform>(true);
        foreach (Transform t in trs)
        {
            if (t.name == name)
            {
                return t.gameObject;
            }
        }
        return null;
    }

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

        Sprite t1CursorAl = Resources.Load<Sprite>("T_1_cursor_al");

        GameObject leftButtonEffects = FindObject("LeftButtonEffects");
        GameObject rightButtonEffects = FindObject("RightButtonEffects");
        GameObject leftButtonMusic = FindObject("LeftButtonMusic");
        GameObject rightButtonMusic = FindObject("RightButtonMusic");

        leftButtonEffects.GetComponent<Image>().sprite = t1CursorAl;
        rightButtonEffects.GetComponent<Image>().sprite = t1CursorAl;

        leftButtonMusic.GetComponent<Image>().sprite = t1CursorAl;
        rightButtonMusic.GetComponent<Image>().sprite = t1CursorAl;
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
