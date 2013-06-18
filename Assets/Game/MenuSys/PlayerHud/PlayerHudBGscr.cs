using UnityEngine;
using System.Collections;

public class PlayerHudBGscr : MonoBehaviour {
	
	public UILabel cost_label,resource_label,name_label;
	
	public void SetName(string name){
		name_label.text=name;
	}
	
	public void SetCost(int cost){
		cost_label.text="Cost [FF4C61]"+cost;
	}
	
	public void SetResources(int r){
		resource_label.text="Resources [FFF242]"+r;
	}
}
