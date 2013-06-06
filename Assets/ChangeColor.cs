using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChangeColor : MonoBehaviour
{
	public UIButton uibutton;
	public bool updatedOnce = false;
	public int controller = 0, colorIndex = 0;
	List<Color> presetColors;
	public PlayerManager manager;
	// Use this for initialization
	void Start ()
	{
		manager = GameObject.Find ("PLAYERDATAS!").GetComponent<PlayerManager> ();
		int.TryParse (name [3].ToString (), out controller);
		colorIndex = controller;
		
		uibutton = GetComponent<UIButton> ();
		
		presetColors = new List<Color> ();
		presetColors.Add (new Color (256, 256, 256));
		presetColors.Add (new Color (256, 0, 0));
		presetColors.Add (new Color (0, 256, 0));
		presetColors.Add (new Color (0, 0, 256));
		presetColors.Add (new Color (256, 256, 0));
		presetColors.Add (new Color (256, 0, 256));
		presetColors.Add (new Color (0, 256, 256));
		
		uibutton.pressed = presetColors [controller-1];
		uibutton.UpdateColor (true, true);
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		float input = Input.GetAxis ("L_XAxis_" + controller);
			Debug.Log (input + " " + uibutton.name + " " + manager.players[controller-1].state);
		if (input != 0 && !updatedOnce && !manager.gameStarting && manager.players[controller-1].state==PlayerManager.playerState.connected) {
			
			updatedOnce = true;
			if (input > 0) {
				colorIndex++;
			} else {
				colorIndex--;
			}
			if(colorIndex<0){
			colorIndex+=presetColors.Count;
			}
			colorIndex = colorIndex % presetColors.Count;
			//uibutton.defaultColor = presetColors [colorIndex];
			uibutton.UpdateColor (true, true);
			uibutton.OnPress (true);
		
		} else if (input == 0) {
			updatedOnce = false;
		}
	}
}
