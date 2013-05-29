using UnityEngine;
using System.Collections;

public class ProjectileMain : MonoBehaviour {
	
	//DEV.TEMP projectile type variables
	public bool DestroyOnGround=false;
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void setDirection(Vector3 direction,float speed){
		rigidbody.velocity=direction*speed;
	}
	
	
	public void OnCollision(Collision other){
		if (other.gameObject.tag=="Ground"){
			if (DestroyOnGround)
				Destroy(this);
		}
	}
}
