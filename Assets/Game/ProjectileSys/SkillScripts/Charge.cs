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

		Vector3 speedVector=heading*force / 2f;
 		speedVector.y=0;
		
		
		plr_main.rigidbody.velocity = new Vector3(speedVector.x,plr_main.rigidbody.velocity.y, speedVector.z);
	}

	
	void OnDestroy(){
	
	}
}
