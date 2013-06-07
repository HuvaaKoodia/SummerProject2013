using UnityEngine;
using System.Collections;

public class PlayerHPBar : MonoBehaviour {
	
	CharacterMain player;
	UISlider slider;
	
	// Use this for initialization
	void Start () {
		player=transform.parent.GetComponent<PlayerHudPanel>().player;
		slider=GetComponent<UISlider>();
	}
	
	// Update is called once per frame
	void Update () {
		slider.sliderValue=player.HP/100f;
		Debug.Log ("hphphp "+player.HP);
	}
}
