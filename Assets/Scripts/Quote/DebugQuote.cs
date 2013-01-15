using UnityEngine;
using System.Collections;

public class DebugQuote : MonoBehaviour {
	
	BillOfMaterials bom;
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{	
		if (Input.GetKey(KeyCode.Q))
		{
			bom = new BillOfMaterials();
			bom.CreateBOM();
		}
	}
}
