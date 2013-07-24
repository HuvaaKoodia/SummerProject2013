using UnityEngine;
using System.Collections;

public class Charge : MonoBehaviour, ProjectileModifier {
	
	public float auto_stop_angle=35;
	
	ProjectileMain pro_main;
	PlayerMain player;
	Vector3 heading;
	float force,power,knockback;
	Vector3 speedVector;
	
	// Use this for initialization
	void Start (){
		pro_main=GetComponent<ProjectileMain>();
		player = pro_main.Creator;
		heading = player.LowerGraphicsDir*Vector3.forward;
		force=pro_main.mod_stats.Speed;
		power=pro_main.mod_stats.Power;
		knockback=pro_main.mod_stats.Knockback;
		
		speedVector=heading * (force / 2f);

		player.DashStart(speedVector);
	}
	
	// Update is called once per frame
	void Update () {
		if (!player){
			Destroy(gameObject);
			return;
		}
		transform.position = player.transform.position;
	}
	
	void OnDestroy(){
		if (player!=null)
			player.DashEnd();
	}
	
	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag=="Player" && other.gameObject != player.gameObject&&player.ReallyDashing){
			var other_p=other.GetComponent<PlayerMain>();
			
			other_p.KNOCKBACKHAX(player.transform.position+Vector3.down,knockback,-1,1);
			other_p.HP-=power;
			
			float angle=Vector3.Angle(heading,other.transform.position-player.transform.position);
			if (angle<auto_stop_angle){
				Destroy(gameObject);
			}
		}
	}
}
