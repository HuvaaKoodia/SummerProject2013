using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UpgradePanelScr : MonoBehaviour {
	public PlayerHudMain playerHud;
	public GameObject UpgradeSliderPrefab;
	public UIGrid UpgradeSliderGrid;
	
	
	// Use this for initialization
	void Awake () {
		for(int i =0 ; i<4; i++){
			var o=NGUITools.AddChild(UpgradeSliderGrid.gameObject, UpgradeSliderPrefab);
			o.GetComponent<UpgradeSliderScr>()._camera=playerHud._Camera;
		}
		UpgradeSliderGrid.Reposition();
	}
	
	// Update is called once per frame
	void Update () {
		
	
	}
}
