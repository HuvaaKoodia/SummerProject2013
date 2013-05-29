using UnityEngine;
using System.Collections;

public class CharacterMain : MonoBehaviour {

	bool onGround,canJump;	
	float acceleration=50,
		jump_speed=1000,jump_speed_max=10,
		speed_max=10;

	Transform graphics;
	
	Timer jump_timer;
	
	// Use this for initialization
	void Start () {
		graphics=transform.Find("Graphics").Find ("temp") as Transform;
		
		jump_timer=new Timer(400,OnJumpTimer);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		rigidbody.WakeUp();
		
		
		if (onGround){
		
			if (Input.GetKey(KeyCode.A)){
				rigidbody.AddForce(Vector3.left*acceleration);
			}
			
			if (Input.GetKey(KeyCode.D)){
				rigidbody.AddForce(Vector3.right*acceleration);
			}
			
			if (Input.GetKey(KeyCode.W)){
				rigidbody.AddForce(Vector3.forward*acceleration);
			}
			
			if (Input.GetKey(KeyCode.S)){
				rigidbody.AddForce(Vector3.back*acceleration);
			}
			
			//jump
			if (Input.GetKeyDown(KeyCode.Space)){
				if (canJump){
					rigidbody.AddForce(Vector3.up*jump_speed);
					canJump=false;
				}
			}
		}
		//restrict movement speed
		var xz_vec=new Vector2(rigidbody.velocity.x,rigidbody.velocity.z);
		
		if (xz_vec.magnitude>speed_max){
			xz_vec=Vector2.ClampMagnitude(xz_vec,speed_max);
		}
		
		rigidbody.velocity=new Vector3(
			xz_vec.x,
			Mathf.Clamp(rigidbody.velocity.y,-jump_speed,jump_speed_max),
			xz_vec.y
			);
		
		graphics.renderer.material.color=Color.red;
		if (onGround)
			graphics.renderer.material.color=Color.green;

		jump_timer.Active=true;
		canJump=true;
	}
	
	
	void OnCollisionStay(Collision other){
		if (other.gameObject.tag=="Ground"){
			onGround=true;
			
			jump_timer.Active=false;
			jump_timer.Reset();
		}
	}
	
	void OnJumpTimer(){
		onGround=false;
	}
	
	void OnDestroy(){
		jump_timer.Destroy();
	}
}
