using System;
using System.Collections.Generic;
using UnityEngine;

class AppState_AdjustPorchWallDimensionsUsingSliders : AppState
{
	#region Public data
	#endregion

	#region Private data

	private Vector3 mPorchSize;
    private const float cOutOfView = -100f;

	#endregion

	#region Interface

	public AppState_AdjustPorchWallDimensionsUsingSliders()
		: base(AppStateId.AdjustPorchWallDimensionsUsingSliders)
	{
	}

	#endregion

	#region Implementation of AppState

	public override void OnActivate(object param)
	{
		base.OnActivate(param);

		mPorchSize = AppRoot.Scene.Porch.Size;

        foreach (GameObject go in AppRoot.Instance.mUIGroups[Constants.cAdjustPorchWallDimensionsUsingSliders].UIGroup)
        {
            go.SetActiveRecursively(true);
        }

	}

	public override void OnDeactivate()
	{
		base.OnDeactivate();

        
         foreach (GameObject go in AppRoot.Instance.mUIGroups[Constants.cAdjustPorchWallDimensionsUsingSliders].UIGroup)
         {
             if (go.animation != null)
             {
                 foreach (AnimationState ac in go.animation)
                 {
                     if (ac.name.ToLower().Contains("hide"))
                     {                          
                         //go.animation.Play(ac.name);
                         // !!!!HACK 
                         go.transform.Translate(new Vector3(cOutOfView, 0f, 0f));

                      }
                  }
               }
          }
        
        foreach (GameObject go in AppRoot.Instance.mUIGroups[Constants.cAdjustPorchWallDimensionsUsingSliders].UIGroup)
        {
            go.SetActiveRecursively(false);
        }

		AppRoot.Scene.Porch.DisplayAffordances = false;
	}

    public override void OnUiAction(string controlName, object param)
    {
        base.OnUiAction(controlName, param);

		Vector3 size = AppRoot.Scene.Porch.Size;

		if (controlName == Constants.cWallDimensionWidthSlider)
		{
			size.x = (float)param;
			AppRoot.Scene.Porch.Size = size;
		}
		else if (controlName == Constants.cWallDimensionDepthSlider)
		{
			size.z = (float)param;
			AppRoot.Scene.Porch.Size = size;
		}
		else if (controlName == Constants.cWallDimensionHeightSlider)
		{
			size.y = (float)param;
			AppRoot.Scene.Porch.Size = size;
		}
        else if (controlName == Constants.cWallDimensionDone)
        {
			AppRoot.Scene.Porch.AutoResetWallStudsAndRows();

            // all test cases
            {
                PorchWall wall = null;
                
                // front wall - left side
                //wall = AppRoot.Scene.Porch.GetWall(PorchWallId.Front);
                //wall.AddDoor(2, true, Constants.cDoorSize36 * Constants.In2M); // 30, 32, 36

                // front wall - right side
                //wall = AppRoot.Scene.Porch.GetWall(PorchWallId.Front);
                //wall.AddDoor(2, false, Constants.cDoorSize36 * Constants.In2M);

                // Left wall - left side
                //wall = AppRoot.Scene.Porch.GetWall(PorchWallId.Left);
                //wall.AddDoor(2, true, Constants.cDoorSize36 * Constants.In2M);

                // Left wall - right side
                //wall = AppRoot.Scene.Porch.GetWall(PorchWallId.Left);
                //wall.AddDoor(2, false, Constants.cDoorSize36 * Constants.In2M);

                // Right wall - left side
                //wall = AppRoot.Scene.Porch.GetWall(PorchWallId.Right);
                //wall.AddDoor(2, true, Constants.cDoorSize36 * Constants.In2M);

                // Right wall - right side
                //wall = AppRoot.Scene.Porch.GetWall(PorchWallId.Right);
                //wall.AddDoor(2, false, Constants.cDoorSize36 * Constants.In2M);

                ///////////////////////////////////////////////////////////////
                
                // front wall - left side - 0i stud
                //wall = AppRoot.Scene.Porch.GetWall(PorchWallId.Front);
                //wall.AddDoor(0, true, Constants.cDoorSize36 * Constants.In2M);

                // front wall - right side - last stud
                //wall = AppRoot.Scene.Porch.GetWall(PorchWallId.Front);
                //wall.AddDoor(wall.VStudsCount - 1, false, Constants.cDoorSize36 * Constants.In2M);

                // left wall - left side - 0i stud
                //wall = AppRoot.Scene.Porch.GetWall(PorchWallId.Left);
                //wall.AddDoor(0, true, Constants.cDoorSize36 * Constants.In2M); // 30, 32, 36

                // left wall - right side - last stud
                //wall = AppRoot.Scene.Porch.GetWall(PorchWallId.Left);
                //wall.AddDoor(wall.VStudsCount - 1, false, Constants.cDoorSize36 * Constants.In2M);

                // right wall - left side - last stud
                //wall = AppRoot.Scene.Porch.GetWall(PorchWallId.Right);
                //wall.AddDoor(wall.VStudsCount - 1, true, Constants.cDoorSize36 * Constants.In2M); 

                // right wall - right side - 0i
                //wall = AppRoot.Scene.Porch.GetWall(PorchWallId.Right);
                //wall.AddDoor(0, false, Constants.cDoorSize36 * Constants.In2M); 


                ///////////////////////////////////////////////////////////////

                // front wall - left side - 2i stud
                //wall = AppRoot.Scene.Porch.GetWall(PorchWallId.Front);
                //wall.AddDoor(2, true, Constants.cDoorSize36 * Constants.In2M);

                // front wall - right side - 2i stud
                //wall = AppRoot.Scene.Porch.GetWall(PorchWallId.Front);
                //wall.AddDoor(2, false, Constants.cDoorSize36 * Constants.In2M);

                // left wall - left side - 2i stud
                //wall = AppRoot.Scene.Porch.GetWall(PorchWallId.Left);
                //wall.AddDoor(2, true, Constants.cDoorSize36 * Constants.In2M);

                // left wall - right side - 2i stud
                //wall = AppRoot.Scene.Porch.GetWall(PorchWallId.Left);
                //wall.AddDoor(2, false, Constants.cDoorSize36 * Constants.In2M);

                // right wall - left side - 2i stud
                //wall = AppRoot.Scene.Porch.GetWall(PorchWallId.Right);
                //wall.AddDoor(2, true, Constants.cDoorSize36 * Constants.In2M);

                // right wall - right side - 2i stud
                wall = AppRoot.Scene.Porch.GetWall(PorchWallId.Right);
                wall.AddDoor(2, false, Constants.cDoorSize36 * Constants.In2M);


            }
            
            

            AppRoot.Instance.SetState(AppStateId.SelectingWallPropertiesIcon);
        }
        else if (controlName == Constants.сBackButton)
        {          
            AppRoot.Instance.SetState(AppStateId.ChoosePorchWallLayout);
        }


        if (controlName == Constants.cMenuButton)
        {
            foreach (GameObject go in AppRoot.Instance.mUIGroups[Constants.cAdjustPorchWallDimensionsUsingSliders].UIGroup )
            {
                if (go.animation != null)
                {             
                    foreach (AnimationState ac in go.animation)
                    {
                        if (ac.name.ToLower().Contains("show"))
                        {
                            go.animation.Play(ac.name);
                        }                 
                    }
                }
            }
        }

        
    }

	#endregion
}
