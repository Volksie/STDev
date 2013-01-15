using UnityEngine;
using System.Collections;

public class ChangeSliderParameter : MonoBehaviour {

    public UILabel minWidthLabel;
    public UILabel maxWidthLabel;

    public UILabel minDepthLabel;
    public UILabel maxDepthLabel;

    public UILabel minHeightLabel;
    public UILabel maxHeightLabel;


    public float mMinValue;
    public float mMaxValue;

    private const float cInchToMeterMult = 0.0254f;
    private const float cMeterToFeet = 3.2808f;

	// Use this for initialization
	void Start () {
        
        if( gameObject.name.ToLower().Contains("width"))
        {
            mMinValue = Constants.PorchWidthMin * cMeterToFeet;
            mMaxValue = Constants.PorchWidthMax * cMeterToFeet;

            minWidthLabel.text = mMinValue.ToString("0") + "'";
            maxWidthLabel.text = mMaxValue.ToString("0") + "'";

        }

        if (gameObject.name.ToLower().Contains("depth"))
        {
            mMinValue = Constants.PorchDepthMin * cMeterToFeet;
            mMaxValue = Constants.PorchDepthMax * cMeterToFeet;

            minDepthLabel.text = mMinValue.ToString("0") + "'";
            maxDepthLabel.text = mMaxValue.ToString("0") + "'";

        }

        if (gameObject.name.ToLower().Contains("height"))
        {
            mMinValue = Constants.PorchHeightMin * cMeterToFeet;
            mMaxValue = Constants.PorchHeightMax * cMeterToFeet;

            minHeightLabel.text = mMinValue.ToString("0") + "'";
            maxHeightLabel.text = mMaxValue.ToString("0") + "'";

        }

        OnSliderChange( 0f );

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnSliderChange(float value)
    {
        UILabel uiLabel = GetComponent(typeof(UILabel)) as UILabel;

        float feets = System.Convert.ToSingle((mMinValue + value * (mMaxValue - mMinValue)).ToString("0.00"));
        float inches =  (mMinValue + value * (mMaxValue - mMinValue)) -
                        Mathf.Floor( (mMinValue + value * (mMaxValue - mMinValue)) );
        
        uiLabel.text = Mathf.Floor(( mMinValue + value * ( mMaxValue - mMinValue))).ToString("0") + "'" + (inches * 12f).ToString("0") + "''";
                
        //Debug.Log("Ok slider " + gameObject.name + " " + uiLabel.text );

        if (AppRoot.Instance != null)
        {
            //AppRoot.Instance.OnUICliked(this.gameObject, System.Convert.ToSingle(uiLabel.text) / System.Convert.ToSingle(cMeterToFeet));
            AppRoot.Instance.OnUICliked(this.gameObject,  feets / cMeterToFeet);
        }

    }
}
