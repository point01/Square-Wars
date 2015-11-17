using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Parts/all of this should probably be combined with BaseUnitClass
public class UnitActions : MonoBehaviour
{

    // do not change
    public Vector2 gridPosition
    {
        get { return new Vector2(transform.position.x, transform.position.z); }
        set { transform.position.Set(value.x, transform.position.y, value.y); }
    }

    public ModelUnit unit = new ModelUnit();
    public Vector3 moveDestination;
    public Tile DestinationTile;
    public float moveSpeed;
    public System.Collections.Generic.Queue<Tile> moveQueue;

    public bool moving = false;
    public bool attacking = false;
    public bool isAlive
    {
        get { return unitHP > 0; }
        set
        {
            if (!value) unitHP = 0;
            if (value && unitHP < 1) unitHP = 1;
        }
    }

    public string unitName;
    public string unitType;
    public string unitLore;
    public string unitStatus;
    public int unitAGI;
    public int unitHP;
    public int MovementTiles;
    public float MovementJump = 0.5f;
    public int AttackRange;

    public float attackChance = 1;
    public int unitSTR;
    public int unitDEF;
    public int unitMAG;
    public int unitMDF;
    public float damageRollSides = 6; //d6
    public int actionPoints = 2;
    public int unitPoisonCounter = 3;
    public bool isPoisoned = false;

    //List of stats
    List<string> tempList = new List<string>();

    void Awake()
    {
        moveDestination = transform.position;
    }

    public virtual void TurnUpdate()
    {

        if (actionPoints <= 0)
        {
            actionPoints = 2;
            moving = false;
            attacking = false;
            GameManager.instance.nextTurn();
          //  if (unitStatus.Equals("Poison"))
          //      unitPoisonCounter -= 1;
        }
        updateStatus();
    }

    public void setStats(UnitActionsPlayer player, string unitClass)
    {
        ModelUnit bu;

        if (unitClass.Equals("King"))
        {
            bu = new ModelKing();
            player.unitName = bu.UnitClassName;
            player.unitType = bu.UnitClassType;
            player.unitLore = bu.UnitClassLore;
            player.unitHP = bu.UnitClassHP;
            player.unitSTR = bu.UnitClassSTR;
            player.unitDEF = bu.UnitClassDEF;
            player.unitMAG = bu.UnitClassMAG;
            player.unitMDF = bu.UnitClassMDF;
            player.MovementTiles = bu.UnitClassSPD;
            player.AttackRange = 1;
            player.unitAGI = bu.UnitClassAGI;
            player.unitStatus = "Normal";
        }

        if (unitClass.Equals("Soldier"))
        {
            bu = new ModelSoldier();
            player.unitName = bu.UnitClassName;
            player.unitType = bu.UnitClassType;
            player.unitLore = bu.UnitClassLore;
            player.unitHP = bu.UnitClassHP;
            player.unitSTR = bu.UnitClassSTR;
            player.unitDEF = bu.UnitClassDEF;
            player.unitMAG = bu.UnitClassMAG;
            player.unitMDF = bu.UnitClassMDF;
            player.MovementTiles = bu.UnitClassSPD;
            player.AttackRange = 1;
            player.unitAGI = bu.UnitClassAGI;
            player.unitStatus = "Normal";
        }

        if (unitClass.Equals("Knight"))
        {
            bu = new ModelKnight();
            player.unitName = bu.UnitClassName;
            player.unitType = bu.UnitClassType;
            player.unitLore = bu.UnitClassLore;
            player.unitHP = bu.UnitClassHP;
            player.unitSTR = bu.UnitClassSTR;
            player.unitDEF = bu.UnitClassDEF;
            player.unitMAG = bu.UnitClassMAG;
            player.unitMDF = bu.UnitClassMDF;
            player.MovementTiles = bu.UnitClassSPD;
            player.AttackRange = 1;
            player.unitAGI = bu.UnitClassAGI;
            player.unitStatus = "Normal";
        }

        if (unitClass.Equals("Mage"))
        {
            bu = new ModelMage();
            player.unitName = bu.UnitClassName;
            player.unitType = bu.UnitClassType;
            player.unitLore = bu.UnitClassLore;
            player.unitHP = bu.UnitClassHP;
            player.unitSTR = bu.UnitClassSTR;
            player.unitDEF = bu.UnitClassDEF;
            player.unitMAG = bu.UnitClassMAG;
            player.unitMDF = bu.UnitClassMDF;
            player.MovementTiles = bu.UnitClassSPD;
            player.AttackRange = 2;
            player.unitAGI = bu.UnitClassAGI;
            player.unitStatus = "Normal";
        }

        if (unitClass.Equals("Cavalier"))
        {
            bu = new ModelCavalier();
            player.unitName = bu.UnitClassName;
            player.unitType = bu.UnitClassType;
            player.unitLore = bu.UnitClassLore;
            player.unitHP = bu.UnitClassHP;
            player.unitSTR = bu.UnitClassSTR;
            player.unitDEF = bu.UnitClassDEF;
            player.unitMAG = bu.UnitClassMAG;
            player.unitMDF = bu.UnitClassMDF;
            player.MovementTiles = bu.UnitClassSPD;
            player.AttackRange = 2;
            player.unitAGI = bu.UnitClassAGI;
            player.unitStatus = "Normal";
        }
    }

