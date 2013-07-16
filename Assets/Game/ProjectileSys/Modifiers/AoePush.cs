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
	} 
	
	// Update is called once per frame
	void Update () {
		plr.IgnoreExplosion();
	
		NotificationCenter.Instance.sendNotification(new Explosion_note(plr.transform.position+Vector3.down, force, radius));
		
		Destroy(gameObject);
	}
}
