using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class MenuScript : MonoBehaviour {

    //Start menu UI elements
    public Canvas quitMenu;
    public Button startText;
    public Button exitText;
    public Canvas startCanvas;

    //Game mode UI elements
    public Canvas gameModeCanvas;
    public Dropdown difficultDropDown;
    public Dropdown turnTimerDropDown;
    public Button nextToMap;
    public Button backToMain;

    //Choose Map UI elements
    public Canvas chooseMapCanvas;
    public Dropdown chooseMapDropDown;
    public Image currentMap;
    public Button nextToChooseTeam;
    public Button backToGameMode;

    //Choose Team UI elements
    public Canvas chooseTeamCanvas;
    public Button chooseTeamPlayButton;
    public Button backToChooseMap;


	// Use this for initialization
	void Start () {


        //Quit menu and Start Menu
        startCanvas = startCanvas.GetComponent<Canvas>();
        quitMenu = quitMenu.GetComponent<Canvas>();
        startText = startText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();
        quitMenu.enabled = false;
        startCanvas.enabled = true;
        //Game Mode
        gameModeCanvas = gameModeCanvas.GetComponent<Canvas>();
        difficultDropDown = difficultDropDown.GetComponent<Dropdown>();
        turnTimerDropDown = turnTimerDropDown.GetComponent<Dropdown>();
        nextToMap = nextToMap.GetComponent<Button>();
        backToMain = backToMain.GetComponent<Button>();
        gameModeCanvas.enabled = false;

        //Choose Map
        chooseMapCanvas = chooseMapCanvas.GetComponent<Canvas>();
        chooseMapDropDown = chooseMapDropDown.GetComponent<Dropdown>();
        currentMap = currentMap.GetComponent<Image>();
        nextToChooseTeam = nextToChooseTeam.GetComponent<Button>();
        backToGameMode = backToGameMode.GetComponent<Button>();
        chooseMapCanvas.enabled = false;

        //Choose Team
        chooseTeamCanvas = chooseTeamCanvas.GetComponent<Canvas>();
        chooseTeamPlayButton = chooseTeamPlayButton.GetComponent<Button>();
        backToChooseMap = backToChooseMap.GetComponent<Button>();
        chooseTeamCanvas.enabled = false;
        

    }

    //Enable the selected canvas
    private void setEnabled(Canvas enabledCanvas)
    {
        if(enabledCanvas == startCanvas)
        {
            startCanvas.enabled = true;
            gameModeCanvas.enabled = false;
            chooseMapCanvas.enabled = false;
            chooseTeamCanvas.enabled = false;

        }else
            if(enabledCanvas == gameModeCanvas)
        {
            startCanvas.enabled = false;
            gameModeCanvas.enabled = true;
            chooseMapCanvas.enabled = false;
            chooseTeamCanvas.enabled = false;
        }
        else
            if(enabledCanvas == chooseMapCanvas)
        {
            startCanvas.enabled = false;
            gameModeCanvas.enabled = false;
            chooseMapCanvas.enabled = true;
            chooseTeamCanvas.enabled = false;
        }
        else
            if(enabledCanvas == chooseTeamCanvas)
        {
            startCanvas.enabled = false;
            gameModeCanvas.enabled = false;
            chooseMapCanvas.enabled = false;
            chooseTeamCanvas.enabled = true;
        }        
        
    }

    //Press exit in start screen
    public void ExitPress()
    {
        quitMenu.enabled = true;
        startText.enabled = false;
        exitText.enabled = false;
    }

    //In quit menu press no
    public void NoPress ()
    {
        quitMenu.enabled = false;
        startText.enabled = true;
        exitText.enabled = true;
    }

    //In start screen press Play
    public void StartGame()
    {
        setEnabled(gameModeCanvas);
        //Application.LoadLevel(1);        
    }

    //In quit screen press exit
    public void ExitGame()
    {
        Application.Quit();
    }

    //TODO: In game mode difficulty drop down

    //TODO: In game mode turn timer drop down

    //TODO: In game mode next button to choose map
    public void nextToChooseMapPress()
    {
        setEnabled(chooseMapCanvas);

    }

    //TODO: In game mode back button to start screen
    public void backToStartScreenPress()
    {
        setEnabled(startCanvas);

    }

    //TODO: In choose map map drop down

    //TODO: In choose map next to choose team
    public void nextToChooseTeamPress()
    {
        setEnabled(chooseTeamCanvas);

    }

    //TODO: In choose map back to game mode
    public void backToGameModePress()
    {
        setEnabled(gameModeCanvas);

    }

    //TODO: In choose map current map image

    //TODO: In choose team play button
    public void chooseTeamPlayPress()
    {
        Application.LoadLevel(1);
    }

    //TODO: In choose team back to choose map
    public void backToChooseMapPress()
    {
        setEnabled(chooseMapCanvas);

    }


}
