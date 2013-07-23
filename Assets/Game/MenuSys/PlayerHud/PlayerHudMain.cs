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
	
	PlayerManager playerManager;
	GameController gameController;
	
	public PlayerSelectScript playerActivatorMenu;
	public UpgradePanelScr UpgradePanel;
	public GameObject ShopPanel;
	public GridInput AbilityBarGrid, AbilityPanelGrid, UpgradeGrid;
	public PlayerHudBGscr menu_BG_panel;
	public UISlider hp_slider, mp_slider;
	public UIAnchor anchor;
	
	ItemContainerMain swap_item;
	
	public void Start ()
	{
		var entDB=GameObject.FindGameObjectWithTag("LevelController");
		playerManager=entDB.GetComponent<PlayerManager>();
		gameController=entDB.GetComponent<GameController>();
		
		playerActivatorMenu.setPlayer (playerData);
		_Camera.verticalAxisName="L_YAxis_"+playerData.controllerNumber;
		_Camera.horizontalAxisName="L_XAxis_"+playerData.controllerNumber;
		
		//set side
		UIAnchor.Side[] Sides=new UIAnchor.Side[]{UIAnchor.Side.TopLeft,UIAnchor.Side.TopRight,UIAnchor.Side.BottomLeft,UIAnchor.Side.BottomRight};
		anchor.side=Sides[playerData.controllerNumber-1];
		
		AbilityBarGrid.UpdateGrid ();
		AbilityPanelGrid.UpdateGrid ();
		UpgradeGrid.UpdateGrid ();
		
		if (playerData.state==PlayerState.ready)
			state=AbilityMenuState.Bar;
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
			
				
			}
		
			if (gameController.State==GameState.Setup) {
				if (Input.GetButtonDown ("Start_" + playerData.controllerNumber)||Input.GetKeyDown(KeyCode.K)) {//DEV.key
					if (state == AbilityMenuState.Ready) {
						changeState (AbilityMenuState.Bar);
					} else {
						if (resources <0){
							menu_BG_panel.NoMoniesWarning();
						}else{
							changeState (AbilityMenuState.Ready);
						}
					}
				}
				
				//back to player activation
				if (Input.GetButtonDown ("Back_" + playerData.controllerNumber)) {
					if (state != AbilityMenuState.Ready && state != AbilityMenuState.Off) {
						changeState (AbilityMenuState.Off);
					}
					if (state == AbilityMenuState.Ready) {
						changeState (AbilityMenuState.Bar);
					}
				}
			}
		
			//update bg labels
			menu_BG_panel.SetResources(resources);
		
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
			if (state == AbilityMenuState.Bar) {
				if (Input.GetButtonDown ("X_" + playerData.controllerNumber)) {
					if (swap_item == null) {
						swap_item = GetSelectedBar ();
						_Camera.selectedObjectHighlight = AbilityBarGrid.SelectedItem ().gameObject;
						swap_item.enableSwapSprite(true);
						
					} else {
						var s_ab = GetSelectedBar ().Ability;
						GetSelectedBar ().Ability = swap_item.Ability;
						swap_item.Ability = s_ab;
						disableSwap();
					}
				}
			}
			if (state == AbilityMenuState.Ready) {
				if (playerData.Player != null) {
					hp_slider.sliderValue = playerData.Player.HP / 100f;
					mp_slider.sliderValue = playerData.Player.MP / 100f;
				}
				else{
					//hp_slider.sliderValue=mp_slider.sliderValue = 0;
					if (gameController.State==GameState.GameOn)
						gameObject.SetActive(false);
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
	void disableSwap(){
		if(!swap_item)
			return ;
		swap_item.enableSwapSprite(false);
		swap_item = null;
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
		disableSwap();
		
		var old_state=this.state;
		this.state = state;
		
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
			
			gameController.startCounter (true);
		}
		
		if (state == AbilityMenuState.Bar) {
			menu_BG_panel.enableButtonHelper(true);
			menu_BG_panel.setPlayer(playerData);
			_Camera.selectedObjectInput = AbilityBarGrid.gameObject;
			AbilityBarGrid.HighlightCurrent ();
			
			gameController.startCounter (false);
		}else if (old_state == AbilityMenuState.Bar) {
			menu_BG_panel.enableButtonHelper(false);
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
				UpgradeGrid.HighlightCurrent();
			}
			else
				return;
		}
		
		if (old_state == AbilityMenuState.Upgrade) {//Coming from upgrade
			UpgradePanel.clearGrid();
		}
		
		if (old_state == AbilityMenuState.Ready) {//Coming from ready
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
				playerData.Abilities[i]=a;
				i++;
			}
			
			//create player object
			playerManager.CreatePlayer (playerData);
			
			gameController.startCounter (true);
		}
		
	}
	
	/*public void setHudOriantation(UIAnchor.Side side){
		anchor.side=side;
	}*/
}
