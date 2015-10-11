using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	
	public Vector2 gridPosition = Vector2.zero;
    public int MoveCost = 1;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseEnter() {
		/*if (GameManager.players[GameManager.currentPlayerIndex].moving) {
			transform.GetComponent<Renderer>().material.color = Color.blue;
		} else if (GameManager.players[GameManager.currentPlayerIndex].attacking) {
			transform.GetComponent<Renderer>().material.color = Color.red;
		}*/
		//Debug.Log("my position is (" + gridPosition.x + "," + gridPosition.y);
	}
	
	void OnMouseExit() {
		//transform.GetComponent<Renderer>().material.color = Color.white;
	}
	
	
	void OnMouseDown() {
		if (GameManager.players[GameManager.currentPlayerIndex].moving && UserPlayer.MoveList.Contains(this)) {
			GameManager.instance.moveCurrentPlayer(this);
		} else if (GameManager.players[GameManager.currentPlayerIndex].attacking && UserPlayer.AttackList.Contains(this)) {
			GameManager.instance.attackWithCurrentPlayer(this);
		}		
		
	}
	
}
