using UnityEngine;
using System.Collections;

public class Grass_Tile_Script : MonoBehaviour {
	public bool MoveTo = false;
	public int i = -1;
	public int j = -1;

	void Setup(){
//		TileColor = Color.green;
	}

	void Update(){
		if(MoveTo)
			this.GetComponent<Renderer>().material.color = Color.blue;
		else
			this.GetComponent<Renderer>().material.color = Color.green;
	}

	void OnMouseDown(){
		if (MoveTo) {
			NewBehaviourScript.dtstat.text = "Want to move to " + i.ToString () + ", " + j.ToString ();
			Transform tf = 			((GameObject)NewBehaviourScript.punit).GetComponent<Transform>();
			tf.position = new Vector3(i, tf.position.y,j);
			for(int x = 0; x < NewBehaviourScript.MapSize * NewBehaviourScript.MapSize; ++x){
				if(NewBehaviourScript.Map[x] == 1)
					((GameObject)NewBehaviourScript.tiles [x]).GetComponent<Grass_Tile_Script> ().MoveTo = false;
			}
		}
	}
}

