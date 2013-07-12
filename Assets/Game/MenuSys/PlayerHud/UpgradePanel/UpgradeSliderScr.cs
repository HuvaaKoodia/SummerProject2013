 using UnityEngine;
using System.Collections;

public class UpgradeSliderScr : MonoBehaviour{
	public UICamera _camera;
	public UISlider Slider;
	public UILabel NameLabel,PointsLabel;
	
	UpgradeStatContainer abilityStats;
	UpgradeStat stat;
	
	// Use this for initialization
	void Start () {
		Slider.onValueChange+=OnSliderValueChange;
	}
	
	// Update is called once per frame
	void Update () {
		PointsLabel.text="+ "+(10*Slider.sliderValue).ToString();
		//Slider.ForceUpdate();
		
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
	
	void OnSliderValueChange(float val){
		if (abilityStats!=null){
			abilityStats.Data[stat]=(int)Mathf.Ceil(Slider.sliderValue*10);
		}
	}
	
	public void setStat(UpgradeStat stat,UpgradeStatContainer stats){
		this.stat=stat;
		abilityStats=stats;
		//update to current value
		Slider.sliderValue=abilityStats.Data[stat]/10f;
		NameLabel.text=stat.ToString();
	}
}
