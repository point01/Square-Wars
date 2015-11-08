using UnityEngine;

public class ClickDragCam : MonoBehaviour
{
    public float speed = 10.0f;
    private Vector3 dragOrigin;

    void Update()
    {
		// Zoom in/out
		if (Input.GetAxis("Mouse ScrollWheel") != 0.0f){
			transform.Translate(new Vector3(0.0f, 0.0f, Input.GetAxis("Mouse ScrollWheel")));
		}

	//	if (Input.GetMouseButtonDown(1))
      //  {
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

	        transform.Translate(move, Space.World);
    
		//}
	}
}
