using System;
using System.Collections.Generic;
using UnityEngine;

class AppState_EditingDoor : AppState
{
	#region Public data
	#endregion

	#region Private data
	#endregion

	#region Interface

    public AppState_EditingDoor()
        : base(AppStateId.EditingDoor)
	{
	}

	#endregion

	#region Implementation of AppState

	public override void OnActivate(object param)
	{

		base.OnActivate(param);

        foreach (GameObject go in AppRoot.Instance.mUIGroups[Constants.cDoorsPropertiesPanel].UIGroup)
        {            
            go.SetActiveRecursively(true);
        }        

	}

	public override void OnDeactivate()
	{
		base.OnDeactivate();

        foreach (GameObject go in AppRoot.Instance.mUIGroups[Constants.cDoorsPropertiesPanel].UIGroup)
        {
            go.SetActiveRecursively(false);
        }          

	}

    public override void OnSectionClick(GameObject go)
    {
       
    }

	public override void OnUiAction(string controlName, object param)
    {		
		base.OnUiAction(controlName, param);

        if (controlName == Constants.cDoorSizeControl)
        {
                        
        }
        else if (controlName == Constants.cDoorSizeDoneControl)
        {
            foreach (GameObject go in AppRoot.Instance.mUIGroups[Constants.cEditingDoor].UIGroup)
            {
                go.SetActiveRecursively(false);
            }
        }
        else if (controlName == Constants.сBackButton)
        {
            AppRoot.Instance.SetState(AppStateId.SelectingWallPropertiesIcon);
        }
      
        
	}
	
	#endregion
    
}
