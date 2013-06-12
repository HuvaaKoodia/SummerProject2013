using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSelectScript : MonoBehaviour
{
	public UIButton uibutton;
	public bool updatedOnce = false;
	public int controller = 0, colorIndex = 0;
	List<Color> presetColors;
	public PlayerManager manager;
	public playerData player;
	Transform readyLabel, colorLabel;
	// Use this for initialization
	void Start ()
	{
		manager = GameObject.Find ("PLAYERDATAS!").GetComponent<PlayerManager> ();
		int.TryParse (name [3].ToString (), out controller);
		player = manager.players [controller - 1];
		
		
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
		colorIndex = controller - 1;
		
		player.color=presetColors[colorIndex];
		
		readyLabel = player.button.transform.Find("ReadyLabel");
		colorLabel = player.button.transform.Find("ColorLabel");
		stateUpdate(0);
	}
	
	// Update is called once per frame
	void stateUpdate(int addAmount){
		player.state += addAmount;
		if (player.state == playerState.notConnected) {
			setActives(false,false,false, false);
		}
		if (player.state == playerState.connected) {
			setActives(true,false,true, false);
				
		}
		if (player.state == playerState.ready) {
			setActives(false,true,false, true);
		}
		
		
	}
	void setActives(bool sideIcons, bool ready, bool color, bool startCounter){
		foreach (Transform t in transform) {
			if (t.name == "SideIcons") {
				NGUITools.SetActive (t.gameObject, sideIcons);
				
			}
		}
		NGUITools.SetActive (readyLabel.gameObject, ready);
		NGUITools.SetActive (colorLabel.gameObject, color);
		manager.startCounter(startCounter);
	}
	
	void Update ()
	{
		if (Input.GetButtonDown ("Start_" + (controller))) {
			if (player.state != playerState.ready)
				stateUpdate(1);
			
		} else if (Input.GetButtonDown ("B_" + (controller))) {
			if (player.state > 0) {
				stateUpdate(-1);
		
			}
		}
		
			
		float input = Input.GetAxis ("L_XAxis_" + controller);
		//Debug.Log (uibutton.name + " " + manager.players[controller-1].state);
		
		//Transform transformer,transformer2;
		//List<Transform> sideFormers = new List<Transform>();
		
		
		
		if (player.state == playerState.notConnected) {
			player.color=Color.white;
			
		}
		if (player.state == playerState.connected) {
			player.color=presetColors[colorIndex];
			
			if (input != 0 && !updatedOnce && !manager.gameStarting) {
			
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
