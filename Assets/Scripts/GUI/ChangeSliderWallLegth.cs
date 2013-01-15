using UnityEngine;
using System.Collections;

public class ChangeSliderWallLegth : MonoBehaviour
{
	
	public UISlider lengthSlider;
	
    public UILabel minSideWallLength;
    public UILabel maxSideWallLength;

    public float mMinValue = 0f;
    public float mMaxValue = 1f;

    private const float cInchToMeterMult = 0.0254f;
    private const float cMeterToFeet = 3.2808f;
	
	// Use this for initialization
	void OnEnable () {
		
		mMinValue = AppRoot.Scene.Porch.GetWall(AppRoot.Scene.SelectedWallId).MinLength * cMeterToFeet;
		mMaxValue = AppRoot.Scene.Porch.GetWall(AppRoot.Scene.SelectedWallId).MaxLength * cMeterToFeet;
		
        //mMinValue = Constants.MinSideWallLength * cMeterToFeet;
        //mMaxValue = Constants.MaxSideWallLength * cMeterToFeet;

        minSideWallLength.text = mMinValue.ToString("0") + "'";
        maxSideWallLength.text = mMaxValue.ToString("0") + "'";        			
		
		lengthSlider.sliderValue = ( AppRoot.Scene.SelectedWall.Length * cMeterToFeet - mMinValue )/ ( mMaxValue  - mMinValue );
				
		//OnSliderChange( lengthSlider.sliderValue );
		
        /*
        if (gameObject.name.ToLower().Contains("horizontal"))
        {
            mMinValue = Constants.MinHorizontalRows;
            mMaxValue = Constants.MaxHorizontalRows;

            minHorizontalLabel.text = mMinValue.ToString("0");
            maxHorizontalLabel.text = mMaxValue.ToString("0");

        }
         */
       
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnSliderChange(float value)
    {
        UILabel uiLabel = GetComponent(typeof(UILabel)) as UILabel;
        float feets = System.Convert.ToSingle((mMinValue + value * (mMaxValue - mMinValue)).ToString("0.000000"));
        float inches = (mMinValue + value * (mMaxValue - mMinValue)) -
                        Mathf.Floor((mMinValue + value * (mMaxValue - mMinValue)));

        uiLabel.text = Mathf.Floor((mMinValue + value * (mMaxValue - mMinValue))).ToString("0") + "'" + (inches * 12f).ToString("0") + "''";

        //Debug.Log("Ok slider " + gameObject.name + " " + uiLabel.text );

        if (AppRoot.Instance != null)
        {
            //AppRoot.Instance.OnUICliked(this.gameObject, System.Convert.ToSingle(uiLabel.text) / System.Convert.ToSingle(cMeterToFeet));
            AppRoot.Instance.OnUICliked(this.gameObject, feets / cMeterToFeet);
        }

    }
}
