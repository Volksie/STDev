using UnityEngine;
using System.Collections;

public class MenuButtonManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnMenuButtonClick(bool pressed)
    {
        if (pressed)
        {
            AppRoot.Instance.OnUICliked(gameObject, null);
        }
        //AppRoot.Instance.SetState(AppStateId.ChoosePorchWallLayout);
        //AppRoot.Instance.OnUICliked(gameObject, null);
    }

}
