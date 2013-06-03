using UnityEngine;
using System.Collections;

public class CharacterMain : MonoBehaviour {
	
	public Transform projectile_prefab;
	
	bool onGround,canJump;	
	float acceleration=50,
		jump_speed=100,jump_speed_max=10,
		speed_max=2;
	
	float l_axis_x,l_axis_y,r_axis_x,r_axis_y;
	
	Transform graphics,aim_dir;
	
	Timer jump_timer;
	
	public int controllerNumber=1;
	public Color color;
	
	// Use this for initialization
	void Start () {
		graphics=transform.Find("Graphics").Find ("temp") as Transform;
		
		jump_timer=new Timer(400,OnJumpTimer);
		
		aim_dir=transform.Find("aim_direction") as Transform;
	}
	
	void Update(){
		
		l_axis_x=Input.GetAxis("L_XAxis_"+controllerNumber);
		l_axis_y=Input.GetAxis("L_YAxis_"+controllerNumber);
		
		r_axis_x=Input.GetAxis("R_XAxis_"+controllerNumber);
		r_axis_y=Input.GetAxis("R_YAxis_"+controllerNumber);

		if (Input.GetButtonDown("LB_"+controllerNumber)){

			var forward=transform.TransformDirection(new Vector3(r_axis_x, 0, -r_axis_y));
			var obj=Instantiate(projectile_prefab,transform.position+forward*2,Quaternion.identity) as Transform;
			var pro=obj.GetComponent<ProjectileMain>();
			pro.setDirection(forward.normalized,10);
		}
		
		if (Input.GetButtonDown("RB_"+controllerNumber)){

			var forward=transform.TransformDirection(new Vector3(r_axis_x, 0, -r_axis_y));
			var obj=Instantiate(projectile_prefab,transform.position+forward*2,Quaternion.identity) as Transform;
			var pro=obj.GetComponent<ProjectileMain>();
			pro.setDirection(forward.normalized,10);
		}
		
		if (Input.GetAxis("LT_"+controllerNumber)>0){

			var forward=transform.TransformDirection(new Vector3(r_axis_x, 0, -r_axis_y));
			var obj=Instantiate(projectile_prefab,transform.position+forward*2,Quaternion.identity) as Transform;
			var pro=obj.GetComponent<ProjectileMain>();
			pro.setDirection(forward.normalized,10);
		}
		
		if (Input.GetAxis("RT_"+controllerNumber)>0){

			var forward=transform.TransformDirection(new Vector3(r_axis_x, 0, -r_axis_y));
			var obj=Instantiate(projectile_prefab,transform.position+forward*2,Quaternion.identity) as Transform;
			var pro=obj.GetComponent<ProjectileMain>();
			pro.setDirection(forward.normalized,10);
		}
		
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		rigidbody.WakeUp();
		
		if (onGround){
		

			if (l_axis_x<0){
				rigidbody.AddForce(Vector3.left*acceleration);
			}
			
			if (l_axis_x>0){
				rigidbody.AddForce(Vector3.right*acceleration);
			}
			
			if (l_axis_y<0){
				rigidbody.AddForce(Vector3.forward*acceleration);
			}
			
			if (l_axis_y>0){
				rigidbody.AddForce(Vector3.back*acceleration);
			}
			
			//jump
			if (Input.GetButton("A_"+controllerNumber)){
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
		
		graphics.renderer.material.color=color;
		//if (onGround)
		//	graphics.renderer.material.color=Color.green;

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




		
		/*DEV. mouse 
		var mpos=Input.mousePosition+Vector3.up*4;
		var mouse_pos_dif=mpos-Camera.main.WorldToScreenPoint(transform.position);
		mouse_pos_dif=Vector3.ClampMagnitude(mouse_pos_dif,1);	
		mouse_pos_dif.z=mouse_pos_dif.y;
		mouse_pos_dif.y=0;

		//ability usage
		if (Input.GetMouseButtonDown(0)){
			
			var forward=transform.TransformDirection(mouse_pos_dif);
			var obj=Instantiate(projectile_prefab,transform.position+forward*2,Quaternion.identity) as Transform;
			var pro=obj.GetComponent<ProjectileMain>();
			pro.setDirection(forward,10);
			
			//Debug.Log("f: "+forward);
		}
		*/


			/* DEV. keyboard
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
			*/
			