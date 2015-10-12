using UnityEngine;
using System.Collections;


public class Tile : MonoBehaviour
{

    public Vector2 gridPosition = Vector2.zero;
    public EnvironmentType envType;
    public Player containedUnit;
    public bool isAccessible;
    public int MoveCost = 1;
    public int elevation;
    public Color tileColor = Color.white;

    // Use this for initialization
    void Start()
    {
        transform.GetComponent<Renderer>().material.color = tileColor;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public EnvironmentType EnvType
    {
        get { return envType; }
        set { envType = value; }
    }

    void OnMouseEnter()
    {
        // TODO Add some sort of "targeting reticle" above the selected tile
        // Instead of some sort of color change

        /*
        if (GameManager.players[GameManager.currentPlayerIndex].moving) {
			transform.GetComponent<Renderer>().material.color = Color.blue;
		} else if (GameManager.players[GameManager.currentPlayerIndex].attacking) {
			transform.GetComponent<Renderer>().material.color = Color.red;
		}
        */
    }
	
	void OnMouseExit() {
		//transform.GetComponent<Renderer>().material.color = Color.white;
	}
	
	
	void OnMouseDown() {
		if (GameManager.currentTeam.myRoster[GameManager.currentPlayerIndex].moving && UserPlayer.MoveList.Contains(this)) {
			GameManager.instance.moveCurrentPlayer(this);
		} else if (GameManager.currentTeam.myRoster[GameManager.currentPlayerIndex].attacking && UserPlayer.AttackList.Contains(this)) {
			GameManager.instance.attackWithCurrentPlayer(this);
		}		
		
	}

}
