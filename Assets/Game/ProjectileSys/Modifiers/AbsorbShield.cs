using UnityEngine;
using System.Collections;

public class AbsorbShield : MonoBehaviour, ProjectileModifier {
	ProjectileMain pro_main;
	PlayerMain plr_main;
	GameObject shield;
	float strenght;
	// Use this for initialization
	void Start () {
		pro_main=GetComponent<ProjectileMain>();
		plr_main = pro_main.Creator;
		transform.position = plr_main.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (plr_main!=null)
			transform.position = plr_main.transform.position;
	}
	
	/*void OnCollisionEnter(Collision other){
		if (other.gameObject.tag=="Projectile"&&!other.collider.isTrigger)
			Destroy(other.gameObject);
	}*/
	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag=="Projectile"){
		pro_main.sound.playCollisionSound();
		}
		if (other.gameObject.tag=="Projectile"&&!other.collider.isTrigger)
			Destroy(other.gameObject);
	}
	void OnTriggerStay(Collider other){
		if (other.gameObject.tag=="Projectile"&&!other.collider.isTrigger)
			Destroy(other.gameObject);
	}
}
