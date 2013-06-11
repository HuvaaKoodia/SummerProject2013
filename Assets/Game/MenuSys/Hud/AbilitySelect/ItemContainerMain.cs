using UnityEngine;
using System.Collections;

public class ItemContainerMain : MonoBehaviour {
	
	Transform ability;
	public Transform Ability{
		get {return ability;}
		set	{setAbility(value);}
	}
	
	UISprite spr;
	
	// Use this for initialization
	void Awake () {
		spr=transform.Find("Background").GetComponent<UISprite>();
		spr.spriteName="Empty";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void setAbility(Transform ability){
		this.ability=ability;
		var sts=ability.GetComponent<AbilityStats>();
		spr.spriteName=sts.spriteName;
	}
}
