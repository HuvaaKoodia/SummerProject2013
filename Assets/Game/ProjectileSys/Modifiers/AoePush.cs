using UnityEngine;
using System.Collections;
using NotificationSys;

public class AoePush : MonoBehaviour,ProjectileModifier {
	GameObject explosion;
	PlayerMain plr;
	float radius, force;
	// Use this for initialization
	void Start () {
		
		force=GetComponent<ProjectileMain>().mod_stats.Knockback;
		radius=GetComponent<ProjectileMain>().mod_stats.Radius;
		
		plr = GetComponent<ProjectileMain>().Creator;
		
		plr.IgnoreExplosion();
	
		NotificationCenter.Instance.sendNotification(new Explosion_note(plr.transform.position+Vector3.down, force*10000, radius));
		NotificationCenter.Instance.sendNotification (new Knockback_note (plr.transform.position, force, radius,1f,null));
	} 
	
	
}
