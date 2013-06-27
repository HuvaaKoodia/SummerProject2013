using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSelectScript : MonoBehaviour
{
	public PlayerData player;
	public UIButton uibutton;
	public bool updatedOnce = false;
	public int controller = 0, colorIndex = 0;
	List<Color> presetColors;
	public PlayerHudMain playerhud;
	public UILabel textLabel;
	// Use this for initialization
	void Awake ()
	{
		//manager = GameObject.Find("PLAYERDATAS!").GetComponent<PlayerManager> ();

		uibutton = GetComponent<UIButton> ();
		
		presetColors = new List<Color> ();
		presetColors.Add (new Color (256, 0, 0));
		presetColors.Add (new Color (0, 256, 0));
		presetColors.Add (new Color (0, 0, 256));
		presetColors.Add (new Color (256, 256, 0));
		presetColors.Add (new Color (256, 0, 256));
		presetColors.Add (new Color (0, 256, 256));
		presetColors.Add (new Color (256, 256, 256));
		/*foreach (Transform t in transform) {
			
			if (t.name == "SideIcons") {
						
				t.GetComponent<UISprite> ().color = presetColors [colorIndex];
			}
		
		}
		uibutton.pressed = presetColors [colorIndex];
		uibutton.UpdateColor (true, true);*/
		
		NGUITools.SetActive (textLabel.gameObject, true);
	}
	
	public void setPlayer (PlayerData data)
	{
		player = data;
		controller = player.controllerNumber;
		
		colorIndex = controller - 1;
		player.color = presetColors [colorIndex];
		stateUpdate (0);
	}
	
	public void setState (PlayerState state)
	{
		player.state = state;
		
		if (state == PlayerState.notConnected) {
			setActives (false, "Press start", false);
		}
		if (state == PlayerState.connected) {
			setActives (true, "Choose color", false);
				
		}
		if (state == PlayerState.ready) {
			setActives (false, "", true);
		}
	}
	
	void stateUpdate (int addAmount)
	{
		player.state += addAmount;
		setState (player.state);
	}

	void setActives (bool sideIcons, string text, bool startCounter)
	{
		foreach (Transform t in transform) {
			if (t.name == "SideIcons") {
				NGUITools.SetActive (t.gameObject, sideIcons);
			}
		}
		
		textLabel.text = text;
		playerhud.playerManager.startCounter(startCounter);
	}
	
	void Update ()
	{
		if (Input.GetButtonDown ("Start_" + (controller))) {
			if (player.state != PlayerState.ready)
				stateUpdate (1);
			
		} else if (Input.GetButtonDown ("B_" + (controller))) {
			if (player.state > 0) {
				stateUpdate (-1);
		
			}
		}
		
			
		float input = Input.GetAxis ("L_XAxis_" + controller);
		//Debug.Log (uibutton.name + " " + manager.players[controller-1].state);
		
		//Transform transformer,transformer2;
		//List<Transform> sideFormers = new List<Transform>();
		
		if (player.state == PlayerState.notConnected) {
			player.color = Color.white;
		}
		if (player.state == PlayerState.connected) {
			player.color = presetColors [colorIndex];
			
			if (input != 0 && !updatedOnce && !playerhud.playerManager.gameStarting) {
				updatedOnce = true;
				if (input > 0) {
					colorIndex++;
				} else {
					colorIndex--;
				}
				if (colorIndex < 0) {
					colorIndex += presetColors.Count;
				}
				colorIndex = colorIndex % presetColors.Count;
				
			} else if (input == 0) {
				updatedOnce = false;
			}
		}
	}
}
