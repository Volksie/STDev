using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class UIGroups
{
    public GameObject[] UIGroup;

}

public partial class AppRoot
{
    public UIGroups[] mUIGroups;

    public GameObject mUIAll;

    public UIGroups mProductMenu;

	private void InitUI()
	{
		foreach (Transform t in mUIAll.transform)
		{
			//t.gameObject.SetActiveRecursively(false);
		}
	}
}
