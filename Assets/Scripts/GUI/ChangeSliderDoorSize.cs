using UnityEngine;
using System.Collections;

public class ChangeSliderDoorSize : MonoBehaviour
{
	
	public UISlider doorSizeSlider;
	
    private const float cInchToMeterMult = 0.0254f;
    private const float cMeterToFeet = 3.2808f;

    private const float cDoorSize1 = 30f;
    private const float cDoorSize2 = 32f;
    private const float cDoorSize3 = 36f;

	// Use this for initialization
	void OnEnable () {

        doorSizeSlider.sliderValue = 0f;		
       
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnSliderChange(float value)
    {
        if (AppRoot.Instance != null)
        {
            if (value == 0f)
            {
                AppRoot.Instance.OnUICliked(this.gameObject, cDoorSize1 * cInchToMeterMult);
            }
            else if (value == 0.5f)
            {
                AppRoot.Instance.OnUICliked(this.gameObject, cDoorSize2 * cInchToMeterMult);
            }
            else if (value == 1f)
            {
                AppRoot.Instance.OnUICliked(this.gameObject, cDoorSize3 * cInchToMeterMult);
            }
        }
    }
}
