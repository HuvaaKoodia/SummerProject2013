using UnityEngine;
using System.Collections;

public enum AbilityMenuState
{
	Off,
	Bar,
	Shop,
	Upgrade,
	Ready
}

public class PlayerHudMain : MonoBehaviour
{
	public GameObject GameMenu;
	public PlayerData playerData;
	public UICamera _Camera;
	public AbilityMenuState state;
	public PlayerManager playerManager;
	public GameController gameController;
	public PlayerSelectScript playerActivatorMenu;
	public UpgradePanelScr UpgradePanel;
	public GameObject ShopPanel;
	public GridInput AbilityBarGrid, AbilityPanelGrid, UpgradeGrid;
	public PlayerHudBGscr menu_BG_panel;
	public UISlider hp_slider, mp_slider;
	ItemContainerMain swap_item;
	
	public void Start ()
	{
		playerActivatorMenu.setPlayer (playerData);
		_Camera.verticalAxisName="L_YAxis_"+playerData.controllerNumber;
		_Camera.horizontalAxisName="L_XAxis_"+playerData.controllerNumber;
		
		AbilityBarGrid.UpdateGrid ();
		AbilityPanelGrid.UpdateGrid ();
		UpgradeGrid.UpdateGrid ();
		changeState (state);
	}
	
	void Update ()
	{
		if (state == AbilityMenuState.Off) {//player select
			if (playerData.state == PlayerState.ready) {
				changeState (AbilityMenuState.Bar);
			}
		} else {//game huds
			
			int current_cost = 0;
			foreach (var abi in AbilityBarGrid.Grid) {
				current_cost += abi.GetComponent<ItemContainerMain> ().Ability.GetCost ();
			}
		
			int resources = playerData.ResourceAmount - current_cost;
		
			//input
			if (state != AbilityMenuState.Ready) {
				if (Input.GetButtonDown ("A_" + playerData.controllerNumber)) {
					if (state == AbilityMenuState.Bar) {
						changeState (AbilityMenuState.Shop);
					}else if (state == AbilityMenuState.Shop) {
						//select item and change to current slot
						GetSelectedBar ().Ability = new AbilityItem(GetSelectedShop ().Ability.Ability);
						GetSelectedBar ().Ability.Stats.Clear();
						changeState (AbilityMenuState.Bar);
					}else if (state == AbilityMenuState.Upgrade){
						changeState (AbilityMenuState.Bar);
					}
				}
			
				if (Input.GetButtonDown ("B_" + playerData.controllerNumber)) {
					changeState (AbilityMenuState.Bar);
				}
			
				if (Input.GetButtonDown ("Y_" + playerData.controllerNumber)) {
					if (state != AbilityMenuState.Upgrade)
						changeState (AbilityMenuState.Upgrade);
				}
			
				if (Input.GetButtonDown ("X_" + playerData.controllerNumber)) {
					if (swap_item == null) {
						swap_item = GetSelectedBar ();
						_Camera.selectedObjectHighlight = AbilityBarGrid.SelectedItem ().gameObject;
					} else {
						var s_ab = GetSelectedBar ().Ability;
						GetSelectedBar ().Ability = swap_item.Ability;
						swap_item.Ability = s_ab;
						swap_item = null;
					}
				}
			}
		
			if (gameController.State==GameState.Setup) {
				if (resources >= 0 && Input.GetButtonDown ("Start_" + playerData.controllerNumber)||Input.GetKeyDown(KeyCode.K)) {//DEV.key
					if (state == AbilityMenuState.Ready) {
						changeState (AbilityMenuState.Bar);
						gameController.startCounter (false);
					} else {
						changeState (AbilityMenuState.Ready);
						gameController.startCounter (true);
					}
				}
				
				//back to player activation
				if (Input.GetButtonDown ("Back_" + playerData.controllerNumber)) {
					if (state != AbilityMenuState.Ready && state != AbilityMenuState.Off) {
						changeState (AbilityMenuState.Off);
						gameController.startCounter (true);
					}
					if (state == AbilityMenuState.Ready) {
						changeState (AbilityMenuState.Bar);
						gameController.startCounter (false);
					}
				}
			}
		
			//update bg labels
			menu_BG_panel.SetResources (resources);
		
			if (state == AbilityMenuState.Bar || state == AbilityMenuState.Upgrade) {
				menu_BG_panel.SetCost (GetSelectedBar ().Ability.GetCost ());
				menu_BG_panel.SetName (GetSelectedBar ().Ability.GetName ());
			}
			if (state == AbilityMenuState.Shop) {
				if (GetSelectedShop ().Ability.Ability != null){
					menu_BG_panel.SetCost (GetSelectedShop ().Ability.GetCost ());
					menu_BG_panel.SetName (GetSelectedShop ().Ability.GetName ());
				} else {
					menu_BG_panel.SetCost (0);
					menu_BG_panel.SetName ("");
				}
			}
			
			//update MP & HP
			
			if (state == AbilityMenuState.Ready) {
				if (playerData.Player != null) {
					hp_slider.sliderValue = playerData.Player.HP / 100f;
					mp_slider.sliderValue = playerData.Player.MP / 100f;
				}
				else{
					hp_slider.sliderValue=mp_slider.sliderValue = 0;
				}
				if (gameController.State==GameState.Setup) {
					if (playerData.Player == null) {
						playerManager.CreatePlayer(playerData);
					}
					else{	
						if (Input.GetButtonDown("B_" + playerData.controllerNumber)){
							playerData.Player.Die();
						}
					}
				}
			}
		}
	}
	
