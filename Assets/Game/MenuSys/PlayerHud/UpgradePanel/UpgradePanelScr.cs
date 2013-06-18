using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UpgradePanelScr : MonoBehaviour {
	public PlayerHudMain playerHud;
	public GameObject UpgradeSliderPrefab;
	public UIGrid UpgradeSliderGrid;
	public GridInput grid_in;
	
	Transform current_ability;
	
	public void setAbility(AbilityItem ability){
		current_ability=ability.Ability;
		
		var u_stats=current_ability.GetComponent<UpgradeStats>();
		grid_in.grid_height=u_stats.AvailableUpgrades.Length;
		
		clearGrid();
		
		for(int i =0 ; i<u_stats.AvailableUpgrades.Length; i++){
			var o=NGUITools.AddChild(UpgradeSliderGrid.gameObject, UpgradeSliderPrefab);
			var slider=o.GetComponent<UpgradeSliderScr>();
			slider._camera=playerHud._Camera;
			slider.setStat(u_stats.AvailableUpgrades[i],ability.Stats);
		}
		UpgradeSliderGrid.Reposition();
		grid_in.UpdateGrid();
	}
	
	public void clearGrid(){
		grid_in.ClearGrid();
		UpgradeSliderGrid.Reposition();
	}
}
