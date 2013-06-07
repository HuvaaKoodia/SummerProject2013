using UnityEngine;
using System.Collections;

public class PlayerHPBar : MonoBehaviour {
	
	PlayerMain player;

	// Use this for initialization
	void Awake() {
		player=transform.parent.parent.GetComponent<PlayerHudPanel>().Player;
	}
	
	// Update is called once per frame
	void Update () {

	}
}
