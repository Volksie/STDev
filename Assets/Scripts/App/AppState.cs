using UnityEngine;
using System;
using System.Collections;

public enum AppStateId
{
	Default, 
	ChoosePorchWallLayout, 
	AdjustPorchWallDimensionsUsingSliders,
	AdjustPorchWallDimensionsUsingAffordances,
	AdjustPorchWallDimensionsUsingNumericInput,
    SelectingWallPropertiesIcon,
    SelectingHorizontalVertical,
	EditingHorizontalRow,	
	EditingVerticalStud,
    EditingDoor
}

public class AppState
{
	#region Public data

	public readonly AppStateId Id;

	#endregion

	#region Private data



	#endregion

	#region Interface

	protected AppState(AppStateId id)
	{
		Id = id;
	}

	public virtual void OnActivate(object param)
	{
		Debug.Log("State activated: " + Id);
	}

	public virtual void OnDeactivate()
	{
	}

	public virtual void OnUiAction(string controlName, object param)
	{
	}

    public virtual void OnSectionClick(GameObject go)
    {
    }

	#endregion

	#region Implementation
	#endregion
}
