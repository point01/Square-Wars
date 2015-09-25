using UnityEngine;
using System.Collections;

public class Team1click : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		//NewBehaviourScript.dtstat.text = "Click";
		int posi = (int)((GameObject)NewBehaviourScript.punit).GetComponent<Transform> ().position.x;
		int posj = (int)((GameObject)NewBehaviourScript.punit).GetComponent<Transform> ().position.z;
		TryMovable (posi + 1, posj);
		TryMovable (posi - 1, posj);
		TryMovable (posi, posj + 1);
		TryMovable (posi, posj - 1);
		//((GameObject)NewBehaviourScript.tiles [1]).GetComponent<Grass_Tile_Script> ().MoveTo = true;
		//((GameObject)NewBehaviourScript.tiles [10]).GetComponent<Grass_Tile_Script> ().MoveTo = true;
		//(()t).GetComponent<Renderer>().material.SetColor(Color.blue);
	}
	private void TryMovable(int targeti, int targetj){
		if (targeti >= 0 && targeti < NewBehaviourScript.MapSize && targetj >= 0 && targetj < NewBehaviourScript.MapSize) {
			if(NewBehaviourScript.Map[targeti + NewBehaviourScript.MapSize * targetj] == 1)
				((GameObject)NewBehaviourScript.tiles [targeti + NewBehaviourScript.MapSize * targetj]).GetComponent<Grass_Tile_Script> ().MoveTo = true;
		}
	}
}
