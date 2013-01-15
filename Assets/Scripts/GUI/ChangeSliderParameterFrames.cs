using UnityEngine;
using System.Collections;

public class ChangeSliderParameterFrames : MonoBehaviour {
	
	public UISlider verticalSlider;
	public UISlider horizontalSlider;
	
    public UILabel minVerticalLabel;
    public UILabel maxVerticalLabel;

    public UILabel minHorizontalLabel;
    public UILabel maxHorizontalLabel;

    public float mMinValue;
    public float mMaxValue;

    private const float cInchToMeterMult = 0.0254f;
	
	void Start()
	{
		OnEnable();
	}
	
	// Use this for initialization
	void OnEnable () {
			
        if( gameObject.name.ToLower().Contains("vertical"))
        {
			/*
            mMinValue = Constants.MinVerticalStuds;
            mMaxValue = Constants.MaxVerticalStuds;
			*/
			
			mMinValue = AppRoot.Scene.Porch.GetWall(AppRoot.Scene.SelectedWallId).MinVStudsCount;
			mMaxValue = AppRoot.Scene.Porch.GetWall(AppRoot.Scene.SelectedWallId).MaxVStudsCount;
			 
            minVerticalLabel.text = mMinValue.ToString("0");
            maxVerticalLabel.text = mMaxValue.ToString("0");
			
			//Debug.Log( "Vertical" + AppRoot.Scene.SelectedWall.VStudsCount );
			//Debug.Log( "VertivalMax " + mMaxValue );
			
			verticalSlider.sliderValue = (float)AppRoot.Scene.SelectedWall.VStudsCount / ( mMaxValue - mMinValue);
			OnSliderChange( verticalSlider.sliderValue );

        }

        if (gameObject.name.ToLower().Contains("horizontal"))
        {
            mMinValue = AppRoot.Scene.Porch.GetWall(AppRoot.Scene.SelectedWallId).MinHRowsCount;
            mMaxValue = AppRoot.Scene.Porch.GetWall(AppRoot.Scene.SelectedWallId).MaxHRowsCount;

            minHorizontalLabel.text = mMinValue.ToString("0");
            maxHorizontalLabel.text = mMaxValue.ToString("0");
		
			//Debug.Log( "Horizotal" + AppRoot.Scene.SelectedWall.HRowsCount );
			//Debug.Log( "HorizontalMax " + mMaxValue );
			
			horizontalSlider.sliderValue = (float)AppRoot.Scene.SelectedWall.HRowsCount / ( mMaxValue - mMinValue);
			OnSliderChange( horizontalSlider.sliderValue );
        }
				
       
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnSliderChange(float value)
    {
        UILabel uiLabel = GetComponent(typeof(UILabel)) as UILabel;
        uiLabel.text = ( mMinValue + value * ( mMaxValue - mMinValue)).ToString("0");

        if (AppRoot.Instance != null)
        {
           AppRoot.Instance.OnUICliked(this.gameObject, System.Convert.ToInt32(uiLabel.text));
        }

    }
}
