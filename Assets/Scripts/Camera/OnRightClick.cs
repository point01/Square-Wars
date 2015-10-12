using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class OnRightClick : MonoBehaviour
{
    public RaycastHit hit;

    // Update is called once per frame
    void Update()
    {
        //Get main camera
        var mainCam = Camera.main;

        //On right click
        if (Input.GetMouseButtonDown(1))  
        {
            //creates a ray and its equal to the mouse
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit))
            { // raycast from mouse position into scene
              // if the raycast hit anything then:
                if (hit.transform == gameObject.transform)
                {
                    mainCam.GetComponent<MouseOrbitImproved>().setTarget(hit.transform);
                    //this prints the clicked object
                    print("object:" + gameObject.name);
                }

                //Problems- clicking on cilinder makes it the cube
                //clicking on sphere doesnt work (going home)
            }
        }
    }
}
