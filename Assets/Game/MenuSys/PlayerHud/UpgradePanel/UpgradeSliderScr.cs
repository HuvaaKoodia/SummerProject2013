using UnityEngine;
using System.Collections;

public class UpgradeSliderScr : MonoBehaviour {
	public UICamera _camera;
	public UISlider Slider;
	public UILabel NameLabel,PointsLabel;
	
	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {
		PointsLabel.text=(10*Slider.sliderValue).ToString();
		Slider.ForceUpdate();
	}
	
	public void OnHover(object val){
		//_camera.selectedObjectHighlight=Slider.thumb.gameObject;
		bool v=(bool)val;
		Slider.thumb.SendMessage("OnHover",v,SendMessageOptions.DontRequireReceiver);
	}
	
	
	void OnKey (KeyCode key)
	{
		if (enabled)
		{
			switch (key)
			{
			case KeyCode.LeftArrow:
					Slider.sliderValue-=0.1f;
				break;
			case KeyCode.RightArrow:
					Slider.sliderValue+=0.1f;
				break;
			}
		}
	}
}
