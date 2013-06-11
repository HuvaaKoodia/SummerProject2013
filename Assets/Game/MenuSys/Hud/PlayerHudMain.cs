using UnityEngine;
using System.Collections;

public class PlayerHudMain : MonoBehaviour {
	
	enum AbilityMenuState{Bar,Panel,Upgrade,Ready}
	
	public PlayerMain Player;
	public UICamera _Camera;
	AbilityMenuState state;
	
	public GameObject ShopPanel;
	public GridInput AbilityBarGrid,AbilityPanelGrid;
	
	UIAnchor anchor;
	UISlider hp_slider,mp_slider;
	
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
		
		if (Input.GetButtonDown("Start_"+Player.controllerNumber)){
			changeState(AbilityMenuState.Ready);
		}
	}
	
	ItemContainerMain swap_item;
	
	ItemContainerMain GetSelectedBar(){
		return AbilityBarGrid.SelectedItem().GetComponent<ItemContainerMain>();
	} 
	
	ItemContainerMain GetSelectedPanel(){
		return AbilityPanelGrid.SelectedItem().GetComponent<ItemContainerMain>();
	} 
	
	void changeState(AbilityMenuState state){
		
		
		if (state==AbilityMenuState.Bar){
			_Camera.selectedObjectInput=AbilityBarGrid.gameObject;
			AbilityBarGrid.SelectCurrent();
		}
		
		if (state==AbilityMenuState.Panel){
			_Camera.selectedObjectInput=AbilityPanelGrid.gameObject;
			AbilityPanelGrid.SelectCurrent();
		}
		
		if (state==AbilityMenuState.Upgrade){
			
		}
		
		if (state==AbilityMenuState.Ready){
			_Camera.selectedObjectInput=null;
			_Camera.selectedObjectHighlight=null;
			ShopPanel.SetActive(false);
		}
		
		this.state=state;
	}
	
	public void setHudOriantation(UIAnchor.Side side){
		anchor.side=side;
	}
}
