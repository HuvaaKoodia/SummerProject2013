using UnityEngine;
using System.Collections;

public class DestroysProjectiles : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag=="Projectile"&&!other.collider.isTrigger)
			Destroy(other.gameObject);
	}
	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag=="Projectile"&&!other.collider.isTrigger)
			Destroy(other.gameObject);
	}
}
