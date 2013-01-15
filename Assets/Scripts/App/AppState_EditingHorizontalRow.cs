using System;
using System.Collections.Generic;
using UnityEngine;

class AppState_EditingHorizontalRow : AppState
{
	#region Public data
	#endregion

	#region Private data   
	#endregion

	#region Interface

    public AppState_EditingHorizontalRow()
        : base(AppStateId.EditingHorizontalRow)
	{
	}

	#endregion

	#region Implementation of AppState

	public override void OnActivate(object param)
	{

		base.OnActivate(param);
                
        foreach (GameObject go in AppRoot.Instance.mUIGroups[Constants.cEditingHorizontalRow].UIGroup)
        {            
            go.SetActiveRecursively(true);
        }

        AppRoot.Scene.Porch.WallIconsVisible = true;
	}

	public override void OnDeactivate()
	{
		base.OnDeactivate();
        
        foreach (GameObject go in AppRoot.Instance.mUIGroups[Constants.cEditingHorizontalRow].UIGroup)
        {
            go.SetActiveRecursively(false);
        }

        foreach (GameObject go in AppRoot.Instance.mUIGroups[Constants.cMoveHorizontalRow].UIGroup)
        {
            go.SetActiveRecursively(false);
        }

        AppRoot.Scene.Porch.WallIconsVisible = false;

	}

    public override void OnSectionClick(GameObject go)
    {
        if (AppRoot.Porch.GetElement(go) != null)
        {

            DisableAllAffordance();
            
            foreach (GameObject ugo in AppRoot.Instance.mUIGroups[Constants.cMoveHorizontalRow].UIGroup)
            {
                ugo.SetActiveRecursively(false);
            }
            
            /*
            foreach (GameObject ugo in AppRoot.Instance.mUIGroups[Constants.cMoveHorizontalRow].UIGroup)
            {
                ugo.SetActiveRecursively(true);
            }
            */

            AppRoot.Scene.SelectedWallElement = AppRoot.Porch.GetElement(go);
            
            if (AppRoot.Porch.GetElement(go).IsHorizontal)
            {
                ModelBehaviour mb = go.GetComponent(typeof(ModelBehaviour)) as ModelBehaviour;
                mb.Model.DisplayAffordances = true;

                AppRoot.Instance.SetState(AppStateId.EditingHorizontalRow);
            }
            else
            {
                ModelBehaviour mb = go.GetComponent(typeof(ModelBehaviour)) as ModelBehaviour;
                mb.Model.DisplayAffordances = true;

                AppRoot.Instance.SetState(AppStateId.EditingVerticalStud);
            }
        }

        // Side icons
        if (go.name == Constants.cMainWall)
        {
            DisableAllAffordance();
            AppRoot.Instance.SetState(AppStateId.SelectingWallPropertiesIcon);
            AppRoot.Instance.OnUICliked(go, null);
        }
        else if (go.name == Constants.cSidelRightWall)
        {
            DisableAllAffordance();
            AppRoot.Instance.SetState(AppStateId.SelectingWallPropertiesIcon);
            AppRoot.Instance.OnUICliked(go, null);
        }
        else if (go.name == Constants.cSidelLeftWall)
        {
            DisableAllAffordance();
            AppRoot.Instance.SetState(AppStateId.SelectingWallPropertiesIcon);
            AppRoot.Instance.OnUICliked(go, null);
        }

    }

	public override void OnUiAction(string controlName, object param)
    {		
		base.OnUiAction(controlName, param);

		if (controlName == Constants.cEditControl)
		{
            if (AppRoot.Scene.SelectedWallElement != null)
            {
                foreach (GameObject go in AppRoot.Instance.mUIGroups[Constants.cMoveHorizontalRow].UIGroup)
                {
                    go.SetActiveRecursively(true);
                }
            }		
		}
		else if (controlName == Constants.cMoveHorizontalRowDoneControl)
		{
			foreach (GameObject go in AppRoot.Instance.mUIGroups[Constants.cMoveHorizontalRow].UIGroup)
        	{
            	go.SetActiveRecursively(false);
        	}
		}
        else if (controlName == Constants.cRowHeightControl)
        {
            AppRoot.Scene.SelectedWallElement.Offset = (float)param;
        }
        else if (controlName == Constants.cDeleteControl)
        {
            if (AppRoot.Scene.SelectedWallElement != null)
            {
                AppRoot.Scene.SelectedWallElement.Delete();
            }
        }
        else if (controlName == Constants.cCloneControl)
        {
            if (AppRoot.Scene.SelectedWallElement != null)
            {
                DisableAllAffordance();
                AppRoot.Scene.SelectedWallElement.Clone(false);                
                AppRoot.Scene.SelectedModel.DisplayAffordances = true;

            }
        }
        else if (controlName == Constants.сBackButton)
        {
            DisableAllAffordance();
            AppRoot.Instance.SetState(AppStateId.SelectingWallPropertiesIcon);

            foreach (Model md in AppRoot.Porch)
            {                
                md.DisplayAffordances = false;
            }
   
        }
	}
	
	#endregion

    private void DisableAllAffordance()
    {
        foreach (Model md in AppRoot.Porch)
        {
            md.DisplayAffordances = false;
        }
    }

}
