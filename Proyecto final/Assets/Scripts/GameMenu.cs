using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour {

    public static bool GameIsPaused;
    private bool GameIsInitiating;
    private GameObject InitialMenuUi;
    private GameObject PauseMenuUi;
    private GameObject MainMenuUi;
    private GameObject EndMenuUi;

    void Start(){
      GameIsPaused = true;
      GameIsInitiating = true;
      InitialMenuUi = gameObject.transform.Find("InitialMenu").gameObject;
      PauseMenuUi = gameObject.transform.Find("PauseMenu").gameObject;
      MainMenuUi = gameObject.transform.Find("MainMenu").gameObject;
      EndMenuUi = gameObject.transform.Find("EndMenu").gameObject;
      GameEventsController.eventController.OnPlayerDestruction += FinalScreen;
      GameEventsController.eventController.OnPlayerWon += FinalScreen;
    }

    private void DisplayInitialMenu(){
      InitialMenuUi.SetActive(true);
      Time.timeScale = 0f;
    }

    private void Update()
    {

    if(GameIsInitiating)
       DisplayInitialMenu();

		if (Input.GetButtonDown("Start")){
            if (GameIsPaused && PauseMenuUi.activeSelf)
            {
                Resume();
            }
            else if (!MainMenuUi.activeSelf && !InitialMenuUi.activeSelf && !EndMenuUi.activeSelf)
            {
                Pause();
            }
        }
    }

    public void Play(){
      InitialMenuUi.SetActive(false);
      Time.timeScale = 1f;
      GameIsPaused = false;
      GameIsInitiating = false;
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

    public void FinalScreen(string text){
       Time.timeScale = 0f;
       EndMenuUi.transform.Find("EndText").gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = text;
       EndMenuUi.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Bye");
        Application.Quit();
    }

}
