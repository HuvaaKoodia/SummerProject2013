using UnityEngine;
using System.Collections;

public class ItemContainerMain : MonoBehaviour {
	
	public AbilityStats ability;
	UISprite spr;
	
	// Use this for initialization
	void Start () {
		spr=transform.Find("Background").GetComponent<UISprite>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void setAbility(AbilityStats stats){
		ability=stats;
		
		
	}
}
