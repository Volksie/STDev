using UnityEngine;
using System;
using System.Collections.Generic;

public partial class AppRoot : MonoBehaviour
{
	#region Public data

	public static AppRoot Instance
	{
		get
		{
			return mInstance;
		}
	}

	public static Porch Porch
	{
		get
		{
			return mInstance.mScene.Porch;
		}
	}

	public static Scene Scene
	{
		get
		{
			return mInstance.mScene;
		}
	}

	public static AppState State
	{
		get
		{
			return mInstance.mCurState;
		}
	}

	public static AppStateId StateId
	{
		get
		{
			return mInstance.mCurState.Id;
		}
		set
		{
			mInstance.SetState(value);
		}
	}

	#endregion

	#region Private data

	private Dictionary<AppStateId, AppState> mStates = new Dictionary<AppStateId, AppState>();
	private Scene mScene = new Scene();
	private AppState mCurState;

	private static AppRoot mInstance;

	#endregion

	#region Interface

	public void SetState(AppStateId id)
	{
		SetState(id, null);
	}

	public void SetState(AppStateId id, object param)
	{
		if (mCurState.Id != id)
		{
			AppState newState;

			if (!mStates.TryGetValue(id, out newState))
			{
				newState = mStates[AppStateId.Default];
			}
			if (newState != mCurState)
			{                
				mCurState.OnDeactivate();
				mCurState = newState;
				mCurState.OnActivate(param);

                if (mCurState.Id != AppStateId.ChoosePorchWallLayout)
                {
                    foreach (GameObject go in AppRoot.Instance.mProductMenu.UIGroup)
                    {                      
                        go.SetActiveRecursively(true);
                    }
                    
                    HideProductPannel();
                    /*
                    foreach (GameObject go in AppRoot.Instance.mProductMenu.UIGroup )
                    {
                        if (go.animation != null)
                        {                            
                            go.animation.Play();
                        }
                    }
                     */
                    
                }

			}
		}
	}

	#endregion

	#region Implementation of MonoBehaviour

	void Start()
	{
		if (mInstance != null)
		{
			Debug.Log("Cannot create second instance of AppRoot.");
			throw new Exception();
		}

		mInstance = this;

		Initialize();
		InitUI();
		InitStates();
	}
	
	void Update()
	{
		mScene.Update();
	}

	#endregion

	#region Implementation

	private void Initialize()
	{
		mScene.Initialize();
	}

	private void InitStates()
	{
		mStates[AppStateId.Default] = new AppState_Default();
		mStates[AppStateId.ChoosePorchWallLayout] = new AppState_ChoosePorchWallLayout();
		mStates[AppStateId.AdjustPorchWallDimensionsUsingSliders] = new AppState_AdjustPorchWallDimensionsUsingSliders();
		mStates[AppStateId.AdjustPorchWallDimensionsUsingAffordances] = new AppState_AdjustPorchWallDimensionsUsingAffordances();
		mStates[AppStateId.AdjustPorchWallDimensionsUsingNumericInput] = new AppState_AdjustPorchWallDimensionsUsingNumericInput();
        mStates[AppStateId.SelectingWallPropertiesIcon] = new AppState_SelectingWallPropertiesIcon();
        mStates[AppStateId.SelectingHorizontalVertical] = new AppState_SelectingHorizontalVerticalc();
		mStates[AppStateId.EditingHorizontalRow] = new AppState_EditingHorizontalRow();		
		mStates[AppStateId.EditingVerticalStud] = new AppState_EditingVerticalStud();
        mStates[AppStateId.EditingDoor] = new AppState_EditingDoor();	
		
		mCurState = mStates[AppStateId.ChoosePorchWallLayout];
		mCurState.OnActivate(null);
	}

	#endregion
}
