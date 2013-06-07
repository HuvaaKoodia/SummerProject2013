using UnityEngine;
using System.Collections;

public class Ricochet : MonoBehaviour {

	
	ProjectileMain projectile;
	// Use this for initialization
	void Start () {
		projectile=transform.GetComponent<ProjectileMain>();
	}
	
	// Update is called once per frame
	void Update (){
		rigidbody.velocity=rigidbody.velocity.normalized*projectile.MoveDirection.magnitude;
	}
}
