using UnityEngine;
using System.Collections;

public class UpgradeSliderScr : MonoBehaviour {
	public UISlider Slider;
	public UILabel NameLabel,PointsLabel;
	
	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {
		PointsLabel.text=(10*Slider.sliderValue).ToString();
		Slider.ForceUpdate();
	}
}
