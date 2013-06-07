using UnityEngine;
using System.Collections;

public class PlayerHudPanel : MonoBehaviour {
	
	public PlayerMain Player;
	UIAnchor anchor;
	UISlider hp_slider,mp_slider;
	
	// Use this for initialization
	public void Awake() {
		var a=transform.Find("Anchor");
		anchor=a.GetComponent<UIAnchor>();
		hp_slider=a.transform.Find("HPBAR").GetComponent<UISlider>();
		mp_slider=a.transform.Find("MPBAR").GetComponent<UISlider>();
	}
	
	// Update is called once per frame
	void Update () {
		//DEV.HAX
		//transform.position=Vector3.zero;
		
		hp_slider.sliderValue=Player.HP/100f;
		mp_slider.sliderValue=Player.MP/100f;
	}
	
	public void setHudOriantation(UIAnchor.Side side){
		anchor.side=side;
	}
}
