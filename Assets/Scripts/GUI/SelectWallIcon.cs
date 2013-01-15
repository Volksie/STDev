using UnityEngine;
using System.Collections;

public class SelectWallIcon : MonoBehaviour {
			
	public GameObject frontWall;
	public GameObject leftWall;
	public GameObject rightWall;
	
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
					
		if ( pickObj != null )
		{
			
			if ( pickObj.name == frontWall.name )
			{
				AppRoot.Instance.OnUICliked( frontWall, null );
			}
			
			if ( pickObj.name == leftWall.name )
			{
				AppRoot.Instance.OnUICliked( leftWall, null );
			}
			
			if ( pickObj.name == rightWall.name )
			{
				AppRoot.Instance.OnUICliked( rightWall, null );
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
	
}
