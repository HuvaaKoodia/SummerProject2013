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
		//rigidbody.AddForce(other.contacts[0].normal*rigidbody.velocity.magnitude*rigidbody.mass*40);
		rigidbody.velocity=new Vector3(rigidbody.velocity.x,4,rigidbody.velocity.z);//Mathf.Max(1,Mathf.Min(15,rigidbody.velocity.y*6))
	}
}
