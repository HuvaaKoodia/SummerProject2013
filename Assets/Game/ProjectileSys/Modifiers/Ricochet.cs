using UnityEngine;
using System.Collections;

public class Ricochet : MonoBehaviour,ProjectileModifier {
	ProjectileMain projectile;
	public int random_number;
	// Use this for initialization
	void Start () {
		projectile=transform.GetComponent<ProjectileMain>();
	}
	
	// Update is called once per frame
	void Update (){
		rigidbody.velocity=rigidbody.velocity.normalized*projectile.MoveDirection.magnitude;
	}
}
