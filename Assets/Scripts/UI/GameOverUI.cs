using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameOverUI : MonoBehaviour
{
    
    Text textDisplayed;
    // Use this for initialization
    void Start()
    {
        textDisplayed = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameOver == true)
        {
            textDisplayed.text = "Game Over!\n" + GameManager.currentTeam.teamName + " Wins!";
        }
    }
}
