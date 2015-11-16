using System;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    static public GameObject UserPlayerPrefab;
    static public GameObject AIPlayerPrefab;

    // Teams
    static public Team team1 = new Team();
    static public Team team2 = new Team();
    static public bool gameOver;
    static public bool freezeGame;

    // Environment blocks
    static public GameObject TilePrefab;
    static public GameObject TilePrefab_e0;
    static public GameObject TilePrefab_e1;
    static public GameObject TilePrefab_e2;

    // Decorations
    static public GameObject groundPlane;
    static public GameObject decoBush;
	static public GameObject decoTree;

    //The x and y dimensions of map loaded
    static public int MapWidth;
    static public int MapHeight;
    static public float mapcenterX;
    static public float mapcenterY;
    static public Vector3 mapCenter;

    static public List<List<Tile>> map = new List<List<Tile>>();
    static public int currentPlayerIndex = 0;
    static public UnitActions CurrentTurnPlayer;

    static public Team currentTeam;
    static public Team enemyTeam;
    static public int team1TurnNum;
    static public int team2TurnNum;

    void Awake()
    {
        instance = this;

        // Initialize prefabs
        TilePrefab = Resources.Load<GameObject>("Prefabs/Tile");
        TilePrefab_e0 = Resources.Load<GameObject>("Prefabs/Tile_e0");
        TilePrefab_e1 = Resources.Load<GameObject>("Prefabs/Tile_e1");
        TilePrefab_e2 = Resources.Load<GameObject>("Prefabs/Tile_e2");
        UserPlayerPrefab = Resources.Load<GameObject>("Prefabs/UserPlayer");
        AIPlayerPrefab = Resources.Load<GameObject>("Prefabs/AIPlayer");

		// Decorations
		decoBush = Resources.Load<GameObject>("TerrainAssets/Bushes/Bush1");
        groundPlane = Resources.Load<GameObject>("Prefabs/groundPlane");
    }

    // Use this for initialization
    void Start()
    {
        // Load map and set related decorations
        generateMapFromFile("Maps/example1");

        //Place units on board
        //This will later include a call to manual unit placement function
        //once it is made
        generatePlayers();

        //Instantiate gameover
        Movement.UnPaintTiles(null);
        gameOver = false;
        freezeGame = false;
        team1TurnNum = 1;
        team2TurnNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //If not gameOver
        if (!gameOver || !freezeGame)
        {
            //Check enemy roster
            if (enemyTeam.myRoster.Exists(x => x.unitHP > 0))
            {
                if (currentTeam.myRoster[currentPlayerIndex].unitHP > 0)
                {
                    currentTeam.myRoster[currentPlayerIndex].TurnUpdate();
                }
                else nextTurn();
            }
            else
            {
                endGame();
            }
        }
    }

    void OnGUI()
    {
        if (currentTeam.myRoster[currentPlayerIndex].unitHP > 0) currentTeam.myRoster[currentPlayerIndex].TurnOnGUI();
    }

    public void nextTurn()
    {
        //Check if game going
        if (!gameOver || !freezeGame)
        {
           // Debug.Log(currentPlayerIndex.ToString());
            UserPlayerPrefab.GetComponent<UnitActionsPlayer>().StopEverything();

            //Are all the players done with their turn
            if (currentPlayerIndex + 1 < currentTeam.myRoster.Count)
            {
                checkStatus(currentTeam.myRoster[currentPlayerIndex]);
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
                        team2TurnNum++;
                        Debug.Log("Team 2 Turn Number: " + team2TurnNum);
                        break;
                    case "Team2":
                        currentTeam = team1;
                        enemyTeam = team2;
                        team1TurnNum++;
                        Debug.Log("Team 1 Turn Number: " + team1TurnNum);
                        break;
                    default:
                        break;
                }
                currentPlayerIndex = 0;
            }
            CurrentTurnPlayer = currentTeam.myRoster[currentPlayerIndex];
        }

    }

    public void checkStatus(UnitActions Unit)
    {
        if (Unit.isPoisoned == true)
        {
            Unit.unitHP -= 1;
            Unit.unitPoisonCounter -= 1;
            Debug.Log("Unit Poisoned for " + Unit.unitPoisonCounter + " more turns!");
            if(Unit.unitPoisonCounter == 0)
            {
                Unit.isPoisoned = false;
                Unit.unitPoisonCounter = 3;
                Unit.unitStatus = "Normal";
                Debug.Log("Congrats, your unit is NOT poisoned");
            }
        }
    }

    public void moveCurrentPlayer(Tile destTile)
    {
        currentTeam.myRoster[currentPlayerIndex].gridPosition = destTile.gridPosition;
        currentTeam.myRoster[currentPlayerIndex].moveDestination = destTile.transform.position + 1.5f * Vector3.up;
        currentTeam.myRoster[currentPlayerIndex].moveQueue = Movement.CurrentMovementTree.GetMoveQueue(destTile);
        UserPlayerPrefab.GetComponent<UnitActionsPlayer>().StopEverything();
    }

    public void attackWithCurrentPlayer(Tile destTile)
    {
        UnitActions target = null;
        UnitActions currentPlayer = currentTeam.myRoster[currentPlayerIndex];
        foreach (UnitActions p in enemyTeam.myRoster)
        {
            if (p.gridPosition == destTile.gridPosition)
            {
                target = p;
            }
        }

        if (target != null)
        {
            if (UnitActionsPlayer.AttackList.Contains(destTile))
            {
                currentPlayer.actionPoints--;
                currentPlayer.unitCombat(currentPlayer, target);

            }
            else
            {
                Debug.Log("Target is not in range!");
            }
        }
        UserPlayerPrefab.GetComponent<UnitActionsPlayer>().StopEverything();
    }

    void generateMapFromFile(string name)
    {
        FileReader fr = new FileReader();
        string[] mapArr = fr.textAssetToStrArray(name);

        map = new List<List<Tile>>();
        GameObject tilePrefab;

        int xCoord = 0;
        MapWidth = mapArr.Length;

        foreach (string line in mapArr) // for each column
        {
            // The finished product we're building
            List<Tile> column = new List<Tile>();

            // The bits of the file we're building from
            string[] tileStrArr = line.Split(new string[] { ";" }, StringSplitOptions.None);

            int yCoord = 0;
            MapHeight = tileStrArr.Length;

            foreach (string tileStr in tileStrArr)
            {
                string[] attributes = tileStr.Split(new string[] { "," }, StringSplitOptions.None);

                string env = attributes[0];                 //Get string representation of tile type
                int height = Int32.Parse(attributes[1]);    //get int representation of tile height

                Vector3 position = new Vector3(xCoord, (height / 4.0f), yCoord);
                Quaternion rotation = Quaternion.Euler(new Vector3());

                switch (height)     // Set height
                {
                    case 1:
                        tilePrefab = TilePrefab_e0;
                        break;
                    case 2:
                        tilePrefab = TilePrefab_e1;
                        break;
                    case 3:
                        tilePrefab = TilePrefab_e2;
                        break;
                    default:
                        tilePrefab = TilePrefab_e0;
                        break;
                }

				GameObject block = Instantiate(tilePrefab, position, rotation) as GameObject;
                Tile tile = block.GetComponent<Tile>();

                switch (env)        // Create block based on height, type
                {
                    case "barrier":
                        tile.setEnvironment("barrier");
                        break;
                    case "stone":
                        tile.setEnvironment("stone");
                        break;
                    case "grass":
                        tile.setEnvironment("grass");
						GameObject bsh = Instantiate(decoBush, position, rotation) as GameObject;
						break;
                    case "plains":
                        tile.setEnvironment("plains");
                        break;
                    case "forest":
                        tile.setEnvironment("forest");
                        break;
                }
                tile.gridPosition = new Vector2(xCoord, yCoord);    // Add tile's own position on grid to the tile
                column.Add(tile);                                   // Add tile to the column
                yCoord += 1;                                        // Increment yCoord for next run
            }
            map.Add(column);    //Add column to map
            xCoord += 1;        //Increment xCoord for next run
        }

        // Initialize center holder values
        mapcenterX = MapWidth / 2;
        mapcenterY = MapHeight / 2;
        mapCenter = new Vector3(mapcenterX, 0.0f, mapcenterY);
        groundPlane.transform.position = mapCenter;

        Debug.Log(mapcenterX);
        Debug.Log(mapcenterY);
        Debug.Log(mapCenter);
        Debug.Log(groundPlane.transform.position);

        Instantiate(groundPlane, mapCenter, Quaternion.Euler(new Vector3()));

    }

    //Called when one team has run out of playable players
    void endGame()
    {
        gameOver = true;
        freezeGame = true;
    }

    void generatePlayers()
    {
        UnitActionsPlayer player;
        ModelUnit baseUnit = new ModelSoldier();
        currentTeam = team1;
        enemyTeam = team2;
        team1.teamName = "Team1";
        team2.teamName = "Team2";

        // TODO
        // The following code can be adapted into a "create unit" method
        // Which can then be adapted into future versions that include
        // team creation mode. Also keeps us from repeating ourselves too much
        // Pass in a position and unit type as a parameter, then add to roster
        // GeneratePlayers then, for now, would be simply six calls to this function.

        Vector3 position = new Vector3(1, 1.5f, (MapHeight / 2));
        Quaternion rotation = Quaternion.Euler(new Vector3());

        player = ((GameObject)Instantiate(UserPlayerPrefab, position, rotation)).GetComponent<UnitActionsPlayer>();
        player.gridPosition = new Vector2(1, (MapHeight / 2));
        player.setStats(player, "Soldier");
        player.MovementJump = 0.5f;

        team1.myRoster.Add(player);

        // end snippet

        player = ((GameObject)Instantiate(UserPlayerPrefab, new Vector3(1, 1.5f, (MapHeight / 2 - 1)), Quaternion.Euler(new Vector3()))).GetComponent<UnitActionsPlayer>();
        player.gridPosition = new Vector2(1, (MapHeight / 2 - 1));
        player.setStats(player, "Mage");
        player.unitName = "Kyle";
        player.MovementJump = 0.5f;

        team1.myRoster.Add(player);

        player = ((GameObject)Instantiate(UserPlayerPrefab, new Vector3(1, 1.5f, (MapHeight / 2 + 1)), Quaternion.Euler(new Vector3()))).GetComponent<UnitActionsPlayer>();
        player.gridPosition = new Vector2(1, (MapHeight / 2 + 1));
        player.setStats(player, "Cavalier");
        player.unitName = "Lars";
        player.MovementJump = 0.5f;

        team1.myRoster.Add(player);

        player = ((GameObject)Instantiate(UserPlayerPrefab, new Vector3((MapWidth - 1), 1.5f, -1 + Mathf.Floor(MapHeight / 2)), Quaternion.Euler(new Vector3()))).GetComponent<UnitActionsPlayer>();
        player.gridPosition = new Vector2(MapHeight - 1, 1);
        player.setStats(player, "Soldier");
        player.unitName = "Steve";
        player.AttackRange = 5;
        player.MovementTiles = 3;
        player.MovementJump = 0.5f;

        team2.myRoster.Add(player);

        player = ((GameObject)Instantiate(UserPlayerPrefab, new Vector3((MapWidth - 1), 1.5f, -2 + Mathf.Floor(MapHeight / 2)), Quaternion.Euler(new Vector3()))).GetComponent<UnitActionsPlayer>();
        player.gridPosition = new Vector2(MapHeight - 1, 2);
        player.setStats(player, "Knight");
        player.unitName = "Sir William";

        team2.myRoster.Add(player);

        player = ((GameObject)Instantiate(UserPlayerPrefab, new Vector3((MapWidth - 1), 1.5f, -3 + Mathf.Floor(MapHeight / 2)), Quaternion.Euler(new Vector3()))).GetComponent<UnitActionsPlayer>();
        player.gridPosition = new Vector2(MapHeight - 1, 3);
        player.setStats(player, "Cavalier");
        player.unitName = "Tyler";
        player.MovementJump = 0.5f;

        team2.myRoster.Add(player);
    }
}
