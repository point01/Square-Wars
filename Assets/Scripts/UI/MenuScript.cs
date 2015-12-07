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
    public static string currentDifficulty;
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
    public Button unit1Button;
    public Button unit2Button;
    public Button unit3Button;
    public Button unit4Button;
    public Button addUnitButton;
    public Text currentTeam;
    public List<string> currentTeamList;
    public string selectedUnit;
    public int teamCost;
    public Text teamCostText;


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
        currentDifficulty = "Easy";
        chooseMapCanvas.enabled = false;

        //Choose Team
        chooseTeamCanvas = chooseTeamCanvas.GetComponent<Canvas>();
        chooseTeamPlayButton = chooseTeamPlayButton.GetComponent<Button>();
        backToChooseMap = backToChooseMap.GetComponent<Button>();
        chooseTeamCanvas.enabled = false;
        unit1Button = unit1Button.GetComponent<Button>();
        unit2Button = unit2Button.GetComponent<Button>();
        unit3Button = unit3Button.GetComponent<Button>();
        unit4Button = unit4Button.GetComponent<Button>();
        addUnitButton = addUnitButton.GetComponent<Button>();
        currentTeam = currentTeam.GetComponent<Text>();
        teamCostText = teamCostText.GetComponent<Text>();
        selectedUnit = "";

        teamCostText.text = teamCost.ToString();

        currentTeamList = new List<string>();
    }

    void Update()
    {

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
    }

    //In quit screen press exit
    public void ExitGame()
    {
        Application.Quit();
    }

    //TODO: In game mode difficulty drop down
    public void selectDIfficulty()
    {
        currentDifficulty = difficultDropDown.options[difficultDropDown.value].text;
    }

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
    //Unit1 Button is pressed
    public void unit1ButtonPress()
    {
        selectedUnit = "Knight";
        Debug.Log(teamCost.ToString());
        Debug.Log(selectedUnit);
    }

    //Unit2 Button is pressed
    public void unit2ButtonPress()
    {
       selectedUnit = "Mage";
        Debug.Log(selectedUnit);


    }

    //Unit3 Button is pressed
    public void unit3ButtonPress()
    {        
        selectedUnit = "Soldier";
        Debug.Log(selectedUnit);

    }

    //Unit4 Button is pressed
    public void unit4ButtonPress()
    {
        selectedUnit = "Cavalier";
        Debug.Log(selectedUnit);

    }

    // Add Unit button is pressed
    public void addUnitButtonPress()
    {
		Debug.Log (selectedUnit);
        if(selectedUnit != "" && teamCost > 0)
        {
            switch (selectedUnit)
            {
                case "Knight":
                    if (teamCost >= 2)
                    {
                        teamCost = teamCost - 2;
                        currentTeamList.Add(selectedUnit);
						Debug.Log("Here bitch");
                        updateCurrentTeam();
                    }
                    break;
                case "Mage":
                    if (teamCost >= 2)
                    {
                        currentTeamList.Add(selectedUnit);
                        teamCost = teamCost - 2;
                        updateCurrentTeam();
                    }
                    break;
                case "Soldier":
                    if (teamCost >= 1)
                    {
                        currentTeamList.Add(selectedUnit);
                        teamCost = teamCost - 1;
                        updateCurrentTeam();
                    }

                    break;
                case "Cavalier":
                    if(teamCost >= 3)
                    {
                        currentTeamList.Add(selectedUnit);
                        teamCost = teamCost - 3;
                        updateCurrentTeam();
                    }
                    break;
                default:
                    break;
            }
        }
        selectedUnit = "";
    }

    public List<string> CurrentTeamList
    {
        get { return currentTeamList; }
        set { currentTeamList = value; }
    }

    void updateCurrentTeam()
    {
        //Convert the List to a String
		Debug.Log ("Fucked up");
        string info = string.Join("\n", currentTeamList.ToArray());
        //Display the updated info
        currentTeam.text = info;
        string cost = "" + teamCost;
        teamCostText.text = cost;
    }

}
