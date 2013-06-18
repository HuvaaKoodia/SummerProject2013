using UnityEngine;
using System.Collections;

public class AbilityItem{
	Transform ability;
	public Transform Ability{
		get {return ability;}
		set	{ability=value;}
	}
	UpgradeStatContainer abilityStats=new UpgradeStatContainer();
	public UpgradeStatContainer Stats{get{return abilityStats;}}
}

public class ItemContainerMain : MonoBehaviour {
	
	public AbilityItem Ability;
	UISprite spr;
	
	
	// Use this for initialization
	void Awake () {
		spr=transform.Find("Background").GetComponent<UISprite>();
		spr.spriteName="Empty";
	
		Ability=new AbilityItem();
	}
	
	// Update is called once per frame
	void Update (){
		//DEV.HACK!
		setSprite(Ability.Ability);
	}
	
	void setSprite(Transform ability){
		
		string spr_name="Empty";
		if (ability!=null){
			var sts=ability.GetComponent<AbilityStats>();
			spr_name=sts.spriteName;
		}
		spr.spriteName=spr_name;
	}
}
