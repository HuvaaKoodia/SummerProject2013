using UnityEngine;
using System.Collections;

public class Charge : MonoBehaviour, ProjectileModifier {
	ProjectileMain pro_main;
	PlayerMain plr_main;
	Vector3 heading;
	float force;
	Vector3 speedVector;
	
	// Use this for initialization
	void Start (){
		pro_main=GetComponent<ProjectileMain>();
		plr_main = pro_main.Creator;
		heading = plr_main.LowerGraphicsDir*Vector3.forward;
		force=pro_main.mod_stats.Speed;
		
		speedVector=heading * (force / 2f);

		plr_main.DashStart(speedVector);
	}
	
	void OnDestroy(){
		if (plr_main!=null)
			plr_main.DashEnd();
	}
}
