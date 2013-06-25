using UnityEngine;
using System.Collections;

public class DieOnCollision : MonoBehaviour,ProjectileModifier {
	
	
	// Use this for initialization
	void Start () {
		rigidbody.collisionDetectionMode=CollisionDetectionMode.Discrete;
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void OnCollisionEnter(Collision other){
		Destroy(gameObject);
	}
}
