using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSelectScript : MonoBehaviour
{
	public PlayerData player;
	
	public PlayerButtonBGScript color_b,color_a1,color_a2;
	public UILabel textLabel,player_name_label;
	
	bool updatedOnce = false;
	int controller = 0, colorIndex = 0;
	
	List<Color> presetColors;
	
	Color current_color;

	
	// Use this for initialization
	void Awake ()
	{
		presetColors = new List<Color> ();
		presetColors.Add (Color.red);
		presetColors.Add (Color.green);
		presetColors.Add (Color.blue);
		presetColors.Add (Color.yellow);
		presetColors.Add (new Color (1, 0, 1));
		presetColors.Add (new Color (0, 1, 1));
		presetColors.Add (new Color (1, 1, 1));
	}
	
	public void setPlayer (PlayerData data)
	{
		player = data;
		controller = player.controllerNumber;
		
		colorIndex = controller - 1;
		player.color=presetColors[colorIndex];
		current_color=player.color;
		
		player_name_label.text="PLAYER "+player.controllerNumber;
		
		stateUpdate (0);
	}
	
	public void setState (PlayerState state)
	{
		player.state = state;
		
		if (state == PlayerState.notConnected) {
			setButtonColor(Color.white);
			setActives (false, "Press start", false);
		}
		if (state == PlayerState.connected) {
			setButtonColor(current_color);
			setActives (true, "Choose color", false);
				
		}
		if (state == PlayerState.ready) {
			player.color = current_color;
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
	}
	
	void Update ()
	{
		if (Input.GetButtonDown ("Start_" + (controller))||Input.GetButtonDown ("A_" + (controller))||Input.GetKey(KeyCode.K)) {//DEV.key
			if (player.state != PlayerState.ready)
				stateUpdate (1);
			
		} else 
		if (Input.GetButtonDown ("Back_" + (controller))||Input.GetButtonDown ("B_" + (controller))) {
			if (player.state > 0) {
				stateUpdate (-1);
			}
		}
		
		float input = Input.GetAxis ("L_XAxis_" + controller);
		
		if (player.state == PlayerState.connected) {
			
			if (input != 0 && !updatedOnce) {
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
				current_color=presetColors[colorIndex];
				setButtonColor(current_color);
				
			} else if (input == 0) {
				updatedOnce = false;
			}
		}
	}
	
	void setButtonColor(Color c){
		color_b.setColor(c);
		color_a1.setColor(c);
		color_a2.setColor(c);
	}
}
