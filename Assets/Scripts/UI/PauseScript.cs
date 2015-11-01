using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour {

    public Canvas pauseMenu;
    public Button resumeGame;
    public Button restartGame;
    public Button exitGame;

    private bool isPaused;
    private bool resume;

    void Start()
    {
        pauseMenu = pauseMenu.GetComponent<Canvas>();
        resumeGame = resumeGame.GetComponent<Button>();
        restartGame = restartGame.GetComponent<Button>();
        exitGame = exitGame.GetComponent<Button>();
        isPaused = false;
        resume = false;
        pauseMenu.enabled = false;
    }

    void Update()
    {
        //Check if Escape key is pressed to pause game
        if( Input.GetKeyDown(KeyCode.Escape) || resume == true)
        {
            isPaused = !isPaused;
            if (isPaused)
            {

                pauseMenu.enabled = !pauseMenu.enabled;
                Time.timeScale = 0;
                GameManager.freezeGame = true;

            }
            else
            {
                pauseMenu.enabled = false;
                Time.timeScale = 1;
                GameManager.freezeGame = false;
            }
            resume = false;
        }

    }

    public void resumePress()
    {
       resume = true;
 
    }

    public void endPress()
    {       
        Application.LoadLevel(0);
    }

    public void restartPress()
    {
        Application.LoadLevel(1);
    }


}
