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
		colorIndex = controller-1;
		
		uibutton = GetComponent<UIButton> ();
		
		presetColors = new List<Color> ();
		presetColors.Add (new Color (256, 0, 0));
		presetColors.Add (new Color (0, 256, 0));
		presetColors.Add (new Color (0, 0, 256));
		presetColors.Add (new Color (256, 256, 0));
		presetColors.Add (new Color (256, 0, 256));
		presetColors.Add (new Color (0, 256, 256));
		presetColors.Add (new Color (256, 256, 256));
		foreach(Transform t in transform){
			
			if(t.name=="SideIcons"){
						
			t.GetComponent<UISprite>().color=presetColors[colorIndex];
			}
		
		}
		uibutton.pressed = presetColors [colorIndex];
		uibutton.UpdateColor (true, true);
		
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		manager.players[controller-1].color=presetColors[colorIndex];
		
		float input = Input.GetAxis ("L_XAxis_" + controller);
			//Debug.Log (uibutton.name + " " + manager.players[controller-1].state);
		
		Transform transformer,transformer2;
			transformer=transform.FindChild("ReadyLabel");
			transformer2=transform.FindChild("ColorLabel");
		//List<Transform> sideFormers = new List<Transform>();
		
		
		
		if(manager.players[controller-1].state==PlayerManager.playerState.notConnected){
			
			NGUITools.SetActive(transformer.gameObject, false);
			NGUITools.SetActive(transformer2.gameObject, false);
		
			foreach(Transform t in transform){
			
			if(t.name=="SideIcons"){
			NGUITools.SetActive(t.gameObject,false);
			}
		
		}
		
			uibutton.OnPress(false);
		}
		if(manager.players[controller-1].state==PlayerManager.playerState.connected){
			
			foreach(Transform t in transform){
			
			if(t.name=="SideIcons"){
			NGUITools.SetActive(t.gameObject,true);
			}
		
		}
			NGUITools.SetActive(transformer.gameObject, false);
			NGUITools.SetActive(transformer2.gameObject, true);
			if (input != 0 && !updatedOnce && !manager.gameStarting) {
				
				
				
				updatedOnce = true;
				if (input > 0) {
					colorIndex++;
				} else {
					colorIndex--;
				}
				if(colorIndex<0){
				colorIndex += presetColors.Count;
				}
				colorIndex = colorIndex % presetColors.Count;
				uibutton.pressed=presetColors[colorIndex];
				foreach(Transform t in transform){
			
			if(t.name=="SideIcons"){
						
			t.GetComponent<UISprite>().color=presetColors[colorIndex];
			}
		
		}
				
				uibutton.UpdateColor (true, true);
				uibutton.OnPress (true);
				
				
			
			} else if (input == 0) {
				updatedOnce = false;
			}
			
		}
	}
}
