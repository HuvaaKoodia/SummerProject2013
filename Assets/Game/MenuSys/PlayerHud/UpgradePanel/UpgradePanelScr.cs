using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UpgradePanelScr : MonoBehaviour {
	public PlayerHudMain playerHud;
	public GameObject UpgradeSliderPrefab;
	public UIGrid UpgradeSliderGrid;
	public GridInput grid_in;
	
	Transform current_ability;
	
	public void setAbility(AbilityItem item){
		current_ability=item.Ability;
		
		var u_stats=current_ability.GetComponent<UpgradeStats>();
		grid_in.grid_height=u_stats.AvailableUpgrades.Length;
		
		clearGrid();
		
		for(int i =0 ; i<u_stats.AvailableUpgrades.Length; i++){
			var o=NGUITools.AddChild(UpgradeSliderGrid.gameObject, UpgradeSliderPrefab);
			var slider=o.GetComponent<UpgradeSliderScr>();
			slider._camera=playerHud._Camera;
			slider.setStat(u_stats.AvailableUpgrades[i],item.Stats);
		}
		UpgradeSliderGrid.Reposition();
		grid_in.UpdateGrid();
	}
	
	public void clearGrid(){
		grid_in.ClearGrid();
		UpgradeSliderGrid.Reposition();
	}
	
	bool button_ok=true;
	
	void Update(){
		
		if (button_ok){
			if (Input.GetAxis("DPad_XAxis_"+playerHud.playerData.controllerNumber)>0){
				foreach (var g in grid_in.Grid){
					var slider=g.gameObject.GetComponent<UpgradeSliderScr>();
					slider.Slider.sliderValue+=0.1f;
					button_timer();
				}
			}
			
			if (Input.GetAxis("DPad_XAxis_"+playerHud.playerData.controllerNumber)<0){
				foreach (var g in grid_in.Grid){
					var slider=g.gameObject.GetComponent<UpgradeSliderScr>();
					slider.Slider.sliderValue-=0.1f;
					button_timer();
				}
			}
		}
		
		if (Input.GetAxis("DPad_YAxis_"+playerHud.playerData.controllerNumber)>0){
			foreach (var g in grid_in.Grid){
				var slider=g.gameObject.GetComponent<UpgradeSliderScr>();
				slider.Slider.sliderValue=1;
			}
		}
		
		if (Input.GetAxis("DPad_YAxis_"+playerHud.playerData.controllerNumber)<0){
			foreach (var g in grid_in.Grid){
				var slider=g.gameObject.GetComponent<UpgradeSliderScr>();
				slider.Slider.sliderValue=0;
			}
		}
		
		
	}
	
	
	void button_timer(){
		StartCoroutine(buttonDelay());
	}
	
	IEnumerator buttonDelay(){
		button_ok=false;
		yield return new WaitForSeconds(0.1f);
		button_ok=true;
	}
	
}
