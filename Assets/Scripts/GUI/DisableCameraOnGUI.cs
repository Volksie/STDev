using UnityEngine;
using System.Collections;

public class DisableCameraOnGUI : MonoBehaviour {

    private const string cGUITag = "GUI";
    public GameObject mMouseControl;
    public Camera mGUICamera;

	// Use this for initialization
	void Start () {

        mMouseControl = GameObject.FindGameObjectWithTag("CameraControl") as GameObject;

	}
	
	// Update is called once per frame
	void Update () {

        //if (Camera.current != null)
        {
			
            //Ray ray = Camera.current.ScreenPointToRay(Input.mousePosition);
            Ray ray = mGUICamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 10f))
            {
                if (hit.collider.tag == cGUITag)
                {
                    mMouseControl.SetActiveRecursively(false);                    
                }
            }
            else
            {
                if (!Input.GetMouseButton(0))
                {
                    mMouseControl.SetActiveRecursively(true);
                }
            }

        }
	}
}
