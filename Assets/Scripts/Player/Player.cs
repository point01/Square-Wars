using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Parts/all of this should probably be combined with BaseUnitClass
public class Player : MonoBehaviour {
	
	public Vector2 gridPosition = Vector2.zero;
    public BaseUnitClass unit = new BaseUnitClass();
	public Vector3 moveDestination;
    public Tile DestinationTile;
	public float moveSpeed;
    public System.Collections.Generic.Queue<Tile> moveQueue;
	
	public bool moving = false;
	public bool attacking = false;
    public bool isAlive = false;

    public string playerName = "George";
    public string playerLore = "Default";
	public int HP = 25;
    public int MovementTiles = 3;
    public int MovementJump = 0;
    public int AttackRange = 1;
	

	public float attackChance = 0.75f;
	public float defenseReduction = 0.15f;
	public int damageBase = 5;
	public float damageRollSides = 6; //d6
    
	
	public int actionPoints = 2;

    //List of stats
    List<string> tempList = new List<string>();
	
	void Awake () {
		moveDestination = transform.position;
	}
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
       
    }
	
	public virtual void TurnUpdate () {

		if (actionPoints <= 0) {
			actionPoints = 2;
			moving = false;
			attacking = false;			
			GameManager.instance.nextTurn();
		}
        updateStatus();
	}
	
	public virtual void TurnOnGUI () {
		
	}

    

    //Where the UI will be updated. 
    void updateStatus()
    {
        //Clears old UI
        tempList.Clear();
        //Add all stats that want to be displayed
        tempList.Add("Name: " + playerName);
        tempList.Add("HP: " + HP.ToString());
        tempList.Add("Atk Dmg: " + damageBase.ToString());
        tempList.Add("AP: " + actionPoints.ToString());
        tempList.Add("Def Red: " + defenseReduction.ToString());

        //Update UI static variable
        UnitInfo.unitInfo = tempList;

    }
}
