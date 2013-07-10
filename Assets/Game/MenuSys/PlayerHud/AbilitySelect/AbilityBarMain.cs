using UnityEngine;
using System.Collections;

public class AbilityBarMain : MonoBehaviour {
	public PlayerHudMain playerPanel;
	public ItemContainerMain[] icons;
	
	// Use this for initialization
	void Start () {
		var array=playerPanel.playerData.Abilities;
		for (int i=0;i<array.Count;i++){
			var icon=icons[i];
			icon.Ability=array[i];
		}
	}
	
	// Update is called once per frame
	void Update () {
		var p=playerPanel.playerData.Player;
		
		if (playerPanel.state==AbilityMenuState.Ready){
			if (p!=null&&p.ability_containers!=null){
				for (int i=0;i<p.ability_containers.Count;i++){
					var ac = p.ability_containers[i];
					var per=ac.getCooldownPercent();
					/*if (per<1)
						per=1-per;*/
					if (per==1)
						per=0;
					icons[i].setSpriteFillPercent(per);
				}
			}
		}
		else{
			for (int i=0;i<icons.Length;i++){
				icons[i].setSpriteFillPercent(0);
			}
		}
		
	}
}
