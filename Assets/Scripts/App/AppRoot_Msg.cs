using UnityEngine;
using System;
using System.Collections.Generic;

public partial class AppRoot : IModelMsgTarget
{
    #region PrivateData
    private bool mProductPanelVisible = true;
    #endregion

    #region Implementation of IModelMsgTarget

    public void OnModelClicked(GameObject go, object param )
	{
      
	}

    public void HideProductPannel()
    {
        if (mProductPanelVisible)
        {
            foreach (GameObject goa in AppRoot.Instance.mProductMenu.UIGroup)
            {
                if (goa.animation != null)
                {
                    foreach (AnimationState ac in goa.animation)
                    {
                        if (ac.name.ToLower().Contains("hide"))
                        {
                            goa.animation.Play(ac.name);
                        }
                    }
                }
            }

            mProductPanelVisible = false;
        }
    }

    public void OnUICliked(GameObject go, object param)
    {
        Debug.Log(go + " " + param);

        if (go.name == Constants.cMenuButton )
        {
            if (!mProductPanelVisible)
            {
                foreach (GameObject goa in AppRoot.Instance.mProductMenu.UIGroup)
                {
                    if (goa.animation != null)
                    {
                        foreach (AnimationState ac in goa.animation)
                        {
                            if (ac.name.ToLower().Contains("show"))
                            {
                                goa.animation.Play(ac.name);
                            }
                        }
                    }
                }

                mProductPanelVisible = true;

            }
            else
            {
                HideProductPannel();

            }
        }
		else if (go.name == Constants.cScreenTightControl)
		{
			AppRoot.Scene.DisplayMode = ModelDisplayModeId.ScreenTight;
		}
		else if (go.name == Constants.cMiniTrackControl)
		{
			AppRoot.Scene.DisplayMode = ModelDisplayModeId.MiniTrack;
		}
		else if (go.name == Constants.cFastTrackControl)
		{
			AppRoot.Scene.DisplayMode = ModelDisplayModeId.FastTrack;
		}
		else if (go.name == Constants.сBare)
		{
			AppRoot.Scene.DisplayMode = ModelDisplayModeId.Default;
		}

		mCurState.OnUiAction(go.name, param);
        mCurState.OnSectionClick(go);
	 }

	#endregion
}
