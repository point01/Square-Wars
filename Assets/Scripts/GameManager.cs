using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	public static GameManager instance;
	
	public GameObject TilePrefab;
	public GameObject UserPlayerPrefab;
	public GameObject AIPlayerPrefab;
   
    static public int mapSize = 11;

    static public List<List<Tile>> map = new List<List<Tile>>();
	static public List <Player> players = new List<Player>();
    static public List<Player> team2 = new List<Player>();
	static public int currentPlayerIndex = 0;
    static public Player CurrentTurnPlayer;
	
	void Awake() {
		instance = this;
	}
	
	// Use this for initialization
	void Start () {		
		generateMap();
		generatePlayers();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (players[currentPlayerIndex].HP > 0) players[currentPlayerIndex].TurnUpdate();
		else nextTurn();

	}
	
	void OnGUI () {
		if (players[currentPlayerIndex].HP > 0) players[currentPlayerIndex].TurnOnGUI();
	}
	
	public void nextTurn() {
        UserPlayerPrefab.GetComponent<UserPlayer>().StopEverything();
		if (currentPlayerIndex + 1 < players.Count) {
			currentPlayerIndex++;
		} else {
			currentPlayerIndex = 0;
		}
        CurrentTurnPlayer = players[currentPlayerIndex];
	}
	
	public void moveCurrentPlayer(Tile destTile) {
		players[currentPlayerIndex].gridPosition = destTile.gridPosition;
		players[currentPlayerIndex].moveDestination = destTile.transform.position + 1.5f * Vector3.up;
        UserPlayerPrefab.GetComponent<UserPlayer>().StopEverything();
	}
	
	public void attackWithCurrentPlayer(Tile destTile) {
		Player target = null;
        Player attackingUnit = players[currentPlayerIndex];
		foreach (Player p in players) {
			if (p.gridPosition == destTile.gridPosition) {
				target = p;
			}
        }

        if (target != null) {
            if (UserPlayer.AttackList.Contains(destTile)) {
                attackingUnit.actionPoints--;
                attackingUnit.attackEnemy(attackingUnit, target);
                //This is code for future combat
                // if (attackingUnit.playerAGI >= target.playerAGI)
                //     attackingUnit.attackEnemy(attackingUnit, target);
                // else
                //     target.attackEnemy(target, attackingUnit);
                Debug.Log(attackingUnit.playerName + " successfuly hit " + target.playerName + " for " + attackingUnit.Damage + " damage!");
			} else {
				Debug.Log ("Target is not in range!");
			}
		}
        UserPlayerPrefab.GetComponent<UserPlayer>().StopEverything();
    }
	
	void generateMap() {
		map = new List<List<Tile>>();
		for (int i = 0; i < mapSize; i++) {
			List <Tile> row = new List<Tile>();
			for (int j = 0; j < mapSize; j++) {
				Tile tile = ((GameObject)Instantiate(TilePrefab, new Vector3(i - Mathf.Floor(mapSize/2),0, -j + Mathf.Floor(mapSize/2)), Quaternion.Euler(new Vector3()))).GetComponent<Tile>();
				tile.gridPosition = new Vector2(i, j);
				row.Add (tile);
			}
			map.Add(row);
		}
	}

	void generatePlayers() {
		UserPlayer player;
        BaseUnitClass baseUnit = new BaseSoldierClass();
		
		player = ((GameObject)Instantiate(UserPlayerPrefab, new Vector3(0 - Mathf.Floor(mapSize/2),1.5f, -0 + Mathf.Floor(mapSize/2)), Quaternion.Euler(new Vector3()))).GetComponent<UserPlayer>();
		player.gridPosition = new Vector2(0,0);
        //player.playerName = baseUnit.UnitClassName;
        //player.playerLore = baseUnit.UnitClassLore;
        //setClass(player, "Soldier");
        player.setStats(player, "Soldier");
        Debug.Log(player.playerLore);	
		players.Add(player);
		
		player = ((GameObject)Instantiate(UserPlayerPrefab, new Vector3((mapSize-1) - Mathf.Floor(mapSize/2),1.5f, -(mapSize-1) + Mathf.Floor(mapSize/2)), Quaternion.Euler(new Vector3()))).GetComponent<UserPlayer>();
		player.gridPosition = new Vector2(mapSize-1,mapSize-1);
        player.setStats(player, "Soldier");
		player.playerName = "Kyle";
        player.playerAGI = 5;
		players.Add(player);
				
		player = ((GameObject)Instantiate(UserPlayerPrefab, new Vector3(4 - Mathf.Floor(mapSize/2),1.5f, -4 + Mathf.Floor(mapSize/2)), Quaternion.Euler(new Vector3()))).GetComponent<UserPlayer>();
		player.gridPosition = new Vector2(4,4);
        player.setStats(player, "Soldier");
		player.playerName = "Lars";
        player.AttackRange = 3;
        player.MovementTiles = 1;
		
		players.Add(player);
		
		//AIPlayer aiplayer = ((GameObject)Instantiate(AIPlayerPrefab, new Vector3(6 - Mathf.Floor(mapSize/2),1.5f, -4 + Mathf.Floor(mapSize/2)), Quaternion.Euler(new Vector3()))).GetComponent<AIPlayer>();
		
		//players.Add(aiplayer);
	}
}
