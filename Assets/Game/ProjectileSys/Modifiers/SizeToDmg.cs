using UnityEngine;
using System.Collections;

public class SizeToDmg : MonoBehaviour,ProjectileModifier {
	
	ProjectileMain pro;
	float start_scale,start_dmg;
	// Use this for initialization
	void Start () {
		pro=GetComponent<ProjectileMain>();
		
		start_scale=transform.localScale.x;
		start_dmg=pro.Power;
	}
	
	// Update is called once per frame
	void Update (){
		pro.Power=start_dmg*(transform.localScale.x/start_scale);
	}
}
