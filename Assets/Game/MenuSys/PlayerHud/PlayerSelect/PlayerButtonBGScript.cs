using UnityEngine;
using System.Collections;

public class PlayerButtonBGScript : MonoBehaviour {
	public PlayerSelectScript playerSelectScript;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(GetComponent<UISprite>()!=null){
			GetComponent<UISprite>().color=playerSelectScript.player.color;
		}
		if(GetComponent<UILabel>()!=null){
			GetComponent<UILabel>().color=playerSelectScript.player.color;
		}	
	}
}
