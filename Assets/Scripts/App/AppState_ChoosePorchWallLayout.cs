using System;
using System.Collections.Generic;
using UnityEngine;

class AppState_ChoosePorchWallLayout : AppState
{
	#region Public data
	#endregion

	#region Private data
	#endregion

	#region Interface

	public AppState_ChoosePorchWallLayout()
		: base(AppStateId.ChoosePorchWallLayout)
	{
	}

	#endregion

	#region Implementation of AppState

	public override void OnActivate(object param)
	{
		base.OnActivate(param);

		// clear the scene
		AppRoot.Scene.Clear();
		AppRoot.Scene.CameraTarget = AppRoot.Instance.transform;
		AppRoot.Scene.DisplayMode = ModelDisplayModeId.Default;

		// GUI
        foreach (GameObject go in AppRoot.Instance.mUIGroups[Constants.cChoosePorchWallLayout].UIGroup)
        {
            go.SetActiveRecursively( true );
        }

        foreach (GameObject go in AppRoot.Instance.mUIGroups[Constants.cChoosePorchWallLayout].UIGroup)
        {
            if (go.animation != null)
            {
                int animationIndex = 0;
                foreach (AnimationState ac in go.animation)
                {
                    if ( ac.name.ToLower().Contains("show") )
                    {
                        go.animation.Play(ac.name);
                    }
                    animationIndex++;
                }
            }
        }
	}

	public override void OnDeactivate()
	{
		base.OnDeactivate();

        foreach (GameObject go in AppRoot.Instance.mUIGroups[Constants.cChoosePorchWallLayout].UIGroup)
        {
            if (go.animation != null)
            {                
                go.animation.Play();
            }
        }
	}

    public override void OnUiAction(string controlName, object param)
	{
		base.OnUiAction(controlName, param);

		if (controlName == Constants.cMenuPreConfig)
		{
			AppRoot.Scene.Porch.Layout = (PorchWallLayoutId)param;
			AppRoot.Scene.Porch.DisplayAffordances = true;
			AppRoot.Scene.ResetCamera();
			AppRoot.Scene.CameraMode = CameraModeId.RotateZoom;
			AppRoot.StateId = AppStateId.AdjustPorchWallDimensionsUsingSliders;

            // GUI
            foreach (GameObject go in AppRoot.Instance.mUIGroups[Constants.cChoosePorchWallLayout].UIGroup)
            {
                go.SetActiveRecursively(false);
            }

		}

	}

    #endregion
}
