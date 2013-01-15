using UnityEngine;
using System.Collections;

public class MenuPreConfigManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OneWall()
    {
		AppRoot.Instance.OnUICliked(gameObject, PorchWallLayoutId.OneWall);
    }

    public void TwoWallLeft()
    {
		AppRoot.Instance.OnUICliked(gameObject, PorchWallLayoutId.TwoWallLeft);
    }

    public void TwoWallRight()
    {
		AppRoot.Instance.OnUICliked(gameObject, PorchWallLayoutId.TwoWallRight);
    }

    public void ThreeWall()
    {
		AppRoot.Instance.OnUICliked(gameObject, PorchWallLayoutId.ThreeWall);
    }

}
