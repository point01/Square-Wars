using UnityEngine;
using System.Collections;


public class Tile : MonoBehaviour
{
    public GameObject gameobj;
    public Vector2 gridPosition = Vector2.zero;
    public EnvironmentType envType;
    public UnitActions containedUnit;
    public string tileStatus;
    public bool isAccessible;
    public int MoveCost = 1;
    public float elevation { get { return transform.position.y; } set { transform.position.Set(transform.position.x, value, transform.position.z); } }
    public Color tileColor;

    // Use this for initialization
    void Start()
    {
        
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

    public void setEnvironment (string env)
    {
        switch (env)
        {
            case "barrier":
                envType = EnvironmentType.Barrier;
                tileColor = new Color(42f / 255f, 42f / 255f, 42f / 255f, 1f);
                isAccessible = false;
                break;
            case "stone":
                envType = EnvironmentType.Plains;
                tileColor = new Color(195f / 255f, 5f / 255f, 55f / 255f, 1f);
                isAccessible = true;
                tileStatus = "Stoned #420";
                break;
            case "grass":
                envType = EnvironmentType.Grass;
                tileColor = new Color(144f / 255f, 219f / 255f, 31f / 255f, 1.0f);
                isAccessible = true;
                tileStatus = "Normal";
                break;
            case "plains":
                envType = EnvironmentType.Plains;
                tileColor = new Color(210f / 255f, 170f / 255f, 40f / 255f, 1f);
                isAccessible = true;
                tileStatus = "Poison";
                break;
            case "tallgrass":
                envType = EnvironmentType.TallGrass;
                tileColor = new Color(27f / 255f, 183 / 255f, 21 / 255f, 1f);
                isAccessible = true;
                tileStatus = "tallgrass";
                break;
            case "swamp":
                envType = EnvironmentType.Swamp;
                tileColor = new Color(52f / 255f, 125f / 255f, 13f / 255f, 1f);
                isAccessible = true;
                tileStatus = "Poison";
                break;
            case "forest":
                envType = EnvironmentType.Forest;
                tileColor = new Color(122f / 255f, 86f / 255f, 54f / 255f, 1f);
                isAccessible = true;
                tileStatus = "Normal";
                break;
        }
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
	
	//S_AI: Add in a check to make sure it's not an AI turn or the player will be able to control their units
	void OnMouseDown() {
		if (UnitActionsPlayer.MoveList != null && GameManager.currentTeam.myRoster[GameManager.currentPlayerIndex].CanMove 
            && UnitActionsPlayer.MoveList.Contains(this)) {
			GameManager.instance.moveCurrentPlayer(this);
            GameManager.currentTeam.myRoster[GameManager.currentPlayerIndex].unitStatus = tileStatus;
            GameManager.currentTeam.myRoster[GameManager.currentPlayerIndex].setUnitStatus(GameManager.currentTeam.myRoster[GameManager.currentPlayerIndex]);
        } else if (UnitActionsPlayer.AttackList != null && GameManager.currentTeam.myRoster[GameManager.currentPlayerIndex].CanAttack 
            && UnitActionsPlayer.AttackList.Contains(this)) {
			GameManager.instance.attackWithCurrentPlayer(this);
        }		
		
	}

}
