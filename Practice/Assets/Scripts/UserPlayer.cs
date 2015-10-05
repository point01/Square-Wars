using UnityEngine;
using System.Collections;

public class UserPlayer : Player {

    void Awake()
    {
        moveDestination = transform.position;
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void TurnUpdate()
    {
        if (Vector3.Distance(moveDestination, transform.position) > 0.1f)
        {
            transform.position += (moveDestination - transform.position).normalized * moveSpeed * Time.deltaTime;

            if (Vector3.Distance(moveDestination, transform.position) > 0.1f)
            {
                transform.position = moveDestination;
                GameManager.instance.nextTurn();
            }
        }
        base.TurnUpdate();
    }
}
