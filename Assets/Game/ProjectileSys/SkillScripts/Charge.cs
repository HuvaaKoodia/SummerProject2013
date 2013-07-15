using UnityEngine;
using System.Collections;

public class Charge : MonoBehaviour, ProjectileModifier {
	ProjectileMain pro_main;
	PlayerMain plr_main;
	Vector3 heading;
	float force;
	Vector3 speedVector;
	
	// Use this for initialization
	void Start () {
		pro_main=GetComponent<ProjectileMain>();
		plr_main = pro_main.Creator;
		heading = plr_main.LowerTorsoDir;
		force=pro_main.stats.Speed;
	}
	
	// Update is called once per frame
 	void Update () {
		plr_main.freezeMovement(true);
		speedVector=heading * (force / 2f);
 		speedVector.y = plr_main.rigidbody.velocity.y;
		
		plr_main.rigidbody.velocity = new Vector3(speedVector.x,plr_main.rigidbody.velocity.y, speedVector.z);
	}

	
	void OnDestroy(){
		if (plr_main!=null)
			plr_main.freezeMovement(false);
	}
}
