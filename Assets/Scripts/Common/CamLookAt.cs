using UnityEngine;
using System.Collections;
 
public class CamLookAt : MonoBehaviour {

public Transform target ;
public float damping = 6;
public bool smooth = true;
 
void LateUpdate () {	
 
	if (target) 
		{
			if (smooth)
			{
				// Look at and dampen the rotation
				var rotation = Quaternion.LookRotation(target.position - transform.position);
				transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
			}
			else
			{
				// Just lookat
		    	transform.LookAt(target);
			}
		}
	}
}