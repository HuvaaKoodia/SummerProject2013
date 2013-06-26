using UnityEngine;
using System.Collections;

public class Bouncy : MonoBehaviour,ProjectileModifier {
	
	ProjectileMain pro;
	// Use this for initialization
	void Start () {
		pro=GetComponent<ProjectileMain>();
	}
	
	// Update is called once per frame
	void Update (){
	
	}
	
	void OnCollisionEnter(Collision other){
		//rigidbody.AddForce(other.contacts[0].normal*rigidbody.velocity.magnitude*rigidbody.mass*40);
		//pro.setMoveDirection(new Vector3(pro.MoveDirection.x,4,pro.MoveDirection.z));//Mathf.Max(1,Mathf.Min(15,rigidbody.velocity.y*6))
		
		//pro.setDirection(new Vector3(pro.MoveDirection.x,4*pro.SpeedMulti,pro.MoveDirection.z));
		pro.setDirection(new Vector3(pro.MoveDirection.x,4,pro.MoveDirection.z));
	}
}
