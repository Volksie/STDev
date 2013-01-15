using UnityEngine;
using System.Collections;

public class ChangeSliderRowHeight : MonoBehaviour
{
	
	public UISlider rowHeightSlider;
	
    public UILabel minRowHeight;
    public UILabel maxRowHeight;

    public float mMinValue = 0f;
    public float mMaxValue = 1f;

    private const float cInchToMeterMult = 0.0254f;
    private const float cMeterToFeet = 3.2808f;
	
	// Use this for initialization
	void OnEnable () {
				
        mMinValue = AppRoot.Scene.SelectedWallElement.MinOffset;
        mMaxValue = AppRoot.Scene.SelectedWallElement.MaxOffset;
        
        rowHeightSlider.sliderValue = (AppRoot.Scene.SelectedWallElement.Offset - mMinValue) / (mMaxValue - mMinValue);  
		//rowHeightSlider.sliderValue = (float)AppRoot.Scene.SelectedWall.Length / ( mMaxValue - mMinValue);
		OnSliderChange( rowHeightSlider.sliderValue );
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnSliderChange(float value)
    {
        float feets = System.Convert.ToSingle((mMinValue + value * (mMaxValue - mMinValue)).ToString("0.000000"));
        float inches = (mMinValue * cMeterToFeet + value * (mMaxValue * cMeterToFeet - mMinValue * cMeterToFeet)) -
                        Mathf.Floor((mMinValue * cMeterToFeet + value * (mMaxValue * cMeterToFeet - mMinValue * cMeterToFeet )));
				
        minRowHeight.text = Mathf.Floor((mMinValue * cMeterToFeet + value * (mMaxValue * cMeterToFeet - mMinValue * cMeterToFeet ))).ToString("0") + "'" + (inches * 12f).ToString("0") + "''";
		maxRowHeight.text = Mathf.Floor((mMinValue * cMeterToFeet + (1f - value ) * (mMaxValue * cMeterToFeet - mMinValue * cMeterToFeet))).ToString("0") + "'" + (inches * 12f).ToString("0") + "''";
		        
        //AppRoot.Scene.SelectedWallElement.Offset = feets;

        if (AppRoot.Instance != null)
        {
            //AppRoot.Instance.OnUICliked(this.gameObject, System.Convert.ToSingle(uiLabel.text) / System.Convert.ToSingle(cMeterToFeet));
            AppRoot.Instance.OnUICliked(this.gameObject, feets );
        }

    }
}
