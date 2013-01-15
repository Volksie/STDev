using System;
using System.Collections.Generic;
using UnityEngine;

class AppState_SelectingWallPropertiesIcon : AppState
{
	#region Public data
	#endregion

	#region Private data
    private string mSelectedWall;
	#endregion

	#region Interface

    public AppState_SelectingWallPropertiesIcon()
        : base(AppStateId.SelectingWallPropertiesIcon)
	{
	}

	#endregion

	#region Implementation of AppState

	public override void OnActivate(object param)
	{
		base.OnActivate(param);
        
		AppRoot.Scene.Porch.WallIconsVisible = true;
	}

	public override void OnDeactivate()
	{
		base.OnDeactivate();

        foreach (GameObject go in AppRoot.Instance.mUIGroups[Constants.cWallSections].UIGroup)
        {
            go.SetActiveRecursively(false);
        }
		
		foreach (GameObject go in AppRoot.Instance.mUIGroups[Constants.cSideWallLength].UIGroup)
        {
            go.SetActiveRecursively(false);
        }

		AppRoot.Scene.Porch.WallIconsVisible = false;
	}

    public override void OnSectionClick(GameObject go)
    {        
        if (AppRoot.Porch.GetElement(go) != null)
        {
            
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

    }

    public override void OnUiAction(string controlName, object param)
    {
        base.OnUiAction(controlName, param);

        UISelectWall(controlName);

        if (controlName == Constants.cMainWallPropertiesDoneControl)
        {
            //AppRoot.Instance.SetState(AppStateId.SelectingHorizontalVertical);  
            AppRoot.Instance.SetState(AppStateId.EditingDoor);
        }
        else if (controlName == Constants.cSideWallLengthDoneContol)
        {
            if (AppRoot.Scene.SelectedWallId == PorchWallId.Left ||
                AppRoot.Scene.SelectedWallId == PorchWallId.Right)
            {
                DeactivateUI();

                foreach (GameObject go in AppRoot.Instance.mUIGroups[Constants.cWallSections].UIGroup)
                {
                    go.SetActiveRecursively(true);
                }
            }
        }
        if (controlName == Constants.cSideWallLengthContol)
        {
            AppRoot.Scene.Porch.SetWallLength(AppRoot.Scene.SelectedWallId, (float)param);
        }
        else if (controlName == Constants.cHorizontalFramesControl)
        {
            AppRoot.Scene.Porch.SetWallHRowsCount(AppRoot.Scene.SelectedWallId, (int)param);
        }
        else if (controlName == Constants.cVerticalFramesControl)
        {
            AppRoot.Scene.Porch.SetWallVStudsCount(AppRoot.Scene.SelectedWallId, (int)param);
        }
        else if (controlName == Constants.сBackButton)
        {
            AppRoot.Instance.SetState(AppStateId.AdjustPorchWallDimensionsUsingSliders);
        }
      
    }

	private void DeactivateUI()
	{
		foreach (GameObject go in AppRoot.Instance.mUIGroups[Constants.cWallSections].UIGroup)
        {
            go.SetActiveRecursively(false);
        }
		
		foreach (GameObject go in AppRoot.Instance.mUIGroups[Constants.cSideWallLength].UIGroup)
        {
            go.SetActiveRecursively(false);
        }

        /*
        foreach (GameObject go in AppRoot.Instance.mUIGroups[Constants.cEditingHorizontalRow].UIGroup)
        {
            go.SetActiveRecursively(false);
        }

        foreach (GameObject go in AppRoot.Instance.mUIGroups[Constants.cMoveHorizontalRow].UIGroup)
        {
            go.SetActiveRecursively(false);
        }

        foreach (GameObject go in AppRoot.Instance.mUIGroups[Constants.cEditingVericalStud].UIGroup)
        {
            go.SetActiveRecursively(false);
        }

        foreach (GameObject go in AppRoot.Instance.mUIGroups[Constants.cMoveVerticalStud].UIGroup)
        {
            go.SetActiveRecursively(false);
        }
        */

	}

	#endregion

    private void UISelectWall(string controlName)
    {
        // Display warning box
        if (controlName == Constants.cMainWall ||
             controlName == Constants.cSidelLeftWall ||
             controlName == Constants.cSidelRightWall
            )
        {
            foreach (GameObject go in AppRoot.Instance.mUIGroups[Constants.cWallWarningDialog].UIGroup)
            {
                go.SetActiveRecursively(true);
            }

            mSelectedWall = controlName;

        }

        if (controlName == Constants.cWarningDialogCancel)
        {
            foreach (GameObject go in AppRoot.Instance.mUIGroups[Constants.cWallWarningDialog].UIGroup)
            {
                go.SetActiveRecursively(false);
            }
        }

        if (controlName == Constants.cWarningDialogContinue)
        {

            foreach (GameObject go in AppRoot.Instance.mUIGroups[Constants.cWallWarningDialog].UIGroup)
            {
                go.SetActiveRecursively(false);
            }

            controlName = mSelectedWall;

            if (controlName == Constants.cMainWall)
            {
                AppRoot.Scene.SelectedWallId = PorchWallId.Front;

                DeactivateUI();

                foreach (GameObject go in AppRoot.Instance.mUIGroups[Constants.cWallSections].UIGroup)
                {
                    go.SetActiveRecursively(true);
                }

            }
            else if (controlName == Constants.cSidelLeftWall)
            {
                AppRoot.Scene.SelectedWallId = PorchWallId.Left;

                DeactivateUI();

                foreach (GameObject go in AppRoot.Instance.mUIGroups[Constants.cSideWallLength].UIGroup)
                {
                    go.SetActiveRecursively(true);
                }

                //AppRoot.Instance.SetState(AppStateId.SideWallLength);
            }
            else if (controlName == Constants.cSidelRightWall)
            {
                AppRoot.Scene.SelectedWallId = PorchWallId.Right;

                DeactivateUI();

                foreach (GameObject go in AppRoot.Instance.mUIGroups[Constants.cSideWallLength].UIGroup)
                {
                    go.SetActiveRecursively(true);
                }
                //AppRoot.Instance.SetState(AppStateId.SideWallLength);
            }
        }
        
    }

}
