using UnityEngine;
using System.Collections;

public class SlowMoField : MonoBehaviour {

	// Use this for initialization
	void Start () {
		rigidbody.isKinematic=true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	/*
	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag=="Projectile"&&!other.collider.isTrigger){
			var proMain=other.GetComponent<ProjectileMain>();
			proMain.rigidbody.velocity*=0.5f;
		}
	}
	
	void OnTriggerExit(Collider other){
		if (other.gameObject.tag=="Projectile"&&!other.collider.isTrigger){
			var proMain=other.GetComponent<ProjectileMain>();
			proMain.rigidbody.velocity*=2f;
		}
	}*/
	
	void OnTriggerStay(Collider other){
		if (other.gameObject.tag=="Projectile"&&!other.collider.isTrigger){
			var proMain=other.GetComponent<ProjectileMain>();
			proMain.SpeedMulti=0.2f;
		}
	}
}
