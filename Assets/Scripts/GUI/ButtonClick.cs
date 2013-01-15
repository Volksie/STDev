using UnityEngine;
using System.Collections;

public class ButtonClick : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Click(bool pressed)
    {
        if (pressed)
        {		
            AppRoot.Instance.OnUICliked(gameObject, null);
        }
    }

}
