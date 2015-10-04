using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	
	public Vector2 gridPosition = Vector2.zero;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseEnter() {
		if (GameManager.instance.players[GameManager.instance.currentPlayerIndex].moving) {
			transform.GetComponent<Renderer>().material.color = Color.blue;
		} else if (GameManager.instance.players[GameManager.instance.currentPlayerIndex].attacking) {
			transform.GetComponent<Renderer>().material.color = Color.red;
		}
		//Debug.Log("my position is (" + gridPosition.x + "," + gridPosition.y);
	}
	
	void OnMouseExit() {
		transform.GetComponent<Renderer>().material.color = Color.white;
	}
	
	
	void OnMouseDown() {
		if (GameManager.instance.players[GameManager.instance.currentPlayerIndex].moving) {
			GameManager.instance.moveCurrentPlayer(this);
		} else if (GameManager.instance.players[GameManager.instance.currentPlayerIndex].attacking) {
			GameManager.instance.attackWithCurrentPlayer(this);
		}		
		
	}
	
}
