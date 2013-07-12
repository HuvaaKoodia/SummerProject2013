using UnityEngine;
using System.Collections;

public class PlayerHudBGscr : MonoBehaviour {
	
	public UILabel cost_label,resource_label,name_label;
	public UISprite background;
	Color base_color;
	
	void Start(){
		base_color=resource_label.color;
	}
	
	public void SetName(string name){
		name_label.text=name;
	}
	
	public void SetCost(int cost){
		cost_label.text="Cost [FF4C61]"+cost;
	}
	
	public void SetResources(int r){
		resource_label.text="Resources [FFF242]"+r;
	}
	
	public void NoMoniesWarning(){
		StopCoroutine("blinkResourceLabel");
		StartCoroutine("blinkResourceLabel");
	}
	
	IEnumerator blinkResourceLabel(){
		bool change=true;

		for (int i=0;i<10;i++){
			if (change)
				resource_label.color=Color.red;
			else
				resource_label.color=base_color;
				
			change=!change;
			yield return new WaitForSeconds(0.1f);
		}
	}

	public void setPlayer (PlayerData playerData)
	{
		var c=playerData.color;
		background.color=new Color(c.r,c.g,c.b,background.color.a);
	}
}
