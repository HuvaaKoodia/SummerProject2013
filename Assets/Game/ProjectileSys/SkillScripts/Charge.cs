using UnityEngine;
using System.Collections;

public class Charge : MonoBehaviour, ProjectileModifier {
	ProjectileMain pro_main;
	PlayerMain plr_main;
	Vector3 heading;
	float force,power,knockback;
	Vector3 speedVector;
	
	// Use this for initialization
	void Start (){
		pro_main=GetComponent<ProjectileMain>();
		plr_main = pro_main.Creator;
		heading = plr_main.LowerGraphicsDir*Vector3.forward;
		force=pro_main.mod_stats.Speed;
		power=pro_main.mod_stats.Power;
		knockback=pro_main.mod_stats.Knockback;
		
		speedVector=heading * (force / 2f);

		plr_main.DashStart(speedVector);
	}
	
	// Update is called once per frame
	void Update () {
		if (!plr_main){
			Destroy(gameObject);
			return;
		}
		transform.position = plr_main.transform.position;
	}
	
	void OnDestroy(){
		if (plr_main!=null)
			plr_main.DashEnd();
	}
	
	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag=="Player" && other.gameObject != plr_main.gameObject){
			var other_p=other.GetComponent<PlayerMain>();
			Vector3 heading = other.gameObject.transform.position - plr_main.transform.position;
			//heading.y=1f;
			//heading=heading.normalized;
			
			other_p.KNOCKBACKHAX(plr_main.transform.position+Vector3.down,knockback,1,1);
			other_p.HP-=power;
			
			if (Vector3.Angle(heading,plr_main.transform.rotation*Vector3.forward)<45)
				plr_main.DashEnd();
			
			//other.gameObject.rigidbody.AddForce(heading*10000);
			
		}
	}
}
