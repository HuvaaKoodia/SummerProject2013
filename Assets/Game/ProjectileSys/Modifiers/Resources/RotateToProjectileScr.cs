using UnityEngine;
using System.Collections;

public class RotateToProjectileScr : MonoBehaviour {
	
	public ProjectileMain pro;
	// Use this for initialization
	void Start () {
		Update ();
	}
	
	// Update is called once per frame
	void Update (){
		var newRotation = Quaternion.LookRotation(-pro.MoveDirection);
		transform.rotation=newRotation;
	}
}
