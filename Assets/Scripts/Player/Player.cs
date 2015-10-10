using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Parts/all of this should probably be combined with BaseUnitClass
public class Player : MonoBehaviour {
	
	public Vector2 gridPosition = Vector2.zero;
    public BaseUnitClass unit = new BaseUnitClass();
	public Vector3 moveDestination;
	public float moveSpeed;
	
	public bool moving = false;
	public bool attacking = false;


    public string playerName;
    public string playerLore;
	public int HP;
    public int MovementTiles;
    public int playerAGI;
    public int MovementJump = 0;
    public int AttackRange;
    public int damage;
	
	//public float attackChance = 0.75f;
	public int playerDEF;
	public int playerSTR;
	//public float damageRollSides = 6; //d6
	
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

    public void setStats(UserPlayer player, string unitClass)
    {
        if (unitClass.Equals("Soldier"))
        {
            BaseUnitClass bu = new BaseSoldierClass();
            player.playerName = bu.UnitClassName;
            player.playerLore = bu.UnitClassLore;
            player.HP = bu.UnitClassHP;
            player.playerSTR = bu.UnitClassSTR;
            player.playerDEF = bu.UnitClassDEF;
            player.MovementTiles = bu.UnitClassSPD;
            player.AttackRange = 1;
            player.playerAGI = bu.UnitClassAGI;

        }
        else
            Debug.Log("Do Nothing");
    }
    
    public void attackEnemy(Player currentPlayer, Player target)
    {
        damage = currentPlayer.playerSTR - target.playerDEF;
        target.HP = target.HP - damage;
    }

    public int Damage
    {
        get { return damage; }
        set { Damage = value; }
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
        tempList.Add("Atk Dmg: " + playerSTR.ToString());
        tempList.Add("AP: " + actionPoints.ToString());
        tempList.Add("Def Red: " + playerDEF.ToString());

        //Update UI static variable
        UnitInfo.unitInfo = tempList;

    }
}
