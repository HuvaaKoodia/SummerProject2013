using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UpgradePanelScr : MonoBehaviour {
	public PlayerHudMain playerHud;
	public GameObject UpgradeSliderPrefab;
	public UIGrid UpgradeSliderGrid;
	public GridInput grid_in;
	public float row_offset;
	
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
		UpgradeSliderGrid.RepositionHard();
		grid_in.UpdateGrid();
		int gfx_offset=0;
		foreach(var g in grid_in.Grid){
			
			g.transform.localPosition += new Vector3(52f+(gfx_offset*row_offset),0f,0f);
			gfx_offset++;
		}
		UpgradeSliderGrid.SkipStartReposition();
	}
	
	public void clearGrid(){
		grid_in.ClearGrid();
		UpgradeSliderGrid.RepositionHard();
	}
}
