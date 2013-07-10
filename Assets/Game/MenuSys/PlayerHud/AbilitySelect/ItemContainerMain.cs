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
	
	public AbilityItem(){}
	
	public AbilityItem(Transform ability){
		Ability=ability;
	}
	
	public string GetName ()
	{
		return ability.GetComponent<AbilityStats>().Name;
	}

	public int GetCost ()
	{
		int cost=ability.GetComponent<AbilityStats>().Cost;
		
		foreach (var s in Stats.Data.Values){
			cost+=s;
		}
		
		return cost;
	}
}

public class ItemContainerMain : MonoBehaviour {
	AbilityItem ability;
	public AbilityItem Ability{
		get{return ability;}
		set{ability=value;
			setSprite(value.Ability);
		}
	}
	UISprite spr;

	// Use this for initialization
	void Awake () {
		spr=transform.Find("Background").GetComponent<UISprite>();
		spr.spriteName="empty";
	
		Ability=new AbilityItem();
	}

	void setSprite(Transform ability){
		
		string spr_name="empty";
		if (ability!=null){
			var sts=ability.GetComponent<AbilityStats>();
			spr_name=sts.Sprite;
		}
		spr.spriteName=spr_name;
	}
	/// <summary>
	/// 0-1 please
	/// </param>
	public void setSpriteFillPercent(float percent){
		spr.fillAmount=percent;
	}
}
