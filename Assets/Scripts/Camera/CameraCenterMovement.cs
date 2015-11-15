using UnityEngine;
using System.Collections;

public class CameraCenterMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float horizontal = 0.0f;
		float vertical = 0.0f;

		if (Input.GetKey("w")){
			vertical = 0.5f;
		}

		if (Input.GetKey("s")){
			vertical = -0.5f;
		}

		if (Input.GetKey("d")){
			horizontal = 0.5f;
		}

		if (Input.GetKey("a")){
			horizontal = -0.5f;
		}

        Vector3 move = new Vector3(horizontal, 0.0f, vertical);

        transform.Translate(move);

		float yrot = Camera.main.transform.eulerAngles.y;
		transform.eulerAngles = new Vector3(0, yrot, 0);


	}
}
