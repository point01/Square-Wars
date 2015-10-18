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

    public string playerName;
    public string playerLore;
    public int playerAGI;
    public int HP;
    public int MovementTiles;
    public int MovementJump = 0;
    public int AttackRange;
	

	public float attackChance = 1;
	public int defenseReduction;
	public int damageBase;
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

    public void setStats(UserPlayer player, string unitClass)
    {
        if (unitClass.Equals("Soldier"))
        {
            BaseUnitClass bu = new BaseSoldierClass();
            player.playerName = bu.UnitClassName;
            player.playerLore = bu.UnitClassLore;
            player.HP = bu.UnitClassHP;
            player.damageBase = bu.UnitClassSTR;
            player.defenseReduction = bu.UnitClassDEF;
            player.MovementTiles = bu.UnitClassSPD;
            player.AttackRange = 1;
            player.playerAGI = bu.UnitClassAGI;

        }
        else
            Debug.Log("Do Nothing");
    }

    /**************************************************************************************************************************
     * This method currently handles general unit combat. If the attacking unit has higher AGI, it'll attack first. Otherwise
     * the defending unit will attack first. 
     *
     * @attacker The attacking unit. Can be either the User's unit or the Enemy (other user or AI).
     * @defender The unit that didn't start the attack. Can be either the User's unit or the Enemy (other user or AI).
     *
     *************************************************************************************************************************/
    public void unitCombat(Player attacker, Player defender)
    {
        //The damage the attacking unit will give
        int attackerDamage = attackDamage(attacker, defender);
        //The damage the defending unit will give
        int defenderDamage = attackDamage(defender, attacker);

        //If attacking unit has more AGI, it'll attack first
        if(attacker.playerAGI >= defender.playerAGI)
        {
            Debug.Log("" + attacker.playerName + " attacked first!");
            defender.HP = defender.HP - attackerDamage;
            Debug.Log("" + attacker.playerName + " attacked " + defender.playerName + " for " + attackerDamage + " damage!");
            if(defender.HP > 0)
            {
                attacker.HP = attacker.HP - defenderDamage;
                Debug.Log("" + defender.playerName + " attacked " + attacker.playerName + " for " + defenderDamage + " damage!");
            }
        }
        //If defending unit (non-attacking unit) has higher AGI, it will attack first
        else
        {
            Debug.Log("" + defender.playerName + " attacked first!");
            attacker.HP = attacker.HP - defenderDamage;
            Debug.Log("" + defender.playerName + " attacked " + attacker.playerName + " for " + defenderDamage + " damage!");
            if (attacker.HP > 0)
            {
                defender.HP = defender.HP - attackerDamage;
                Debug.Log("" + attacker.playerName + " attacked " + defender.playerName + " for " + attackerDamage + " damage!");
            } 
        }
    }

    /**************************************************************************************************************************
     * This method calculates the damage of a potential attack. Can be used for unitCombat method, the UI, and the log.
     *
     * @attacker The attacking unit. Can be either the User's unit or the Enemy (other user or AI).
     * @defender The unit that didn't start the attack. Can be either the User's unit or the Enemy (other user or AI).
     *
     *************************************************************************************************************************/
    //Once environment tiles and abilities are done, this will be more complex
    public int attackDamage(Player attacker, Player defender)
    {
        int damage = attacker.damageBase - defender.defenseReduction;
        return damage;
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
