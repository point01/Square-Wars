using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UnitInfo : MonoBehaviour {

    public static List<string> unitInfo = new List<string>();
    Text infoDisplayed;
    
	// Use this for initialization
	void Start () {

        //Set up the reference
        infoDisplayed = GetComponent<Text>();

        //Instantiate holders
        unitInfo.Add("Name:");
        unitInfo.Add("Health:");        
    }
	
	// Update is called once per frame
	void Update () {

        //Convert the List to a String
        string info = string.Join("\n", unitInfo.ToArray());
        //Display the updated info
        infoDisplayed.text = info;
    }
}
