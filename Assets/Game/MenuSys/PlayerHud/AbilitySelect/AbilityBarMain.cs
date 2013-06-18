using UnityEngine;
using System.Collections;

public class AbilityBarMain : MonoBehaviour {
	public PlayerHudMain playerPanel;
	public ItemContainerMain[] icons;
	
	// Use this for initialization
	void Start () {
		
		var array=playerPanel.Player.ability_containers;
		for (int i=0;i<array.Count;i++){
			var icon=icons[i];
			icon.Ability.Ability=array[i].Ability.Ability;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
