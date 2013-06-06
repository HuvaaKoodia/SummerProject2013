using UnityEngine;
using System.Collections;

public class Bouncy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update (){
	
	}
	
	void OnCollisionEnter(Collision other){
		rigidbody.AddForce(other.contacts[0].normal*rigidbody.velocity.magnitude*rigidbody.mass*10);
	}
}
