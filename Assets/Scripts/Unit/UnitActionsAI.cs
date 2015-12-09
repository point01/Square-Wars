using UnityEngine;
using System.Collections;

public class UnitActionsAI : UnitActions {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	public override void TurnUpdate ()
	{
		if (Vector3.Distance(moveDestination, transform.position) > 0.1f) {
			transform.position += (moveDestination - transform.position).normalized * moveSpeed * Time.deltaTime;
			
			if (Vector3.Distance(moveDestination, transform.position) <= 0.1f) {
				transform.position = moveDestination;
                CanMove = false;
			}
		} else {
			moveDestination = new Vector3(0 - Mathf.Floor(GameManager.MapWidth/2),1.5f, -0 + Mathf.Floor(GameManager.MapHeight/2));
		}
		
		base.TurnUpdate ();
	}
	
	public override void TurnOnGUI () {
		base.TurnOnGUI ();
	}
}
