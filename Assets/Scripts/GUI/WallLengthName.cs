using UnityEngine;
using System.Collections;

public class WallLengthName : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnEnable()
	{
	 	UILabel uil = GetComponent(typeof(UILabel)) as UILabel;
		
		if (AppRoot.Scene.SelectedWallId == PorchWallId.Front )
		{
			uil.text = "MAIN WALL LENGTH";
		}
		
		if (AppRoot.Scene.SelectedWallId == PorchWallId.Left )
		{
			uil.text = "LEFT WALL LENGTH";
		}
		
		if (AppRoot.Scene.SelectedWallId == PorchWallId.Right )
		{
			uil.text = "RIGHT WALL LENGTH";
		}
				
	}
	
}
