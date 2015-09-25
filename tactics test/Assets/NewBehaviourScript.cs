using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {
//These Objects ___PROBABLY___ should be GameObjects instead
	public int PlayerTurn;
	public static int[] Map;
	public static int MapSize;
	public Object[] Tiles;
//	public Object Tile_Grass;
//	public Object Tile_Water;
	public UnityEngine.UI.Text DebugText;
	static public UnityEngine.UI.Text dtstat;
	public static Object[] tiles;
	public Object Player_Unit;
	public Object Enemy_Unit;

	static public Object punit;
	private Object eunit;

	// Use this for initialization
	void Start () {
		dtstat = DebugText;
		PlayerTurn = 1;
		MapSize = 10;
		Map = new int[MapSize * MapSize];
		for (int i = 0; i < MapSize*MapSize; ++i) {
			Map[i] = 1;
		}
		Map [6] = 0;
		Map [2] = 0;
		Tiles = new Object[MapSize * MapSize];
		Tile_Data td = GameObject.Find ("Tiles").GetComponent<Tile_Data>();
		for (int i = 0; i < MapSize; ++i) {
			for(int j = 0; j < MapSize; ++j){
				Object current_tile;
				switch (Map[i + j * MapSize]){
				case 0:
					current_tile = td.WaterTile;
					break;
				case 1:
					current_tile = td.GrassTile;
					break;
				default:
					current_tile = td.WaterTile;
					break;
				}
				Tiles[i + j * MapSize] = (Object)Instantiate(current_tile,new Vector3(i,0,j), new Quaternion(0,0,0,0));
				if(Map[i + j * MapSize] == 1){
					((GameObject)Tiles[i + j * MapSize]).GetComponent<Grass_Tile_Script>().i = i;
					((GameObject)Tiles[i + j * MapSize]).GetComponent<Grass_Tile_Script>().j = j;
				}
			}
		}
		DebugText.text = "Player 1 turn.";
		
		tiles = Tiles;
		punit = Instantiate (Player_Unit, new Vector3 (0, 1.5f, 0), new Quaternion (0, 0, 0, 0));
		eunit = Instantiate (Enemy_Unit, new Vector3 (MapSize - 1, 1.5f, MapSize - 1), new Quaternion (0, 0, 0, 0));
	}
	
	// Update is called once per frame
	void Update () {
	}
}
