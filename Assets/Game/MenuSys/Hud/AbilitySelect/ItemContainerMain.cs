using UnityEngine;
using System.Collections;

public class ItemContainerMain : MonoBehaviour {
	
	public AbilityStats ability;
	
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void setAbility(AbilityStats stats){
		ability=stats;
		
	}
}
