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
	public string buttonSpriteName="none";
	public AbilityItem Ability{
		get{return ability;}
		set{ability=value;
			setSprite(value.Ability);
		}
	}
	public UISprite abilityCooldownSprite, buttonSprite, abilitySprite, backgroundSprite,swapSprite;
	// Use this for initialization
	void Awake () {
		if(buttonSpriteName=="none")
			buttonSprite.gameObject.SetActive(false);
		else
			buttonSprite.spriteName=buttonSpriteName;
		abilitySprite.spriteName="empty";
		abilityCooldownSprite.fillAmount=0;
		Ability=new AbilityItem();
	}

	void setSprite(Transform ability){
		
		string spr_name="Transparent";
		if (ability!=null){
			var sts=ability.GetComponent<AbilityStats>();
			spr_name=sts.Sprite;
		}
		
		abilitySprite.spriteName=spr_name;
		
		
	}
	public void enableSwapSprite(bool truth){
		swapSprite.gameObject.SetActive(truth);
	}
	/// <summary>
	/// 0-1 please
	/// </param>
	public void setSpriteFillPercent(float percent){
		abilityCooldownSprite.fillAmount=percent;
	}
}