    /**************************************************************************************************************************
     * This method currently handles general unit combat. If the attacking unit has higher AGI, it'll attack first. Otherwise
     * the defending unit will attack first. 
     *
     * @attacker The attacking unit. Can be either the User's unit or the Enemy (other user or AI).
     * @defender The unit that didn't start the attack. Can be either the User's unit or the Enemy (other user or AI).
     *
     *************************************************************************************************************************/
    public void unitCombat(UnitActions attacker, UnitActions defender)
    {
        //The damage the attacking unit will give
        int attackerDamage = attackDamage(attacker, defender);
        //The damage the defending unit will give
        int defenderDamage = attackDamage(defender, attacker);

        //If attacking unit has more AGI, it'll attack first
        if (attacker.unitAGI >= defender.unitAGI)
        {
            Debug.Log("" + attacker.unitName + " attacked first!");
            defender.unitHP = defender.unitHP - attackerDamage;
            Debug.Log("" + attacker.unitName + " attacked " + defender.unitName + " for " + attackerDamage + " damage!");
            if (defender.unitHP > 0)
            {
                attacker.unitHP = attacker.unitHP - defenderDamage;
                Debug.Log("" + defender.unitName + " attacked " + attacker.unitName + " for " + defenderDamage + " damage!");
            }
        }
        //If defending unit (non-attacking unit) has higher AGI, it will attack first
        else
        {
            Debug.Log("" + defender.unitName + " attacked first!");
            attacker.unitHP = attacker.unitHP - defenderDamage;
            Debug.Log("" + defender.unitName + " attacked " + attacker.unitName + " for " + defenderDamage + " damage!");
            if (attacker.unitHP > 0)
            {
                defender.unitHP = defender.unitHP - attackerDamage;
                Debug.Log("" + attacker.unitName + " attacked " + defender.unitName + " for " + attackerDamage + " damage!");
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
    public int attackDamage(UnitActions attacker, UnitActions defender)
    {
        int damage;

        if (attacker.unitType.Equals("Mage"))
        {
            damage = attacker.unitMAG - defender.unitMDF;
        }

        else
        {
            damage = attacker.unitSTR - defender.unitDEF;
        }

        return damage;
    }

    public void setUnitStatus(UnitActions unit)
    {
        if (unit.unitStatus.Equals("Normal"))
        {
            
        }

        if (unit.unitStatus.Equals("Poison"))
        {
            unit.isPoisoned = true;
        }
    }

    public virtual void TurnOnGUI()
    {

    }



    //Where the UI will be updated. 
    void updateStatus()
    {
        //Clears old UI
        tempList.Clear();
        //Add all stats that want to be displayed
        tempList.Add("Name: " + unitName);
        tempList.Add("Type: " + unitType);
        tempList.Add("Status: " + unitStatus);
        tempList.Add("HP: " + unitHP.ToString());
        tempList.Add("Atk Dmg: " + unitSTR.ToString());
        tempList.Add("Def: " + unitDEF.ToString());
        tempList.Add("Magic Dmg: " + unitMAG.ToString());
        tempList.Add("Mag Def: " + unitMDF.ToString());
        tempList.Add("AP: " + actionPoints.ToString());

        //Update UI static variable
        UnitInfo.unitInfo = tempList;

    }
}
