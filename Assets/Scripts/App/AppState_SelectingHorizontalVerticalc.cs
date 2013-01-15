using System;
using System.Collections.Generic;
using UnityEngine;

class AppState_SelectingHorizontalVerticalc : AppState
{
	#region Public data
	#endregion

	#region Private data
	#endregion

	#region Interface

    public AppState_SelectingHorizontalVerticalc()
        : base(AppStateId.SelectingHorizontalVertical)
	{
	}

	#endregion

	#region Implementation of AppState

	public override void OnActivate(object param)
	{

		base.OnActivate(param);
        
		//AppRoot.Instance.SetState(AppStateId.EditingHorizontalRow);
		AppRoot.Instance.SetState(AppStateId.EditingVerticalStud);
		/*
        foreach (GameObject go in AppRoot.Instance.mUIGroups[Constants.cSideWallLength].UIGroup)
        {            
            go.SetActiveRecursively(true);
        }
        */

	}

	public override void OnDeactivate()
	{
		base.OnDeactivate();
		
		/*
        foreach (GameObject go in AppRoot.Instance.mUIGroups[Constants.cSideWallLength].UIGroup)
        {
            go.SetActiveRecursively(false);
        }
        */

	}
	
	public override void OnUiAction(string controlName, object param)
    {		
		base.OnUiAction(controlName, param);
		
		/*
		if (controlName == Constants.cSideWallLengthDoneContol)
		{
			//Debug.Log("Ok");
			AppRoot.Instance.SetState(AppStateId.SelectingWallPropertiesIcon);			
		}
		*/
	}
	
	#endregion
}
