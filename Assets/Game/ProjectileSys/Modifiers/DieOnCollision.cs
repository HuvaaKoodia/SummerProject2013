using UnityEngine;
using System.Collections;

public class DieOnCollision : MonoBehaviour,ProjectileModifier {
	
	void Start () {
		rigidbody.collisionDetectionMode=CollisionDetectionMode.Discrete;
	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag!="Projectile")
			Destroy(gameObject);
	}
}
