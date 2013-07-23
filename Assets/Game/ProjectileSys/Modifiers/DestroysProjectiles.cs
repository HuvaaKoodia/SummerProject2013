using UnityEngine;
using System.Collections;

public class DestroysProjectiles : MonoBehaviour,ProjectileModifier {

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag=="Projectile"&&!other.collider.isTrigger)
			Destroy(other.gameObject);
	}
	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag=="Projectile"&&!other.collider.isTrigger)
			Destroy(other.gameObject);
	}
}
