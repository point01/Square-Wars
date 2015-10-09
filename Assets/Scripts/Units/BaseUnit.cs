using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Parts/all of this should probably be combined with BaseUnitClass
public class BaseUnit : MonoBehaviour {
	
    // Old
	public Vector2 gridPosition = Vector2.zero;
	public Vector3 moveDestination;
	public float moveSpeed;
	
	public bool moving = false;
	public bool attacking = false;

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
    // Old

    //Description
    private string unitClassName;
    private string unitClassLore;

    //STATS
    private int unitClassHP;
    private int unitClassSTR;
    private int unitClassDEF;
    private int unitClassSPD;
    private int unitClassAGI;

    // set + get Name string
    public string UnitClassName
    {
        get { return unitClassName; }
        set { unitClassName = value; }
    }

    // set + get Lore string 
    public string UnitClassLore
    {
        get { return unitClassLore; }
        set { unitClassLore = value; }
    }

    // set + get HP
    public int UnitClassHP
    {
        get { return unitClassHP; }
        set { unitClassHP = value; }
    }

    // set + get STR
    public int UnitClassSTR
    {
        get { return unitClassSTR; }
        set { unitClassSTR = value; }
    }

    // set + get DEF
    public int UnitClassDEF
    {
        get { return unitClassDEF; }
        set { unitClassDEF = value; }
    }

    // set + get SPD
    public int UnitClassSPD
    {
        get { return unitClassSPD; }
        set { unitClassSPD = value; }
    }

    // set + get AGI
    public int UnitClassAGI
    {
        get { return unitClassAGI; }
        set { unitClassAGI = value; }
    }


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
