using UnityEngine;
using System.Collections;

public class UpdatePoints : MonoBehaviour {
	public UISlider slider;
	public UILabel label;
	
	// Use this for initialization
	void Start () {
	slider = transform.Find("SliderOfSkill").GetComponent<UISlider>();
		
	label = transform.Find("PtsLabel").GetComponent<UILabel>();
	}
	
	// Update is called once per frame
	void Update () {
	label.text=(10*slider.sliderValue).ToString();
		slider.ForceUpdate();
	}
}
