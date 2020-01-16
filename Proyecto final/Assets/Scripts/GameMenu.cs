using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour {

    public static bool GameIsPaused;
    private GameObject PauseMenuUi;
    private GameObject MainMenuUi;
    private GameObject EndMenuUi;

    void Start(){
      GameIsPaused = false;
      PauseMenuUi = gameObject.transform.Find("PauseMenu").gameObject;
      MainMenuUi = gameObject.transform.Find("MainMenu").gameObject;
      EndMenuUi = gameObject.transform.Find("EndMenu").gameObject;
      GameEventsController.eventController.OnPlayerDestruction += FinalScreen;
      GameEventsController.eventController.OnPlayerWon += FinalScreen;
    }

    private void Update()
    {
		if (Input.GetButtonDown("Start")){
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
