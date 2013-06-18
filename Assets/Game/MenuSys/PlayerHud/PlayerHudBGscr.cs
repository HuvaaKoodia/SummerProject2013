using UnityEngine;
using System.Collections;

public class PlayerHudBGscr : MonoBehaviour {
	
	public UILabel cost_label,resource_label;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update() {
	
	}
	
	public void SetCost(int cost){
		cost_label.text="Cost [FF4C61]"+cost;
	}
	
	public void SetResources(int r){
		resource_label.text="Resources [FFF242]"+r;
	}
}