	ItemContainerMain GetSelectedBar ()
	{
		return AbilityBarGrid.SelectedItem ().GetComponent<ItemContainerMain> ();
	}
	
	ItemContainerMain GetSelectedShop ()
	{
		return AbilityPanelGrid.SelectedItem ().GetComponent<ItemContainerMain> ();
	}
	
	void changeState (AbilityMenuState state)
	{
		_Camera.setAnalogStickDelaysDefault ();
		
		GameMenu.gameObject.SetActive (true);
		
		playerActivatorMenu.gameObject.SetActive (false);
		
		ShopPanel.SetActive (false);
		UpgradePanel.gameObject.SetActive (false);
		
		hp_slider.gameObject.SetActive (false);
		mp_slider.gameObject.SetActive (false);
		
		if (state == AbilityMenuState.Off) {
			playerActivatorMenu.gameObject.SetActive (true);
			GameMenu.gameObject.SetActive (false);
			playerActivatorMenu.setState (PlayerState.notConnected);
		}
		
		if (state == AbilityMenuState.Bar) {
			_Camera.selectedObjectInput = AbilityBarGrid.gameObject;
			AbilityBarGrid.HighlightCurrent ();
		}
		
		if (state == AbilityMenuState.Shop) {
			ShopPanel.SetActive (true);
			_Camera.selectedObjectInput = AbilityPanelGrid.gameObject;
			AbilityPanelGrid.HighlightCurrent ();
		}
		
		if (state == AbilityMenuState.Upgrade) {
			if (GetSelectedBar ().Ability.Ability.GetComponent<UpgradeStats> ().AvailableUpgrades.Length > 0) {
				_Camera.AnalogHorizontalDelay = 0.1f;
				UpgradePanel.gameObject.SetActive (true);
				UpgradePanel.setAbility (GetSelectedBar ().Ability);
				_Camera.selectedObjectInput = UpgradeGrid.gameObject;
				UpgradeGrid.HighlightCurrent ();
			}
			else
				return;
		}
		
		if (this.state == AbilityMenuState.Upgrade) {//Coming from upgrade
			UpgradePanel.clearGrid ();
		}
		
		if (this.state == AbilityMenuState.Ready) {//Coming from ready
			menu_BG_panel.gameObject.SetActive (true);
			playerManager.DestroyPlayer (playerData);
		}
		
		if (state == AbilityMenuState.Ready) {//Going to ready
			
			menu_BG_panel.gameObject.SetActive (false);
			
			_Camera.selectedObjectInput = null;
			_Camera.selectedObjectHighlight = null;
			
			hp_slider.gameObject.SetActive (true);
			mp_slider.gameObject.SetActive (true);
			
			//set player data
			
			//save selected abilities.
			int i=0;
			foreach (var item in AbilityBarGrid.Grid) {
				var a = item.GetComponent<ItemContainerMain>().Ability;
				var d=playerData.Abilities[i]=a;
				i++;
			}
			
			//create player object
			playerManager.CreatePlayer (playerData);
		}
		this.state = state;
	}
	
	/*public void setHudOriantation(UIAnchor.Side side){
		anchor.side=side;
	}*/
}
