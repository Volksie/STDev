using UnityEngine;
using System.Collections;

public class SelectRowStud : MonoBehaviour {
			
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnEnable()
    {
        // register finger gesture events
        FingerGestures.OnFingerDown += FingerGestures_OnFingerDown;        
    }
	
    void FingerGestures_OnFingerDown( int fingerIndex, Vector2 fingerPos )
    {
						
		GameObject pickObj = PickObject(fingerPos);
		
		//Debug.Log("Select " + pickObj );
		if ( pickObj != null )
		{
			if ( pickObj.transform.parent != null )
			{					
				if ( gameObject == pickObj )
				{										
					AppRoot.Instance.OnUICliked( pickObj.transform.parent.gameObject, null );
				}
			}
		}
		
    }
	
	public GameObject PickObject( Vector2 screenPos )
    {
        Ray ray = Camera.mainCamera.ScreenPointToRay( screenPos );
        RaycastHit hit;

        if( Physics.Raycast( ray, out hit ) )
            return hit.collider.gameObject;

        return null;
    }
	
	public void OnDestroy()
	{
		 // register finger gesture events
        FingerGestures.OnFingerDown -= FingerGestures_OnFingerDown; 
	}
	
}
