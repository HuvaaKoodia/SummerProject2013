using UnityEngine;
using System.Collections;

public enum AbilityMenuState{Bar,Shop,Upgrade,Ready}

public class PlayerHudMain : MonoBehaviour {
	
	public PlayerMain Player;
	public UICamera _Camera;
	public AbilityMenuState state;
	
	public UpgradePanelScr UpgradePanel;
	public GameObject ShopPanel;
	public GridInput AbilityBarGrid,AbilityPanelGrid,UpgradeGrid;
	public PlayerHudBGscr menu_BG_panel;
	
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
		AbilityBarGrid.UpdateGrid();
		//AbilityPanelGrid.UpdateGrid();
		changeState(state);
	}
	
	// Update is called once per frame
	void Update () {
		//game
		
		hp_slider.sliderValue=Player.HP/100f;
		mp_slider.sliderValue=Player.MP/100f;
		
		int current_cost=0;
		foreach (var abi in AbilityBarGrid.Grid){
			current_cost+=abi.GetComponent<ItemContainerMain>().Ability.GetCost();
		}
		
		int resources=Player.Data.ResourceAmount-current_cost;
		
		//input
		if (state!=AbilityMenuState.Ready){
			if (Input.GetButtonDown("A_"+Player.controllerNumber)){
				if (state==AbilityMenuState.Bar){
					changeState(AbilityMenuState.Shop);
				} else
				if (state==AbilityMenuState.Shop){
					//select item and change to current slot
					GetSelectedBar().Ability.Ability=GetSelectedShop().Ability.Ability;
					GetSelectedBar().Ability.Stats.Clear();
					changeState(AbilityMenuState.Bar);
				} else
				if (state==AbilityMenuState.Upgrade){
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
					_Camera.selectedObjectHighlight=AbilityBarGrid.SelectedItem().gameObject;
				}
				else{
					var s_ab=GetSelectedBar().Ability;
					GetSelectedBar().Ability=swap_item.Ability;
					swap_item.Ability=s_ab;
					swap_item=null;
				}
			}
		}
		
		if (resources>0&&Input.GetButtonDown("Start_"+Player.controllerNumber)){
			if (state==AbilityMenuState.Ready){
				changeState(AbilityMenuState.Bar);
			}
			else
				changeState(AbilityMenuState.Ready);
		}
		
		
		//update bg labels
		
		menu_BG_panel.SetResources(resources);
		
		if (state==AbilityMenuState.Bar||state==AbilityMenuState.Upgrade){
			menu_BG_panel.SetCost(GetSelectedBar().Ability.GetCost());
			menu_BG_panel.SetName(GetSelectedBar().Ability.GetName());
		}
		if (state==AbilityMenuState.Shop){
			if (GetSelectedShop().Ability.Ability!=null){
				menu_BG_panel.SetCost(GetSelectedShop().Ability.GetCost());
				menu_BG_panel.SetName(GetSelectedShop().Ability.GetName());
			}
			else{
				menu_BG_panel.SetCost(0);
				menu_BG_panel.SetName("");
			}
		}
	}
	
	ItemContainerMain GetSelectedBar(){
		return AbilityBarGrid.SelectedItem().GetComponent<ItemContainerMain>();
	}
	
	ItemContainerMain GetSelectedShop(){
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
		
		if (state==AbilityMenuState.Shop){
			ShopPanel.SetActive(true);
			_Camera.selectedObjectInput=AbilityPanelGrid.gameObject;
			AbilityPanelGrid.HighlightCurrent();
		}
		
		if (state==AbilityMenuState.Upgrade){
			_Camera.AnalogHorizontalDelay=0.1f;
			UpgradePanel.gameObject.SetActive(true);
			UpgradePanel.setAbility(GetSelectedBar().Ability);
			_Camera.selectedObjectInput=UpgradeGrid.gameObject;
			UpgradeGrid.HighlightCurrent();
		}
		
		if (this.state==AbilityMenuState.Upgrade){//Coming from upgrade
			UpgradePanel.clearGrid();
			_Camera.setAnalogStickDelaysDefault();
		}
		
		if (this.state==AbilityMenuState.Ready){//Coming from ready
			menu_BG_panel.gameObject.SetActive(true);
		}
		
		if (state==AbilityMenuState.Ready){//Going to ready
			
			menu_BG_panel.gameObject.SetActive(false);
			
			_Camera.selectedObjectInput=null;
			_Camera.selectedObjectHighlight=null;
			
			hp_slider.gameObject.SetActive(true);
			mp_slider.gameObject.SetActive(true);
			
			Player.gameObject.SetActive(true);
			
			//save selected abilities.
			int i=0;
			foreach (var con in Player.ability_containers){
				con.Ability=AbilityBarGrid.Grid[i,0].GetComponent<ItemContainerMain>().Ability;
				i++;
			}
		}
		this.state=state;
	}
	
	public void setHudOriantation(UIAnchor.Side side){
		anchor.side=side;
	}
}
