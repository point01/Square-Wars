using UnityEngine;
using System.Collections;
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
        unitInfo.Add("Info:");
        unitInfo.Add("Lore:");
    }
	
	// Update is called once per frame
	void Update () {

        string info = string.Join("\n", unitInfo.ToArray());
        infoDisplayed.text = info;
    }
}
