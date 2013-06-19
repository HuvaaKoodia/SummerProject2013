using UnityEngine;
using System.Collections;

public class Charge : MonoBehaviour {
	ProjectileMain pro_main;
	PlayerMain plr_main;
	Vector3 heading;
	float force;
	
	// Use this for initialization
	void Start () {
		pro_main=GetComponent<ProjectileMain>();
		plr_main = pro_main.Creator;
		heading = plr_main.LowerTorsoDir;
		force=pro_main.stats.Knockback;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 speedVector=heading;
		speedVector.y=0;
		plr_main.rigidbody.velocity = speedVector*(force/2f);
	}
	
	void OnDestroy(){
	
	}
}
