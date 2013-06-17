using UnityEngine;
using System.Collections;

public enum AbilityMenuState{Bar,Panel,Upgrade,Ready}

public class PlayerHudMain : MonoBehaviour {
	
	public PlayerMain Player;
	public UICamera _Camera;
	public AbilityMenuState state;
	
	public UpgradePanelScr UpgradePanel;
	public GameObject ShopPanel;
	public GridInput AbilityBarGrid,AbilityPanelGrid,UpgradeGrid;
	
	UIAnchor anchor;
	UISlider hp_slider,mp_slider;
	ItemContainerMain swap_item;
	
	// Use this for initialization
	public void Awake() {
		var a=transform.Find("Anchor");
		anchor=a.GetComponent<UIAnchor>();
		hp_slider=a.transform.Find("HPBAR").GetComponent<UISlider>();
		mp_slider=a.transform.Find("MPBAR").GetComponent<UISlider>();
	}
	
	public void Start(){
		changeState(AbilityMenuState.Bar);
	}
	
	// Update is called once per frame
	void Update () {
		//game
		hp_slider.sliderValue=Player.HP/100f;
		mp_slider.sliderValue=Player.MP/100f;
		
		//input
		if (state!=AbilityMenuState.Ready){
			if (Input.GetButtonDown("A_"+Player.controllerNumber)){
				if (state==AbilityMenuState.Bar){
					changeState(AbilityMenuState.Panel);
				}
				else
				if (state==AbilityMenuState.Panel){
					//select item and change to current slot
					GetSelectedBar().Ability=GetSelectedPanel().Ability;
					changeState(AbilityMenuState.Bar);
				}
			}
			
			if (Input.GetButtonDown("B_"+Player.controllerNumber)){
				changeState(AbilityMenuState.Bar);
			}
			
			if (Input.GetButtonDown("Y_"+Player.controllerNumber)){
				if (state!=AbilityMenuState.Upgrade)
					changeState(AbilityMenuState.Upgrade);
			}
			
			if (Input.GetButtonDown("X_"+Player.controllerNumber)){
				if (swap_item==null){
					swap_item=GetSelectedBar();
				}
				else{
					var s_ab=GetSelectedBar().Ability;
					GetSelectedBar().Ability=swap_item.Ability;
					swap_item.Ability=s_ab;
					swap_item=null;
				}
			}
		}
		
		if (Input.GetButtonDown("Start_"+Player.controllerNumber)){
			if (state==AbilityMenuState.Ready){
				changeState(AbilityMenuState.Bar);
			}
			else
				changeState(AbilityMenuState.Ready);
		}
	}
	
	ItemContainerMain GetSelectedBar(){
		return AbilityBarGrid.SelectedItem().GetComponent<ItemContainerMain>();
	} 
	
	ItemContainerMain GetSelectedPanel(){
		return AbilityPanelGrid.SelectedItem().GetComponent<ItemContainerMain>();
	} 
	
	void changeState(AbilityMenuState state){
		Player.gameObject.SetActive(false);
		
		ShopPanel.SetActive(false);
		UpgradePanel.gameObject.SetActive(false);
		
		hp_slider.gameObject.SetActive(false);
		mp_slider.gameObject.SetActive(false);
		
		if (state==AbilityMenuState.Bar){
			_Camera.selectedObjectInput=AbilityBarGrid.gameObject;
			AbilityBarGrid.HighlightCurrent();
		}
		
		if (state==AbilityMenuState.Panel){
			ShopPanel.SetActive(true);
			_Camera.selectedObjectInput=AbilityPanelGrid.gameObject;
			AbilityPanelGrid.HighlightCurrent();
		}
		
		if (state==AbilityMenuState.Upgrade){
			UpgradePanel.gameObject.SetActive(true);
			UpgradePanel.setAbility(GetSelectedBar().Ability,GetSelectedBar().Stats);
			_Camera.selectedObjectInput=UpgradeGrid.gameObject;
			UpgradeGrid.HighlightCurrent();
		}
		
		/*
		if (this.state==AbilityMenuState.Ready){//Coming from ready

		}
		*/
		
		if (state==AbilityMenuState.Ready){//Going to ready
			_Camera.selectedObjectInput=null;
			_Camera.selectedObjectHighlight=null;
			
			hp_slider.gameObject.SetActive(true);
			mp_slider.gameObject.SetActive(true);
			
			Player.gameObject.SetActive(true);
			
			//save selected abilities.
			int i=0;
			foreach (var con in Player.ability_containers){
				con.ability_prefab=AbilityBarGrid.Grid[i,0].GetComponent<ItemContainerMain>().Ability;
				i++;
			}
		}

		
		this.state=state;
	}
	
	public void setHudOriantation(UIAnchor.Side side){
		anchor.side=side;
	}
}
