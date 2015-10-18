using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	public static GameManager instance;

    static public GameObject TilePrefab;
    static public GameObject UserPlayerPrefab;
	static public GameObject AIPlayerPrefab;
    static public BaseUnitClass baseUnit = new BaseSoldierClass();

    //Intstantiate Teams
    static public Team team1 = new Team();        
    static public Team team2 = new Team();
    static public bool gameOver;

   

    static public int mapSize = 11;

    static public List<List<Tile>> map = new List<List<Tile>>();
	//static public List <Player> players = new List<Player>();
   // static public List<Player> enemyPlayers = new List<Player>();
	static public int currentPlayerIndex = 0;
    static public Player CurrentTurnPlayer;

    static public Team currentTeam;
    static public Team enemyTeam;
	
	void Awake() {
		instance = this;
        TilePrefab = Resources.Load<GameObject>("Prefabs/Tile");
        UserPlayerPrefab = Resources.Load<GameObject>("Prefabs/UserPlayer");
        AIPlayerPrefab = Resources.Load<GameObject>("Prefabs/AIPlayer");        
	}
	
	// Use this for initialization
	void Start () {

        
        TilePrefab.AddComponent<GrassTile>();
        //TilePrefab.AddComponent<Transform>();
        TilePrefab.AddComponent<OnRightClick>();
        

        generateMap();
		generatePlayers();
        baseUnit = new BaseUnitClass();
        //Instantiate gameover
        gameOver = false;

    }
	
	// Update is called once per frame
	void Update () {
        //If not gameOver
        if (!gameOver)
        {
            //Check enemy roster
            if (enemyTeam.myRoster.Exists(x => x.HP > 0))
            {
                if (currentTeam.myRoster[currentPlayerIndex].HP > 0)
                {
                    currentTeam.myRoster[currentPlayerIndex].TurnUpdate();
                }
                else nextTurn();
            }
            else
            {
                endGame();
               // Time.timeScale = 0;
            }
        }
        //if (Input.GetKeyDown("enter"))
        //{
        //    Application.LoadLevel(0);
        //    gameOver = false;
        //}
            


	}
	
	void OnGUI () {
		if (currentTeam.myRoster[currentPlayerIndex].HP > 0) currentTeam.myRoster[currentPlayerIndex].TurnOnGUI();
	}
	
	public void nextTurn() {
        //Check if game going
        if (!gameOver)
        {
            Debug.Log(currentPlayerIndex.ToString());
            UserPlayerPrefab.GetComponent<UserPlayer>().StopEverything();

            //Are all the players done with their turn
            if (currentPlayerIndex + 1 < currentTeam.myRoster.Count)
            {
                currentPlayerIndex++;
                //Change teams and put counter at start of team
            }
            else
            {
                switch (currentTeam.teamName)
                {
                    case "Team1":
                        currentTeam = team2;
                        enemyTeam = team1;
                        break;
                    case "Team2":
                        currentTeam = team1;
                        enemyTeam = team2;
                        break;
                    default:
                        break;
                }
                currentPlayerIndex = 0;
            }
            CurrentTurnPlayer = currentTeam.myRoster[currentPlayerIndex];
        }

	}
	
	public void moveCurrentPlayer(Tile destTile) {
        currentTeam.myRoster[currentPlayerIndex].gridPosition = destTile.gridPosition;
        currentTeam.myRoster[currentPlayerIndex].moveDestination = destTile.transform.position + 1.5f * Vector3.up;
        currentTeam.myRoster[currentPlayerIndex].moveQueue = Movement.CurrentMovementTree.GetMoveQueue(destTile);
        UserPlayerPrefab.GetComponent<UserPlayer>().StopEverything();
	}
	
	public void attackWithCurrentPlayer(Tile destTile) {
		Player target = null;
        Player currentPlayer = currentTeam.myRoster[currentPlayerIndex];
		foreach (Player p in enemyTeam.myRoster) {
			if (p.gridPosition == destTile.gridPosition) {
				target = p;
			}
        }
		
		if (target != null) {
			if (UserPlayer.AttackList.Contains(destTile)) {
                currentPlayer.actionPoints--;
                currentPlayer.unitCombat(currentPlayer, target);
                
			} else {
				Debug.Log ("Target is not in range!");
			}
		}
        UserPlayerPrefab.GetComponent<UserPlayer>().StopEverything();
    }

    void generateMap()
    {
        map = new List<List<Tile>>();
        for (int i = 0; i < mapSize; i++)
        {

            // Create one row of tiles
            List<Tile> row = new List<Tile>();

            for (int j = 0; j < mapSize; j++)
            {

                Vector3 position = new Vector3(i - Mathf.Floor(mapSize / 2), 0, -j + Mathf.Floor(mapSize / 2));
                Quaternion rotation = Quaternion.Euler(new Vector3());

                Tile tile = ((GameObject)Instantiate(TilePrefab, position, rotation)).GetComponent<GrassTile>();

                if (tile == null)
                {
                    Debug.Log("WHARRGARBL");
                }

                // Add tile's own position on grid to the tile
                tile.gridPosition = new Vector2(i, j);

                row.Add(tile);
            }
            map.Add(row);
        }
    }

    //Called when one team has run out of playable players
    void endGame()
    {
        gameOver = true;      

    }

    /*public void setClass(UserPlayer player, string c)
    {
        if (c.Equals("Soldier")){
            BaseUnitClass bu = new BaseSoldierClass();
            player.playerName = bu.UnitClassName;
            player.playerLore = bu.UnitClassLore;
        }
        else
        {
            Debug.Log("Nothing");
        }
    }*/

    void generatePlayers() {
		UserPlayer player;
        BaseUnitClass baseUnit = new BaseSoldierClass();
        currentTeam = team1;
        enemyTeam = team2;
        team1.teamName = "Team1";
        team2.teamName = "Team2";
       

		player = ((GameObject)Instantiate(UserPlayerPrefab, new Vector3(1 - Mathf.Floor(mapSize/2),1.5f, -(mapSize/2) + Mathf.Floor(mapSize/2)), Quaternion.Euler(new Vector3()))).GetComponent<UserPlayer>();
		player.gridPosition = new Vector2(1,(mapSize/2));
        player.playerName = baseUnit.UnitClassName;
        player.playerLore = baseUnit.UnitClassLore;
        player.setStats(player, "Soldier");
  //      Debug.Log(player.playerLore);	

		team1.myRoster.Add(player);
		
		player = ((GameObject)Instantiate(UserPlayerPrefab, new Vector3(1 - Mathf.Floor(mapSize/2),1.5f, -(mapSize/2-1) + Mathf.Floor(mapSize/2)), Quaternion.Euler(new Vector3()))).GetComponent<UserPlayer>();
		player.gridPosition = new Vector2(1,(mapSize/2-1));
        player.setStats(player, "Soldier");
		player.playerName = "Kyle";
        player.MovementTiles = 3;

        team1.myRoster.Add(player);

        player = ((GameObject)Instantiate(UserPlayerPrefab, new Vector3(1 - Mathf.Floor(mapSize/2),1.5f, -(mapSize/2+1) + Mathf.Floor(mapSize/2)), Quaternion.Euler(new Vector3()))).GetComponent<UserPlayer>();
		player.gridPosition = new Vector2(1,(mapSize/2+1));
        player.setStats(player, "Soldier");
		player.playerName = "Lars";
        player.AttackRange = 3;
        player.MovementTiles = 1;

        team1.myRoster.Add(player);

        player = ((GameObject)Instantiate(UserPlayerPrefab, new Vector3((mapSize-1) - Mathf.Floor(mapSize / 2), 1.5f, -1 + Mathf.Floor(mapSize / 2)), Quaternion.Euler(new Vector3()))).GetComponent<UserPlayer>();
        player.gridPosition = new Vector2(mapSize-1, 1);
        player.setStats(player, "Soldier");
        player.playerName = "Steve";
        player.AttackRange = 5;
        player.MovementTiles = 3;

        team2.myRoster.Add(player);

        player = ((GameObject)Instantiate(UserPlayerPrefab, new Vector3((mapSize - 1) - Mathf.Floor(mapSize / 2), 1.5f, -2 + Mathf.Floor(mapSize / 2)), Quaternion.Euler(new Vector3()))).GetComponent<UserPlayer>();
        player.gridPosition = new Vector2(mapSize-1, 2);
        player.setStats(player, "Soldier");
        player.playerName = "Sir William";
        player.AttackRange = 2;
        player.MovementTiles = 6;

        team2.myRoster.Add(player);

        player = ((GameObject)Instantiate(UserPlayerPrefab, new Vector3((mapSize - 1) - Mathf.Floor(mapSize / 2), 1.5f, -3 + Mathf.Floor(mapSize / 2)), Quaternion.Euler(new Vector3()))).GetComponent<UserPlayer>();
        player.gridPosition = new Vector2(mapSize-1, 3);
        player.setStats(player, "Soldier");
        player.playerName = "Tyler";
        player.AttackRange = 2;
        player.MovementTiles = 6;

        team2.myRoster.Add(player);

        //AIPlayer aiplayer = ((GameObject)Instantiate(AIPlayerPrefab, new Vector3(6 - Mathf.Floor(mapSize/2),1.5f, -4 + Mathf.Floor(mapSize/2)), Quaternion.Euler(new Vector3()))).GetComponent<AIPlayer>();

        //players.Add(aiplayer);
    }
}
