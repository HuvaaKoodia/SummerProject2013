using UnityEngine;
using System.Collections;

public class ChargeKnockbackDetection : MonoBehaviour,ProjectileModifier {
	PlayerMain player;
	// Use this for initialization
	void Start () {
		player=GetComponent<ProjectileMain>().Creator;	
	}
	

	

}
